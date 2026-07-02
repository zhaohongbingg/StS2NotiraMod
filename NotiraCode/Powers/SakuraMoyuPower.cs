using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Cards;
using Notira.Notira.Extensions;
using Notira.Notira.Powers;
namespace Notira.Notira.Powers;




public sealed class SakuraMoyoPower : NotiraPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    private int Sign => 1;

    public override async Task BeforeApplied(Creature target, decimal amount, Creature? applier, CardModel? cardSource)
    {
        await CreatureCmd.GainMaxHp(target, (int)(Sign * amount));
    }

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature applier, CardModel cardSource)
    {
        if (!(amount == (decimal)base.Amount) && power == this)
        {
            if (amount > 0)
            {
                await CreatureCmd.GainMaxHp(base.Owner, (int)amount);
            }
            else if (amount < 0)
            {
                BlockingPlayerChoiceContext blockingContext = new BlockingPlayerChoiceContext();
                await CreatureCmd.LoseMaxHp(blockingContext, base.Owner, (int)(-amount), isFromCard: false);
            }
        }
    }

    public override async Task AfterCombatEnd(CombatRoom room)
    {
        BlockingPlayerChoiceContext context = new BlockingPlayerChoiceContext();
        await CreatureCmd.LoseMaxHp(context, base.Owner, base.Amount, isFromCard: false);
    }

}


