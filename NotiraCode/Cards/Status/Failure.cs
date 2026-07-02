using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace Notira.Notira.Cards;


public sealed class Failure : NotiraCard
{
    public override int MaxUpgradeLevel => 0;
    private int _cardsInHand;


    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Unplayable];

    public override bool HasTurnEndInHandEffect => true;

    public Failure()
        : base(-1, CardType.Status, CardRarity.Status, TargetType.None)
    {
    }
    public override Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side != CombatSide.Player)
        {
            return Task.CompletedTask;
        }
        if (base.Pile.Type != PileType.Hand)
        {
            return Task.CompletedTask;
        }
        CardsInHand = base.Pile.Cards.Count;
        return Task.CompletedTask;
    }
    private int CardsInHand
    {
        get
        {
            return _cardsInHand;
        }
        set
        {
            AssertMutable();
            _cardsInHand = value;
        }
    }

    protected override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
    {
         
        
    }
}
