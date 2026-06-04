 
using MegaCrit.Sts2.Core.Entities.Creatures;
 
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
 
using MegaCrit.Sts2.Core.ValueProps;
 
using MegaCrit.Sts2.Core.Entities.Powers;
 

 

namespace Notira.Notira.Powers;


public sealed class SayaPower : NotiraPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("DamageDecrease", 0.5m)];


    public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
    {   if (target != base.Owner)
            return 1m;
        if (dealer == null)
            return 1m;

        if (!target.HasPower<KichikuPower>())
        {
            return 1m;
        }
       
        return base.DynamicVars["DamageDecrease"].BaseValue;
    }


}