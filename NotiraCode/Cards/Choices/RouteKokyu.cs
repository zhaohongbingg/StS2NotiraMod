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





public class Routekokyu() : NotiraCard(0, CardType.Skill, CardRarity.Token, TargetType.Self)
{

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    public override bool CanBeGeneratedInCombat => false;
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]{
 
    new DynamicVar("Blood",5m),
    new DynamicVar("Sakura",1m),
    new DynamicVar("Tantie",2.5m),
    new DynamicVar("NoWing",1m)
    };
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<BloodPower>(),
   HoverTipFactory.FromPower<SakuraPower>(),
   HoverTipFactory.FromPower<TantiePower>(),
   HoverTipFactory.FromPower<NoWingPower>()

];



    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        foreach (Creature hittableEnemy in base.CombatState.HittableEnemies)
        {
            await PowerCmd.Apply<BloodPower>(choiceContext, hittableEnemy, DynamicVars["Blood"].BaseValue, Owner.Creature, this);
            await PowerCmd.Apply<SakuraPower>(choiceContext, hittableEnemy, DynamicVars["Sakura"].BaseValue, Owner.Creature, this);
            await PowerCmd.Apply<TantiePower>(choiceContext, hittableEnemy, DynamicVars["Tantie"].BaseValue, Owner.Creature, this);
            await PowerCmd.Apply<NoWingPower>(choiceContext, hittableEnemy, DynamicVars["NoWing"].BaseValue, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        base.BaseReplayCount += 1;
 
    }
}