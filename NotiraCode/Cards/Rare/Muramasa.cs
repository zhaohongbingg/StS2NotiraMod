using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Achievements;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Cards;
using Notira.Notira.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;



public class Muramasa() : NotiraCard(3, CardType.Skill, CardRarity.Rare, TargetType.AnyAlly)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new CalculationBaseVar(0m),
        new ExtraDamageVar(1m),
      new CalculatedDamageVar(ValueProp.Move)
    .WithMultiplier((CardModel card, Creature? target) =>
        target?.CurrentHp * 0.9m ?? 0m
    )
    };
  


    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
     
            await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
            foreach (Creature enemy in base.CombatState.HittableEnemies)
            {

                if (enemy == null || enemy.IsDead)
                    continue;
                await CreatureCmd.Kill(enemy);


            
        }
    }
    protected override void OnUpgrade()
    {
      
        base.EnergyCost.UpgradeBy(-1);
    }
}
