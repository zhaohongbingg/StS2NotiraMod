using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Achievements;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Cards;
using Notira.Notira.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;


public class Euthanasia() : NotiraCard(2, CardType.Attack, CardRarity.Common, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
      new DamageVar(2,ValueProp.Unblockable),
      new RepeatVar(10),
 


    };
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<EuthanasiaPowers>()

];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {


        AttackCommand attackCommand = await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .WithHitCount((int)DynamicVars.Repeat.BaseValue)
            .FromCard(this)
            .TargetingAllOpponents(CombatState)
            .WithHitFx("vfx/vfx_attack_slash")
            .OnlyPlayAnimOnce()
            .Execute(choiceContext);
        IEnumerable<DamageResult> allResults = attackCommand.Results.SelectMany(list => list);
        var hitsPerTarget = allResults
     .Where(r =>r.UnblockedDamage > 0)
     .GroupBy(r => r.Receiver)
     .ToDictionary(g => g.Key, g => g.Count());

        foreach (Creature enemy in CombatState.HittableEnemies)
        {
            int hitsOnThisEnemy = hitsPerTarget.GetValueOrDefault(enemy, 0);
            if (hitsOnThisEnemy > 0)
            {
                await PowerCmd.Apply<EuthanasiaPower>(choiceContext, enemy, hitsOnThisEnemy, Owner.Creature, this);
            }
        }
      
    }



    protected override void OnUpgrade()
    {

        base.DynamicVars.Repeat.UpgradeValueBy(5);
    }
}


