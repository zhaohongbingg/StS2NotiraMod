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




public class Kagome() : NotiraCard(1, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
       new BlockVar(2,ValueProp.Move)

    };
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {

        List<PowerModel> originalDebuffs = (from p in cardPlay.Target.Powers
                                            where p.TypeForCurrentAmount == PowerType.Debuff
                                            select (PowerModel)p.ClonePreservingMutability()).ToList();
        decimal count = 0;
        foreach (PowerModel item in originalDebuffs)
        {
            count += item.Amount;
        }
        for (int i = 0; i < count; i++)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
        }


    }
    protected override void OnUpgrade()
    {
        base.DynamicVars.Block.UpgradeValueBy(2);
        

    }
}