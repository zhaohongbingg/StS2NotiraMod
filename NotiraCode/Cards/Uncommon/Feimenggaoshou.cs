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




   
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int a = base.Owner.Creature.GetPowerAmount<XPoint>();
        int b = 0;
        b = a % 5;
        a /= 5;
        await PowerCmd.Remove<XPoint>(Owner.Creature);
        await PowerCmd.Apply<XPoint>(Owner.Creature, new DynamicVar("XPiont", b).BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<DexterityPower>(Owner.Creature, new DynamicVar("DEXTERITY", a).BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<StrengthPower>(Owner.Creature, new DynamicVar("STRENGTH", a).BaseValue, Owner.Creature, this);


      


    }
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<XPoint>()

];
    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
         
    }


}



