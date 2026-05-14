using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using Notira.Notira.Powers;
 
namespace Notira.Notira.Powers;



public sealed class Combo : NotiraPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

   

    public override int ModifyCardPlayCount(CardModel card, Creature? target, int playCount)
    {
        CombatHistory history = CombatManager.Instance.History;

        List<CardPlayFinishedEntry> plays = history.CardPlaysFinished
             .TakeLast(2)
             .ToList();



        var currentCost = card.EnergyCost.GetAmountToSpend();
        if (plays.Count >= 2)
        {
            var prevCost = plays[0].CardPlay.Card.EnergyCost.GetAmountToSpend();
            var lastCost = plays[1].CardPlay.Card.EnergyCost.GetAmountToSpend();
          

            // 检查是否构成连续等差序列（方向一致，步长为1）
            bool isDoubleSequence = (lastCost - prevCost == 1 && currentCost - lastCost == 1) ||
                                    (lastCost - prevCost == -1 && currentCost - lastCost == -1);
            if (isDoubleSequence)
                return playCount + 2;

            // 否则检查单次相邻变化
            if (Math.Abs(currentCost - lastCost) == 1)
                return playCount + 1;
        }
        else if (plays.Count == 1)  // 仅有一个历史记录时
        {
            var lastCost = plays[0].CardPlay.Card.EnergyCost.GetAmountToSpend();
            if (Math.Abs( currentCost- lastCost) == 1)
                return playCount + 1;
        }

        return playCount;


    }
  
}