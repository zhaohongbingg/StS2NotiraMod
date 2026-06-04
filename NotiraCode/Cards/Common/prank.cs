using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
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


public class Prank() : NotiraCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    private int NextEnergyCost()
    {
        return base.Owner.RunState.Rng.CombatEnergyCosts.NextInt(4);
       

    }
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var selected = await CardSelectCmd.FromHand(
         choiceContext,
         base.Owner,
         new CardSelectorPrefs(base.SelectionScreenPrompt, 0, 999),
         null,
         this
     );
        foreach (CardModel item in selected) {
            if (item.EnergyCost.GetWithModifiers(CostModifiers.None) >= 0) {
                item.EnergyCost.SetThisTurnOrUntilPlayed(NextEnergyCost());
            }
        }



       
    }
    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);

    }

}