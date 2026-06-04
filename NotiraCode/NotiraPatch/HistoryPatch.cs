using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Models.Modifiers;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Cards.Holders;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.HoverTips;
using MegaCrit.Sts2.Core.Nodes.RestSite;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Screens.GameOverScreen;
using MegaCrit.Sts2.Core.Runs;
using Notira.Notira.Characters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Godot.Node;
using static Godot.PackedScene;
 
namespace Notira.Notira.Patches


{
    [HarmonyPatch]
    public static class HandHoverPatch
    {
        private const string MetaKey = "last_two_names";

        [HarmonyPatch(typeof(NHandCardHolder), "OnFocus")]
        [HarmonyPostfix]
        public static void AfterHandFocus(NHandCardHolder __instance)
        {
            Show(__instance);
        }

        [HarmonyPatch(typeof(NHandCardHolder), "OnUnfocus")]
        [HarmonyPostfix]
        public static void AfterHandUnfocus(NHandCardHolder __instance)
        {
            Remove(__instance);
        }
        private static bool IsNotira(NCreature creature)
        {
            return creature?.Entity?.ModelId.ToString() == "CHARACTER.NOTIRA-NOTIRA";
        }


        private static void Show(NHandCardHolder holder)
        {
            Remove(holder);

            var history = CombatManager.Instance.History;
            if (history == null) return;


            var plays = history.CardPlaysFinished.TakeLast(2).ToList();
            if (plays.Count == 0) return;

            var panel = new Control();
            panel.CustomMinimumSize = new Vector2(200, 80);

          /*  var lastCard = plays[1].CardPlay.Card;
            var firstCard = plays[0].CardPlay.Card;
            var tip1 = HoverTipFactory.FromCard(lastCard);
            var tip2 = HoverTipFactory.FromCard(firstCard);
            var tips = new List<IHoverTip> { tip1, tip2 };
            var hoverTipSet = NHoverTipSet.CreateAndShow(holder, tips, HoverTipAlignment.None);
            // 调整位置以适合卡片持有者
            hoverTipSet.SetAlignmentForCardHolder(holder);*/






            var label = new RichTextLabel();

            label.BbcodeEnabled = true;
            label.FitContent = true;
            label.ScrollActive = false;

            label.CustomMinimumSize = new Vector2(200, 80);

            label.Text = string.Join("\n",
                plays.Select(p =>
                    $"[color=##5DA9FF]{p.CardPlay.Card.EnergyCost.GetAmountToSpend()}[/color]\t" +
                     $"[color=red]{p.CardPlay.Card.Title}[/color]"
                )
            );





            panel.AddChild(label);

            panel.Position = new Vector2(-20, -100);

            holder.AddChild(panel);

            holder.SetMeta(MetaKey, panel);
        }
        /*        private static void Show(NHandCardHolder holder)
                {


                    var history = CombatManager.Instance?.History;
                    if (history == null) return;

                    var plays = history.CardPlaysFinished.TakeLast(2);
                    if (!plays.Any()) return;

                    var tips = plays
                        .Select(p => new CardHoverTip(p.CardPlay.Card))
                        .Cast<IHoverTip>()
                        .ToList();

                    HoverSystem.Show(holder, tips);
                }*/
        private static void Remove(NHandCardHolder holder)
        {
            if (!holder.HasMeta(MetaKey)) return;

            var panel = holder.GetMeta(MetaKey).As<Node>();
            panel?.QueueFree();

           
            holder.RemoveMeta(MetaKey);
            NHoverTipSet.Remove(holder);
        }
    }
    
    }