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



public class  Karehan() : NotiraCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AllEnemies)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("Sakura", 1m)];



    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        foreach (Creature hittableEnemy in base.CombatState.HittableEnemies)
        {
            await PowerCmd.Apply<SakuraPower>(hittableEnemy, DynamicVars["Sakura"].BaseValue, Owner.Creature, this);
        }
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<SakuraPower>()

];
    protected override void OnUpgrade()
    {
        DynamicVars["Sakura"].UpgradeValueBy(2m);
    }
}