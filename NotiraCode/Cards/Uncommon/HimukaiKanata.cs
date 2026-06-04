using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Keywords;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;

 
public class HimukaiKanata() : NotiraCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{



    public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
 {
      CardKeyword.Exhaust
 };
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<XPoint>()

];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int a = base.Owner.Creature.GetPowerAmount<XPoint>();
      
        a /= 10;
       
        await PowerCmd.Apply<VigorPower>(Owner.Creature, new DynamicVar("VIGOR", a*2).BaseValue, Owner.Creature, this);





    }
    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);

    }


}
