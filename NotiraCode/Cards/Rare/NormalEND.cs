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

public class NormalEND() : NotiraCard(
    0, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DynamicVar("Heal",10),
        new CalculationBaseVar(0m),
        new CalculationExtraVar(1m),
        new CalculatedBlockVar(ValueProp.Move).WithMultiplier((card,_) => card.Owner.Creature.CurrentHp)    };


    public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
{
        CardKeyword.Exhaust
        
};
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)

    {
        await CreatureCmd.Heal(Owner.Creature, DynamicVars["Heal"].BaseValue);
        await CreatureCmd.GainBlock(
        base.Owner.Creature,
        DynamicVars.CalculatedBlock.Calculate(cardPlay.Target),
        DynamicVars.CalculatedBlock.Props,
        cardPlay
    ); 

    }
    protected override void OnUpgrade()
    {
        DynamicVars["Heal"].UpgradeValueBy(5);

    }
}
       




