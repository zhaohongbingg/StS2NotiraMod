using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Ancients;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;
using MegaCrit.Sts2.Core.Platform;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves;
namespace MegaCrit.Sts2.Core.Models.Events;

using HarmonyLib;
[HarmonyPatch(typeof(TheArchitect), "WinRun")]
 
public static class TheArchitectPatch
{
   
    static bool Prefix(TheArchitect __instance)
    {
        if (__instance.Owner.Character is Notira.Notira.Characters.Notira)
        {
            if (__instance.Owner.RunState.Players.Count > 1)
            {
                NCombatRoom.Instance?.SetWaitingForOtherPlayersOverlayVisible(visible: true);
            }
            RunManager.Instance.ActChangeSynchronizer.SetLocalPlayerReady();
            return false;  
        }

        // 返回true表示原方法继续执行
        return true;
    }
}
