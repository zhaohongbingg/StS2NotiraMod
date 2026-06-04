using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Achievements;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Cards;
using Notira.Notira.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;



public class NienoHakoniwa() : NotiraCard(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DamageVar(6,ValueProp.Move),

    };
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {

        List<PowerModel> originalDebuffs = (from p in cardPlay.Target.Powers
                                            
                                            select (PowerModel)p.ClonePreservingMutability()).ToList();
        decimal count = 1;
        foreach (PowerModel item in originalDebuffs)
        {
            count += item.Amount;
        }
        for(int i = 0; i < count; i++)
        {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
       .FromCard(this)
       .TargetingAllOpponents(CombatState)
       .WithHitFx("vfx/vfx_giant_horizontal_slash")
       .Execute(choiceContext);
        }

    }
    protected override void OnUpgrade()
    {
       base.EnergyCost.UpgradeBy(-1);

    }
}