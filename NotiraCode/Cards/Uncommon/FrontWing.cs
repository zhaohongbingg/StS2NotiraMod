/*using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

using Notira.Notira.Powers;


namespace Notira.Notira.Cards;


public class FrontWing() : NotiraCard(
    1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
 {

         CardKeyword.Exhaust
 };
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var b = base.Owner.Creature.GetPowerAmount<KichikuPower>();
        await PowerCmd.Apply<XPoint>(choiceContext, Owner.Creature, new DynamicVar("XPiont", b*2).BaseValue, Owner.Creature, this, false);
        await PowerCmd.Remove<KichikuPower>(base.Owner.Creature);
    }
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<KichikuPower>(),
   HoverTipFactory.FromPower<XPoint>()

];
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);

    }

}

*/