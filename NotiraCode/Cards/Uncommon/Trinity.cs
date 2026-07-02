using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using Notira.Notira.Cards;
using Notira.Notira.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;

public class Trinity() : NotiraCard(
    3, CardType.Skill, CardRarity.Rare, TargetType.Self)

{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DynamicVar("FREE_ATTACK",1),
        new DynamicVar("FREE_POWER",1),
        new DynamicVar("FREE_SKILL",1)
        
        

    };
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
         
        await PowerCmd.Apply<FreeAttackPower>(choiceContext, Owner.Creature, DynamicVars["FREE_ATTACK"].BaseValue, Owner.Creature, this, false);
        await PowerCmd.Apply<FreeSkillPower>(choiceContext, Owner.Creature, DynamicVars["FREE_SKILL"].BaseValue, Owner.Creature, this, false);
        await PowerCmd.Apply<FreePowerPower>(choiceContext, Owner.Creature, DynamicVars["FREE_POWER"].BaseValue, Owner.Creature, this, false);


    }
    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);   
    }
}