using BaseLib.Utils;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Nodes.Screens.Timeline;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Saves.Managers;
using MegaCrit.Sts2.Core.Timeline;
using Notira.Notira.Interactions.RightClick;
using Notira.Notira.Interactions.RightClick.Examples;
using Notira.Notira.Interactions.RightClick.Tests;

namespace Notira.Notira;

/**
 * Ideas
 * 
 * Self Bind
 * 
 * Bind effect - square texture based on model size, lines random generated (amount equal to bind amount)
 * shader of transparency of line based on average of point spread of the model
 * colored
 * 
 * Bind... rename? Necrobinder kinda overlaps.
 * */

[ModInitializer(nameof(Initialize))]
public class MainFile
{
    public const string ModId = "Notira"; //At the moment, this is used only for the Logger and harmony names.

    public static Logger Logger { get; } =
        new(ModId, LogType.Generic);

    public static void Initialize()
    {
        Harmony harmony = new(ModId);

        harmony.PatchAll();

        // Initialize right-click system
        // 初始化右键事件系统
        InitializeRightClickSystem();
    }

    private static void InitializeRightClickSystem()
    {
        // Register example right-click handler
        // 注册示例右键处理器
        RightClickExample.RegisterExampleHandler();

        // Register example card right-click binding
        // 注册示例卡片右键绑定
        RightClickExample.RegisterExampleCardRightClick();

        // Run test
        // 运行测试
        RightClickSystemTest.RunTest();

        Logger.Info("[Notira] Right-click system initialized");
    }
}
