using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Models;
using static Notira.Notira.MainFile;

namespace Notira.Notira.Interactions.RightClick;

/// <summary>
///     Registry and dispatcher for model right-click interactions.
///     模型右键交互的注册表与分发器。
/// </summary>
public static class ModRightClickRegistry
{
    private static readonly object Gate = new();
    private static long _nextBindingSequence;

    private static readonly List<IModRightClickHandler> Handlers = [];

    private static readonly List<RegisteredRightClickBinding> Bindings = [];

    /// <summary>
    ///     Registers a custom right-click handler. Higher priority handlers run first.
    ///     注册自定义右键 handler；优先级越高越先运行。
    /// </summary>
    public static void Register(IModRightClickHandler handler)
    {
        ArgumentNullException.ThrowIfNull(handler);
        lock (Gate)
        {
            if (Handlers.Contains(handler))
                return;

            Handlers.Add(handler);
            Handlers.Sort((a, b) => b.Priority.CompareTo(a.Priority));
        }
    }

    /// <summary>
    ///     Registers a right-click binding for models of type <typeparamref name="TModel" />.
    ///     为 <typeparamref name="TModel" /> 类型的模型注册右键绑定。
    /// </summary>
    /// <param name="modId">Owning mod id. 所属 mod id。</param>
    /// <param name="localStem">Local binding id stem. 本地 binding id stem。</param>
    /// <param name="canHandle">
    ///     Execution-time guard. It runs after the synced action resolves the model on each peer. Do not use this
    ///     delegate for local-only UI filtering.
    ///     执行期判定：同步动作在各端解析模型后调用。不要将它用于仅本地 UI 过滤。
    /// </param>
    /// <param name="execute">Right-click behavior. 右键行为。</param>
    /// <param name="priority">Binding priority; higher values run first. 优先级越高越先运行。</param>
    /// <returns>
    ///     A disposable registration handle.
    ///     可释放的注册句柄。
    /// </returns>
    public static IDisposable Register<TModel>(
        string modId,
        string localStem,
        Func<ModRightClickContext, bool> canHandle,
        Func<ModRightClickExecutionContext, Task> execute,
        int priority = 0)
        where TModel : AbstractModel
    {
        ArgumentNullException.ThrowIfNull(canHandle);

        return Register<TModel>(
            modId,
            localStem,
            execute,
            priority,
            null,
            context => canHandle(new(context.Player, context.Model, context.Trigger)));
    }

    /// <summary>
    ///     Registers a right-click binding for models of type <typeparamref name="TModel" />.
    ///     为 <typeparamref name="TModel" /> 类型的模型注册右键绑定。
    /// </summary>
    /// <param name="modId">Owning mod id. 所属 mod id。</param>
    /// <param name="localStem">Local binding id stem. 本地 binding id stem。</param>
    /// <param name="execute">Right-click behavior. 右键行为。</param>
    /// <param name="priority">Binding priority; higher values run first. 优先级越高越先运行。</param>
    /// <param name="canHandleLocal">
    ///     Optional local-only fast filter. Use only stable, local UI facts here; mutable gameplay state should be
    ///     checked in <paramref name="canExecute" /> or <paramref name="execute" />.
    ///     可选的仅本地快速过滤。这里只应使用稳定的本地 UI 信息；可变游戏状态应在
    ///     <paramref name="canExecute" /> 或 <paramref name="execute" /> 中检查。
    /// </param>
    /// <param name="canExecute">
    ///     Optional execution-time guard. It runs after the synced action resolves the model on each peer.
    ///     可选执行期判定：同步动作在各端解析模型后调用。
    /// </param>
    /// <returns>
    ///     A disposable registration handle.
    ///     可释放的注册句柄。
    /// </returns>
    public static IDisposable Register<TModel>(
        string modId,
        string localStem,
        Func<ModRightClickExecutionContext, Task> execute,
        int priority = 0,
        Func<ModRightClickContext, bool>? canHandleLocal = null,
        Func<ModRightClickExecutionContext, bool>? canExecute = null)
        where TModel : AbstractModel
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(modId);
        ArgumentException.ThrowIfNullOrWhiteSpace(localStem);
        ArgumentNullException.ThrowIfNull(execute);

