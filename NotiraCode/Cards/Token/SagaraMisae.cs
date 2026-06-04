using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars; 
using Notira.Notira.Keywords;
using Notira.Notira.Powers;
namespace Notira.Notira.Cards;

 
public class SagaraMisae() : NotiraCard(0, CardType.Skill, CardRarity.Token, TargetType.Self, false)
{
 
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new CardsVar(1),
        new DynamicVar("LightOrb", 1m)
    };
    public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
{
           NotiraKeyWords.CL
};
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
HoverTipFactory.FromPower<LightOrb>()

];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<LightOrb>(choiceContext, Owner.Creature, DynamicVars["LightOrb"].BaseValue, Owner.Creature, this);
        await CardPileCmd.Draw(choiceContext,base.DynamicVars.Cards.IntValue, base.Owner);
    }


    protected override void OnUpgrade()
    {
        DynamicVars["LightOrb"].UpgradeValueBy(1m);
        DynamicVars.Cards.UpgradeValueBy(1);

    } }