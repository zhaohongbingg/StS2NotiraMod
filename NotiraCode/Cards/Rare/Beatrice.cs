using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;


public class  Beatrice(): NotiraCard(4,CardType.Skill,CardRarity.Rare,TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(10)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<Goldmajou>()

];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        decimal baseValue = base.DynamicVars.Cards.BaseValue;
        int count = base.Owner.PlayerCombatState.Hand.Cards.Count;
        decimal count2 = Math.Max(0m, baseValue - (decimal)count);
        await CardPileCmd.Draw(choiceContext, count2, base.Owner);
        await PowerCmd.Apply<Goldmajou>(choiceContext, base.Owner.Creature, 1, base.Owner.Creature, this);}
    protected override void OnUpgrade()
    {
       base.EnergyCost.UpgradeBy(-1);

    }


}