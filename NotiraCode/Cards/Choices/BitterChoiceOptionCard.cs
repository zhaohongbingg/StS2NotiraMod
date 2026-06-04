using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Notira.Notira.Cards;
using Notira.Notira.Characters;

public abstract class BitterChoiceOptionCard : NotiraCard
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { (CardKeyword)1 };

    protected BitterChoiceOptionCard(int baseCost, CardType type, CardRarity rarity, TargetType target)
        : base(baseCost, type, rarity, target)
    {
    }
}

