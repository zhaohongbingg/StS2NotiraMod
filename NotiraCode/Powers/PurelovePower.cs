using BaseLib.Extensions;
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


public sealed class PurelovePower : NotiraPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

 
    public override decimal ModifyDamageAdditive(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
    {
        if (!props.IsPoweredAttack())
        {
            return 1m;
        }
        if (cardSource == null)
        {
            return 1m;
        }
        if (cardSource.Owner.Creature != base.Owner)
        {
            return 1m;
        }
        if (!dealer.HasPower<PurelovePower>())
        {
            return 1m;
        }
        decimal Edamage = dealer.GetPowerAmount<XPoint>();
        return 1m + Edamage;

    }

}