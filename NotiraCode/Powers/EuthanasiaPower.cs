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
using Notira.Notira.Cards;
using Notira.Notira.Extensions;
using Notira.Notira.Powers;
namespace Notira.Notira.Powers;


public class EuthanasiaPower : NotiraPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool ShouldReceiveCombatHooks => true;
    public PowerModel InternallyAppliedPower => ModelDb.Power<StrengthPower>();

    private int Sign => -1;

    public override async Task BeforeApplied(Creature target, decimal amount, Creature? applier, CardModel? cardSource)
    {
        await PowerCmd.Apply<StrengthPower>(null, target, (decimal)Sign * amount, applier, cardSource, silent: true);
    }

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature applier, CardModel cardSource)
    {
        if (!(amount == (decimal)base.Amount) && power == this)
        {
            await PowerCmd.Apply<StrengthPower>(choiceContext, base.Owner, (decimal)Sign * amount, applier, cardSource, silent: true);
        }
    }

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == Owner.Side)
        {
            Flash();
            await PowerCmd.Remove(this);
            await PowerCmd.Apply<StrengthPower>(choiceContext, Owner, -Sign * Amount, Owner, null);
        }
    }

    public override string CustomPackedIconPath
       => "res://Notira/Images/Powers/euthanasia_power.png";

    public override string CustomBigIconPath
        => "res://Notira/Images/Powers/euthanasia_power.png";
}