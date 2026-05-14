using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.RestSite;
using MegaCrit.Sts2.Core.Nodes.Screens.GameOverScreen;
using Notira.Notira.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Godot.Node;
using static Godot.PackedScene;

[HarmonyPatch(typeof(NCreature), "SetAnimationTrigger")]
public static class AnimationPatch
{
    public static void Postfix(NCreature __instance, string trigger)
    {
        if (__instance.Entity == null || !__instance.Entity.IsPlayer)
            return;

        if (__instance.Entity.ModelId.ToString() == "CHARACTER.NOTIRA")
            Log.Info("[>>>NotiraMode AnimeTrigger=]" + trigger);
        switch (trigger)
        {
            case "Hit":
                PlayAnim(__instance, "Hit", false);
                break;

            case "Attack":
                PlayAnim(__instance, "Attack", false);
                break;

            case "Cast":
                PlayAnim(__instance, "Attack", true);
                break;

            case "Dead":
                PlayAnim(__instance, "Dead", false);
                break;

            default:
                PlayAnim(__instance, "Idle", false);
                break;
        }
    }

    private static void PlayAnim(NCreature node, string animName, bool fromEnd)
    {
        var visual = node.GetNodeOrNull<Node2D>("NotiraCH");
        if (visual == null) return;

        var anim = visual.GetNodeOrNull<AnimatedSprite2D>("Visuals");
        if (anim == null) return;

        // 切换动画
        anim.Frame = 0;
        anim.Play(animName, 1f, fromEnd);

        // 动画结束后回到 Idle
        anim.Connect("animation_finished", Callable.From(() =>
        {
            anim.Play("Idle");
        }), 4u);
    }
    [HarmonyPatch(typeof(NGameOverScreen), "MoveCreaturesToDifferentLayerAndDisableUi")]
    public static class NotiraGameOverPatch
    {
        public static void Postfix(NGameOverScreen __instance)
        {
            var _creatureContainer = __instance.GetNodeOrNull<Control>("%CreatureContainer");
            Log.Info("[>>>NotiraMode AnimeTrigger=]DEAD" );
            foreach (Node2D visual in _creatureContainer.GetChildren())
            {
                if (visual.Name.ToString().StartsWith("Notira")) visual.GetNodeOrNull<AnimatedSprite2D>("Visuals").Play("Dead");
            }
        }
    }
}

