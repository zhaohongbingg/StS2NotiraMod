using BaseLib.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Potions;
using MegaCrit.Sts2.Core.Models.Powers;
using Notira.Notira.Cards;
using Notira.Notira.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;


public class Syusaku() : NotiraCard(
    1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DynamicVar("Kichiku",2m),
       new PowerVar<VulnerablePower>(2),
       new PowerVar<WeakPower>(2)

    };
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<KichikuPower>(),
   HoverTipFactory.FromPower<WeakPower>(),
   HoverTipFactory.FromPower<VulnerablePower>()


];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<KichikuPower>(choiceContext, Owner.Creature, DynamicVars["Kichiku"].BaseValue, Owner.Creature, this, false);
        foreach (Creature hittableEnemy in base.CombatState.HittableEnemies)
        {
            await PowerCmd.Apply<VulnerablePower>(choiceContext, hittableEnemy, base.DynamicVars.Vulnerable.BaseValue, Owner.Creature, this, false);
            await PowerCmd.Apply<WeakPower>(choiceContext, hittableEnemy, DynamicVars.Weak.BaseValue, Owner.Creature, this, false);
        }
    }
    protected override void OnUpgrade()
    {
        base.DynamicVars["Kichiku"].UpgradeValueBy(1);
        base.DynamicVars.Vulnerable.UpgradeValueBy(1);
        base.DynamicVars.Weak.UpgradeValueBy(1);



    }
}