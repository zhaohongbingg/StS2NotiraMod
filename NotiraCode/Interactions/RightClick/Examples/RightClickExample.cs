using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;

namespace Notira.Notira.Interactions.RightClick.Examples;

/// <summary>
///     Example showing how to use the right-click system.
///     展示如何使用右键系统的示例。
/// </summary>
public static class RightClickExample
{
    /// <summary>
    ///     Example: Register a right-click binding for a specific card type.
    ///     示例：为特定卡片类型注册右键绑定。
    ///     NOTE: This is commented out to avoid conflicts with IModRightClickableModel testing.
    ///     注意：这里注释掉了，以避免与 IModRightClickableModel 测试冲突。
    /// </summary>
    public static void RegisterExampleCardRightClick()
    {
        // Commented out to allow IModRightClickableModel interface to work
        // 注释掉以允许 IModRightClickableModel 接口工作
        /*
        ModRightClickRegistry.Register<CardModel>(
            modId: "Notira",
            localStem: "example_card_rightclick",
            canHandle: context => context.Model is CardModel,
            execute: async context =>
            {
                if (context.Model is CardModel card)
                {
                    MainFile.Logger.Info($"[RightClick] Right-clicked card: {card.Id}");
                }
            },
            priority: 0);
        */
    }

    /// <summary>
    ///     Example: Create a custom right-click handler.
    ///     示例：创建自定义右键处理器。
    /// </summary>
    public class ExampleRightClickHandler : IModRightClickHandler
    {
        public int Priority => 0; // Lower priority, don't block other handlers

        public bool TryHandle(ModRightClickContext context)
        {
            // Example: Log right-clicks on all models
            // 示例：记录所有模型的右键点击
            MainFile.Logger.Info($"[RightClick] Custom handler triggered for: {context.Model.Id}");

            // Return false to NOT consume the input, allowing IModRightClickableModel to be checked
            // 返回 false 以不消耗输入，允许检查 IModRightClickableModel
            return false;
        }
    }

    /// <summary>
    ///     Example: Register the custom handler.
    ///     示例：注册自定义处理器。
    /// </summary>
    public static void RegisterExampleHandler()
    {
        ModRightClickRegistry.Register(new ExampleRightClickHandler());
    }
}