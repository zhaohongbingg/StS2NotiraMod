using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using Notira.Notira.Cards;
using Notira.Notira.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;

public class TrueEND():NotiraCard(
    0,CardType.Skill,CardRarity.Rare,TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    new DynamicVar[]
    {
        new EnergyVar(2),
        new CardsVar(3) };
    public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
{
         
         
};
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        int num = Math.Min(base.DynamicVars.Cards.IntValue, 10 - PileType.Hand.GetPile(base.Owner).Cards.Count);
        if (num > 0)
        {
            await CardPileCmd.Add(await CardSelectCmd.FromSimpleGrid(choiceContext, PileType.Discard.GetPile(base.Owner).Cards, base.Owner, new CardSelectorPrefs(base.SelectionScreenPrompt, num)), PileType.Hand);
        }
        await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);


       
    }
    protected override void OnUpgrade()
    {
        DynamicVars.Energy.UpgradeValueBy(1);
        DynamicVars.Cards.UpgradeValueBy(1);
    }

}

