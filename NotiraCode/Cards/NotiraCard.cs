using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Logging;
using Notira.Notira.Extensions;
using Notira.Notira.Characters;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Notira.Notira.Cards;

[Pool(typeof(NotiraCardPool))]
public abstract class NotiraCard : CustomCardModel {
    protected NotiraCard(int cost, CardType type, CardRarity rarity, TargetType target, bool showInCardLibrary = true)
           : base(cost, type, rarity, target, showInCardLibrary)
    {
    }

    //Image size:
    //Normal art: 1000x760 (Using 500x380 should also work, it will simply be scaled.)
    //Full art: 606x852
    public override string CustomPortraitPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
            Log.Info(">>>[NotiraMod]CardPath=" + path, 2);
            return ResourceLoader.Exists(path) ? path : "card.png".CardImagePath();
        }
    }

    //Smaller variants of card Images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190

    //Uses card_portraits/card_name.png as image path. These should be smaller Images.
    public override string PortraitPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
            Log.Info(">>>[SelphinaMod]CardPath=" + path, 2);
            return ResourceLoader.Exists(path) ? path : "card.png".CardImagePath();
        }
    }

    //Optional and I'm not sure it's functional yet.
    public override string BetaPortraitPath
    {
        get
        {
            var path = $"Beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
            Log.Info(">>>[SelphinaMod]CardPath=" + path, 2);
            return ResourceLoader.Exists(path) ? path : "Beta/card.png".CardImagePath();
        }
    }
 
  /*  public override Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
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
    }*/
}

