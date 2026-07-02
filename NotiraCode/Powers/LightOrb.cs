using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Random;
using Notira.Notira.Cards;
using Notira.Notira.Powers;
using System.Threading.Tasks;

namespace Notira.Notira.Powers;


public sealed class LightOrb : NotiraPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;





   
    public override async Task AfterPowerAmountChanged(PlayerChoiceContext context, PowerModel power, decimal amount, Creature applier, CardModel cardSource)
    {
        if (this.Amount >= 13)
        {

            var card = base.Owner.CombatState.CreateCard<ClChoices>(this.Owner.Player);

            var cards = new List<CardModel> { card };

            await CardPileCmd.AddGeneratedCardsToCombat(
                cards,
                PileType.Hand,
                base.Owner.Player,
                CardPilePosition.Random
            );
            await PowerCmd.Remove(this);
        }

    }
}


        
    












