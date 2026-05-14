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
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;

public class LightSheep():NotiraCard(2,CardType.Attack,CardRarity.Uncommon,TargetType.AllEnemies)
{	protected string soundpath   = "event:/sfx/characters/defect/defect_lightning_passive";
    protected string vfxpath = "vfx/vfx_attack_lightning";
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DamageVar(3,ValueProp.Unpowered),
         

    };
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        for (int i = 0; i < 21; i++)
        {
            foreach (Creature enemy in base.CombatState.HittableEnemies)
            {
                if (enemy == null || enemy.IsDead)
                    continue;

                SfxCmd.Play(soundpath);

                await CreatureCmd.Damage(choiceContext, enemy, base.DynamicVars.Damage, this);

                VfxCmd.PlayOnCreature(enemy, vfxpath);
            }
        }
    }
    protected override void OnUpgrade()
    {
       base.EnergyCost.UpgradeBy(-1);
    }


}