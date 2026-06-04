using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Powers;
using System.Formats.Asn1;

namespace Notira.Notira.Cards;


public class Feimenggaoshou() : NotiraCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{




    protected override bool IsPlayable => base.Owner.Creature.GetPowerAmount<XPoint>() >= 20;
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
         new DynamicVar("IntangiblePower", 1m)

    };
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
    
        
        await PowerCmd.Apply<IntangiblePower> (choiceContext, Owner.Creature, base.DynamicVars["IntangiblePower"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<XPoint> (choiceContext, Owner.Creature,20m, Owner.Creature, this);


      


    }
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<XPoint>(),
   HoverTipFactory.FromPower<IntangiblePower>()


];
    protected override void OnUpgrade()
    {
        base.DynamicVars["IntangiblePower"].UpgradeValueBy(1);


    }


}



