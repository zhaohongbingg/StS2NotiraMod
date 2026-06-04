using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Notira.Notira.Cards;
using Notira.Notira.Characters;

public abstract class BitterChoiceCardBase : NotiraCard
{
    protected BitterChoiceCardBase(int cost, CardType type, CardRarity rarity, TargetType target, bool showInCardLibrary = true)
        : base(cost, type, rarity, target, showInCardLibrary)
    {
    }

    protected T CreateChoiceCard<T>() where T : CardModel
    {
        var val = CardScope.CreateCard<T>(Owner);

        if (val.IsUpgraded && val.IsUpgradable)
        {
            val.UpgradeInternal();
            val.FinalizeUpgradeInternal();
        }

        return val;
    }

    protected async Task<CardModel> ChooseOne(PlayerChoiceContext ctx, params CardModel[] choices)
    {
        return await CardSelectCmd.FromChooseACardScreen(
            ctx,
            choices.ToList(),
            Owner,
            false
        );
    }

    protected async Task AddToHand(CardModel card)
    {
        card.SetToFreeThisTurn();

        await CardPileCmd.AddGeneratedCardToCombat(
            card,
            PileType.Hand,
            true,
            CardPilePosition.Random
        );
    }
    protected async Task PresentChoiceCards(
    PlayerChoiceContext choiceContext,
    CardModel firstChoice,
    CardModel secondChoice)
    {
        var selected = await CardSelectCmd.FromChooseACardScreen(
            choiceContext,
            new List<CardModel> { firstChoice, secondChoice },
            Owner,
            false
        );

        if (selected != null)
        {
       

            selected.SetToFreeThisTurn();

            await CardPileCmd.AddGeneratedCardToCombat(
                selected,
                PileType.Hand,
                true,
                CardPilePosition.Random
            );
        }
    }
}