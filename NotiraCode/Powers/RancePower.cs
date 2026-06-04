using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notira.Notira.Powers;

 

 

public sealed class RancePower :NotiraPower
{
    private bool _hasAlreadyBeenGivenBlock;

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
{
       new DynamicVar("Artfact",1)

};

    public override async Task AfterPlayerTurnStartEarly(PlayerChoiceContext choiceContext, Player player)
    {
         await PowerCmd.Apply<ArtifactPower>(choiceContext, Owner, DynamicVars["Artfact"].BaseValue, Owner,null);
    }
    public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
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
        if (!dealer.HasPower<RancePower>())
        {
            return 1m;
        }
        decimal Edamage = dealer.GetPowerAmount<KichikuPower>();
        return 1+Edamage*0.5m;

    }
}