using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
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
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Cards;
using Notira.Notira.Powers;
using System.Threading.Tasks;

namespace Notira.Notira.Powers;



public sealed class LibPower : NotiraPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    private class Data
    {
        public readonly Dictionary<CardModel, int> amountsForPlayedCards = new Dictionary<CardModel, int>();
    }
    protected override object InitInternalData()
    {
        return new Data();
    }
    public override Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature != base.Owner)
        {
            return Task.CompletedTask;
        }
        GetInternalData<Data>().amountsForPlayedCards.Add(cardPlay.Card, base.Amount);
        return Task.CompletedTask;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature == base.Owner && GetInternalData<Data>().amountsForPlayedCards.Remove(cardPlay.Card, out var value) && value > 0)
        {
            await CreatureCmd.GainBlock(base.Owner, value * 2, ValueProp.Unpowered, null, fast: true);
        }
    }

}


