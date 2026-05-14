using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
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


public class Atri():NotiraCard(1,CardType.Attack,CardRarity.Uncommon,TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DamageVar(10,ValueProp.Move),
         
    };
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) { 
     
        await CreatureCmd.LoseBlock(cardPlay.Target, cardPlay.Target.Block);
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_flying_slash", null, "blunt_attack.mp3")
                .Execute(choiceContext);

    }
    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(4);

    }
}