using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Keywords;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;




public class SisterJuliah() : NotiraCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{

 
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(7,ValueProp.Move)];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {    int a = base.Owner.Creature.GetPowerAmount<SakuraPower>();
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
        foreach(Creature enemy in base.CombatState.HittableEnemies)
        {
            if (enemy == cardPlay.Target)
            {
                continue;
            }
            await PowerCmd.Apply<SakuraPower>(enemy, new DynamicVar("Sakura", a).BaseValue, Owner.Creature, this);


        }




    }
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<SakuraPower>()

];

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
       


    }
}
