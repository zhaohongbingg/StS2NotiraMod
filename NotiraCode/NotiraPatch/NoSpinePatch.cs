using HarmonyLib;
using Godot;
using MegaCrit.Sts2.Core.Nodes.Combat;
using BaseLib.Utils;

namespace  Notira.Notira.Patches;
// 恋恋没有spine，所以得用这个补丁让 BaseLib 的整个 CustomAnimation 系统跳过 Koishi 的模型
// 避免 NCreature.Visuals 下的临时 AnimationPlayer 被缓存后导致内存问题或崩溃
// BaseLib 3.1.2 在多人模式下有 SpireField/ConditionalWeakTable 缓存问题
// 关键问题：当多个 Koishi 同时死亡时，BaseLib 3.1.2 的 AnimDie transpiler 可能导致死锁
// 解决方案：让 HasSpineAnimation 返回 false，然后通过 DeathAnimTime = 0f 跳过等待
public static class NoSpinePatch
{
    // 检查是否是 Koishi 角色
    private static bool IsNotira(NCreature creature)
    {
        return creature?.Entity?.ModelId.ToString() == "CHARACTER.NOTIRA-NOTIRA";
    }

    // 检查 Node 是否是 Koishi 的 Visuals 节点
    private static bool IsKoishiVisual(Node n)
    {
        if (n?.GetParent() is NCreature creature)
            return IsNotira(creature);
        return false;
    }

    // Patch 1: get_HasSpineAnimation - 对 Koishi 返回 false
    // 这样 BaseLib 的 AnimDie transpiler 会走 else 分支，使用 DeathAnimTime
    [HarmonyPostfix, HarmonyPatch(typeof(NCreature), "get_HasSpineAnimation")]
    public static void Postfix_HasSpine(NCreature __instance, ref bool __result)
    {
        if (IsNotira(__instance))
            __result = false;  // 改成 false，让它走 custom animation 路径
    }

    // Patch 2: CustomAnimation.HasCustomAnimation - 对 Koishi 返回 false
    // 使 BaseLib 的 AdjustTime 跳过自定义死亡动画时间
    [HarmonyPrefix, HarmonyPatch(typeof(CustomAnimation), "HasCustomAnimation")]
    public static bool Prefix_HasCustomAnimation(Node visualRoot, ref bool __result)
    {
        if (IsKoishiVisual(visualRoot))
        {
            __result = false;
            return false;
        }
        return true;
    }

    // Patch 3: CustomAnimation.PlayCustomAnimation - 对 Koishi 返回 false
    // 额外保险
    [HarmonyPrefix, HarmonyPatch(typeof(CustomAnimation), "PlayCustomAnimation")]
    public static bool Prefix_PlayCustomAnimation(Node n, ref bool __result)
    {
        if (IsKoishiVisual(n))
        {
            __result = false;
            return false;
        }
        return true;
    }
}