        var id = new ModRightClickBindingId($"{modId}:{localStem}");
        var binding = new RegisteredRightClickBinding(
            id,
            typeof(TModel),
            canHandleLocal,
            canExecute,
            execute,
            priority,
            Interlocked.Increment(ref _nextBindingSequence));

        lock (Gate)
        {
            if (Bindings.Any(existing => existing.Id == id))
                throw new InvalidOperationException($"Right-click binding is already registered: {id}");

            Bindings.Add(binding);
            SortBindings();
        }

        return binding;
    }

    /// <summary>
    ///     Attempts to dispatch a local right-click request.
    ///     尝试分发一个本地右键请求。
    /// </summary>
    public static bool TryDispatch(ModRightClickContext context)
    {
        IModRightClickHandler[] handlers;
        lock (Gate)
        {
            handlers = [..Handlers];
        }

        MainFile.Logger.Info($"[RightClick] TryDispatch called for model: {context.Model.Id}, Type: {context.Model.GetType().FullName}");

        // Check if any handler consumes the input
        if (handlers.Any(handler => handler.TryHandle(context)))
            return true;

        MainFile.Logger.Info($"[RightClick] No handler consumed input, checking IModRightClickableModel interface...");

        // Check if the model implements IModRightClickableModel
        if (context.Model is IModRightClickableModel rightClickable)
        {
            MainFile.Logger.Info($"[RightClick] Model implements IModRightClickableModel! Processing...");

            // Check local filter
            try
            {
                if (!rightClickable.CanHandleRightClickLocal(context))
                {
                    MainFile.Logger.Info($"[RightClick] CanHandleRightClickLocal returned false");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MainFile.Logger.Warn(
                    $"[RightClick] Interface preflight failed. " +
                    $"ModelId='{context.Model.Id}' OwnerType='{context.Model.GetType().FullName}' " +
                    $"SourceType='{rightClickable.GetType().FullName}' Error='{ex.Message}'");
                return false;
            }

            MainFile.Logger.Info($"[RightClick] Executing right-click asynchronously...");

            // Execute asynchronously
            _ = Task.Run(async () =>
            {
                try
                {
                    var executionContext = new ModRightClickExecutionContext(
                        context.Player,
                        context.Model,
                        context.Trigger,
                        null,
                        null);

                    // Check execution guard
                    try
                    {
                        if (!rightClickable.CanExecuteRightClick(executionContext))
                        {
                            MainFile.Logger.Info($"[RightClick] CanExecuteRightClick returned false");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MainFile.Logger.Warn(
                            $"[RightClick] Interface execute guard failed. " +
                            $"ModelId='{context.Model.Id}' OwnerType='{context.Model.GetType().FullName}' " +
                            $"SourceType='{rightClickable.GetType().FullName}' Error='{ex.Message}'");
                        return;
                    }

                    MainFile.Logger.Info($"[RightClick] Calling OnRightClick...");

                    // Execute right-click
                    await rightClickable.OnRightClick(executionContext);

                    MainFile.Logger.Info($"[RightClick] OnRightClick completed successfully");
                }
                catch (Exception ex)
                {
                    MainFile.Logger.Warn(
                        $"[RightClick] Interface execution failed. " +
                        $"ModelId='{context.Model.Id}' OwnerType='{context.Model.GetType().FullName}' " +
                        $"SourceType='{rightClickable.GetType().FullName}' Error='{ex.Message}'");
                }
            });

            return true;
        }
        else
        {
            MainFile.Logger.Info($"[RightClick] Model does NOT implement IModRightClickableModel interface");
        }

        return false;
    }

    /// <summary>
    ///     Attempts to execute a right-click action for the given model.
    ///     尝试为给定模型执行右键动作。
    /// </summary>
    public static async Task<bool> TryExecuteRightClick(ModRightClickContext context)
    {
        var bindingIds = CollectBindingIds(context);
        if (bindingIds.Count == 0)
            return false;

        var executionContext = new ModRightClickExecutionContext(
            context.Player,
            context.Model,
            context.Trigger,
            null,
            null);

        var completed = false;
        foreach (var bindingId in bindingIds)
        {
            try
            {
                if (await TryExecuteBinding(bindingId, context.Model, executionContext))
                    completed = true;
            }
            catch (Exception ex)
            {
                MainFile.Logger.Warn(
                    $"[RightClick] Binding execution failed. BindingId='{bindingId}' " +
                    $"ModelId='{context.Model.Id}' OwnerType='{context.Model.GetType().FullName}' Error='{ex.Message}'");
            }
        }

        return completed;
    }

    private static List<ModRightClickBindingId> CollectBindingIds(ModRightClickContext context)
    {
        var bindings = GetBindingsSnapshot();
        var candidates = (from binding in bindings
            where binding.ModelType.IsInstanceOfType(context.Model)
            where TryCanHandleLocal(binding, context)
            select new LocalRightClickCandidate(binding.Id, binding.Priority, binding.Sequence)).ToList();

        return candidates
            .OrderByDescending(static candidate => candidate.Priority)
            .ThenBy(static candidate => candidate.Sequence)
            .Select(static candidate => candidate.Id)
            .ToList();
    }

    private static bool TryCanHandleLocal(RegisteredRightClickBinding binding, ModRightClickContext context)
    {
        if (binding.CanHandleLocal == null)
            return true;

        try
        {
            return binding.CanHandleLocal(context);
        }
        catch (Exception ex)
        {
            MainFile.Logger.Warn(
                $"[RightClick] Binding preflight failed. BindingId='{binding.Id}' " +
                $"ModelId='{context.Model.Id}' OwnerType='{context.Model.GetType().FullName}' " +
                $"Error='{ex.Message}'");
            return false;
        }
    }

    private static async Task<bool> TryExecuteBinding(
        ModRightClickBindingId bindingId,
        AbstractModel model,
        ModRightClickExecutionContext context)
    {
        var binding = TryGetBinding(bindingId);
        if (binding == null || !binding.ModelType.IsInstanceOfType(model))
            return false;

        if (!TryCanExecute(binding, context, out var canExecute))
            return false;

        if (!canExecute)
            return true;

        await binding.Execute(context);
        return true;
    }

    private static bool TryCanExecute(
        RegisteredRightClickBinding binding,
        ModRightClickExecutionContext context,
        out bool canExecute)
    {
        canExecute = true;
        if (binding.CanExecute == null)
            return true;

        try
        {
            canExecute = binding.CanExecute(context);
            return true;
        }
        catch (Exception ex)
        {
            MainFile.Logger.Warn(
                $"[RightClick] Binding execute guard failed. BindingId='{binding.Id}' " +
                $"ModelId='{context.Model.Id}' OwnerType='{context.Model.GetType().FullName}' " +
                $"Error='{ex.Message}'");
            return false;
        }
    }

    private static RegisteredRightClickBinding? TryGetBinding(ModRightClickBindingId bindingId)
    {
        lock (Gate)
        {
            return Bindings.FirstOrDefault(binding => binding.Id == bindingId);
        }
    }

    private static RegisteredRightClickBinding[] GetBindingsSnapshot()
    {
        lock (Gate)
        {
            return [..Bindings];
        }
    }

    private static void SortBindings()
    {
        Bindings.Sort((a, b) =>
        {
            var priority = b.Priority.CompareTo(a.Priority);
            return priority != 0 ? priority : a.Sequence.CompareTo(b.Sequence);
        });
    }

    private readonly record struct LocalRightClickCandidate(ModRightClickBindingId Id, int Priority, long Sequence);

    private sealed class RegisteredRightClickBinding(
        ModRightClickBindingId id,
        Type modelType,
        Func<ModRightClickContext, bool>? canHandleLocal,
        Func<ModRightClickExecutionContext, bool>? canExecute,
        Func<ModRightClickExecutionContext, Task> execute,
        int priority,
        long sequence) : IDisposable
    {
        private bool _disposed;

        public ModRightClickBindingId Id { get; } = id;
        public Type ModelType { get; } = modelType;
        public Func<ModRightClickContext, bool>? CanHandleLocal { get; } = canHandleLocal;
        public Func<ModRightClickExecutionContext, bool>? CanExecute { get; } = canExecute;
        public Func<ModRightClickExecutionContext, Task> Execute { get; } = execute;
        public int Priority { get; } = priority;
        public long Sequence { get; } = sequence;

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            lock (Gate)
            {
                Bindings.Remove(this);
            }
        }
    }
}