using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Keywords;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;





public class  Euphoria() : NotiraCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.AnyEnemy)
{     protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        List<PowerModel> originalDebuffs = (from p in cardPlay.Target.Powers
                                            where p.TypeForCurrentAmount == PowerType.Debuff
                                            select (PowerModel)p.ClonePreservingMutability()).ToList();
    
        foreach (PowerModel item in originalDebuffs)
        {
            PowerModel powerById = cardPlay.Target.GetPowerById(item.Id);
            if (powerById != null && powerById.InstanceType != PowerInstanceType.Instanced)
            {
                DoHackyThingsForSpecificPowers(powerById);
                await PowerCmd.ModifyAmount(choiceContext, powerById, item.Amount, base.Owner.Creature, this, false);
            }
            else
            {
                PowerModel power = (PowerModel)item.ClonePreservingMutability();
                DoHackyThingsForSpecificPowers(power);
                await PowerCmd.Apply(choiceContext, power, cardPlay.Target, item.Amount, base.Owner.Creature, this, false);
            }
        }
    }
    public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
{
        CardKeyword.Exhaust };
        



    private static void DoHackyThingsForSpecificPowers(PowerModel power)
    {
        if (power is ITemporaryPower temporaryPower)
        {
            temporaryPower.IgnoreNextInstance();
        }
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}