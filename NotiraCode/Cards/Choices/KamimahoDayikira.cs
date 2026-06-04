using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
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
using Notira.Notira.Cards;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;






public sealed class KamimahoDayikira : BitterChoiceOptionCard
{
    
  public KamimahoDayikira() :base(0, CardType.Skill, CardRarity.Token, TargetType.Self)
{}

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    public override bool CanBeGeneratedInCombat => false;
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]{
    
        new CalculationBaseVar(0m),
        new CalculationExtraVar(1m),
        new CalculatedBlockVar(ValueProp.Move).WithMultiplier((card,_) => card.Owner.Creature.GetPowerAmount<XPoint>())
        };


    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
            HoverTipFactory.FromPower<XPoint>()



];



    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(
        base.Owner.Creature,
        DynamicVars.CalculatedBlock.Calculate(cardPlay.Target),
        DynamicVars.CalculatedBlock.Props,
        cardPlay
    ); 
        await PowerCmd.Apply<XPoint>(choiceContext, base.Owner.Creature, -DynamicVars.CalculatedBlock.Calculate(cardPlay.Target)*0.5m, base.Owner.Creature, this);


    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);


    }
}
