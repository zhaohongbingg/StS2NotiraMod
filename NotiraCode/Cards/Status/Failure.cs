using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;

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

   
}
