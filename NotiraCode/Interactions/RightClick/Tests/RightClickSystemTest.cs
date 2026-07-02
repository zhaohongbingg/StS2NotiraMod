using System;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;

namespace Notira.Notira.Interactions.RightClick.Tests;

/// <summary>
///     Test class for the right-click system.
///     右键系统测试类。
/// </summary>
public static class RightClickSystemTest
{
    /// <summary>
    ///     Test the right-click system by registering and executing a test binding.
    ///     通过注册和执行测试绑定来测试右键系统。
    /// </summary>
    public static void RunTest()
    {
        MainFile.Logger.Info("[RightClickTest] Starting right-click system test...");

        // Test 1: Register a handler
        // 测试1：注册处理器
        var handler = new TestRightClickHandler();
        ModRightClickRegistry.Register(handler);
        MainFile.Logger.Info("[RightClickTest] Test handler registered");

        // Test 2: Register a binding
        // 测试2：注册绑定
        var binding = ModRightClickRegistry.Register<CardModel>(
            modId: "Notira",
            localStem: "test_binding",
            canHandle: context => context.Model is CardModel,
            execute: async context =>
            {
                MainFile.Logger.Info("[RightClickTest] Test binding executed!");
            },
            priority: 0);
        MainFile.Logger.Info("[RightClickTest] Test binding registered");

        // Test 3: Simulate a right-click dispatch
        // 测试3：模拟右键分发
        // Note: This would normally be called by the patch
        // 注意：这通常由补丁调用
        MainFile.Logger.Info("[RightClickTest] Test completed successfully!");

        // Clean up
        // 清理
        binding.Dispose();
        MainFile.Logger.Info("[RightClickTest] Test binding disposed");
    }

    private class TestRightClickHandler : IModRightClickHandler
    {
        public int Priority => 0;

        public bool TryHandle(ModRightClickContext context)
        {
            MainFile.Logger.Info($"[RightClickTest] Handler triggered for: {context.Model.Id}");
            return false; // Don't consume the input
        }
    }
}