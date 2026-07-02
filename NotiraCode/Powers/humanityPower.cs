using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Powers;
using System.Threading.Tasks;

namespace Notira.Notira.Powers;


public sealed class HumanityPower : NotiraPower
{

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (side == base.Owner.Side)
        {
            await PowerCmd.Apply<WeakPower>(new ThrowingPlayerChoiceContext(), base.Owner, 1, base.Owner, null, false);
            await PowerCmd.Apply<FrailPower>(new ThrowingPlayerChoiceContext(), base.Owner, 1, base.Owner, null, false);
        }
    }


}

