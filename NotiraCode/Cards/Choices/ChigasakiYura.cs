using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Cards;
using Notira.Notira.Keywords;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;


public sealed class ChigasakiYra: BitterChoiceOptionCard
{
    public ChigasakiYra() : base(0, CardType.Skill, CardRarity.Token, TargetType.Self) { }
    public override bool CanBeGeneratedInCombat => false;
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] {
        new DynamicVar("MindRotPower",1),
        new EnergyVar(2)
    };
 


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
        await PowerCmd.Apply<MindRotPower>(choiceContext, Owner.Creature, DynamicVars["MindRotPower"].BaseValue, Owner.Creature, this);
         

    }
    int count = 0;
    public override async Task AfterEnergyReset(Player player)
    {        count++;
          if (count >= 2)
        {  await PowerCmd.Remove<MindRotPower>(Owner.Creature);
            count = 0;
        }
    }



    protected override void OnUpgrade()
    {
        base.AddKeyword(CardKeyword.Retain);
    }

}