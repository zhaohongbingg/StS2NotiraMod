using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Saves.Runs;
using Notira.Notira.Cards;

namespace Notira.Notira.Modifiers;

public class CardPackSelectionModifier : ModifierModel
{
    private static readonly LocString _title = new("modifiers", "CARD_PACK_SELECTION.title");
    private static readonly LocString _description = new("modifiers", "CARD_PACK_SELECTION.description");
    private static readonly LocString _neowTitle = new("modifiers", "CARD_PACK_SELECTION.neow_title");
    private static readonly LocString _neowDescription = new("modifiers", "CARD_PACK_SELECTION.neow_description");
    private static readonly LocString _selectionPrompt = new("modifiers", "CARD_PACK_SELECTION.selection_prompt");

    private HashSet<ModelId>? _allowedCardIds;

    public override LocString Title => _title;
    public override LocString Description => _description;
    public override LocString NeowOptionTitle => _neowTitle;
    public override LocString NeowOptionDescription => _neowDescription;

    public override bool ClearsPlayerDeck => false;

    [SavedProperty]
    public List<string> SelectedPacks { get; set; } = new();

    private static string CharacterPrefix => "Character:";

    private static readonly string[] CharacterPoolNames =
    {
        "ironclad", "silent", "defect", "necrobinder", "regent",
    };

    private HashSet<ModelId> GetAllowedCardIds(Player player)
    {
        if (_allowedCardIds != null) return _allowedCardIds;

        _allowedCardIds = new HashSet<ModelId>();

        foreach (var packName in SelectedPacks)
        {
            if (packName.StartsWith(CharacterPrefix))
            {
                var charId = packName[CharacterPrefix.Length..];
                var charModel = ModelDb.GetByIdOrNull<CharacterModel>(new ModelId("character-model", charId));
                if (charModel != null)
                {
                    foreach (var card in charModel.CardPool.GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint))
                    {
                        _allowedCardIds.Add(card.Id);
                    }
                }
            }
            else
            {
                foreach (var card in CardPackRegistry.GetCardModelsInPacks(new[] { packName }))
                {
                    _allowedCardIds.Add(card.Id);
                }
            }
        }

        return _allowedCardIds;
    }

    public override CardCreationOptions ModifyCardRewardCreationOptions(Player player, CardCreationOptions options)
    {
        if (SelectedPacks == null || SelectedPacks.Count == 0)
            return options;

        var allowed = GetAllowedCardIds(player);
        var existingFilter = options.CardPoolFilter;

        return options.WithCardPools(
            options.CardPools.Count > 0 ? options.CardPools : new[] { player.Character.CardPool },
            (CardModel c) => allowed.Contains(c.Id) && (existingFilter == null || existingFilter(c)));
    }

    public override IEnumerable<CardModel> ModifyMerchantCardPool(Player player, IEnumerable<CardModel> options)
    {
        if (SelectedPacks == null || SelectedPacks.Count == 0)
            return options;

        var allowed = GetAllowedCardIds(player);
        return options.Where(c => allowed.Contains(c.Id));
    }

    public override Func<Task>? GenerateNeowOption(EventModel eventModel)
    {
        return () => ShowPackSelection(eventModel.Owner);
    }

    private async Task ShowPackSelection(Player player)
    {
        var allPacks = CardPackRegistry.AllPackNames;
        if (allPacks.Count == 0) return;

        var pickCount = Math.Min(3, allPacks.Count + CharacterPoolNames.Length);

        var cardSelectorPrefs = new CardSelectorPrefs(_selectionPrompt, pickCount);

        var packCardSample = new List<(string PackName, CardModel SampleCard)>();

        foreach (var packName in allPacks)
        {
            var sample = CardPackRegistry.GetCardModelsInPacks(new[] { packName }).FirstOrDefault();
            if (sample != null)
                packCardSample.Add((packName, sample));
        }

        foreach (var charId in CharacterPoolNames)
        {
            var charModel = ModelDb.GetByIdOrNull<CharacterModel>(new ModelId("character-model", charId));
            if (charModel == null) continue;

            var sample = charModel.CardPool.AllCards
                .FirstOrDefault(c => c.Rarity == CardRarity.Basic || c.Rarity == CardRarity.Common);
            if (sample != null)
            {
                var displayName = charId.First().ToString().ToUpper() + charId[1..];
                packCardSample.Add((CharacterPrefix + charId, sample));
            }
        }

        if (packCardSample.Count == 0) return;

        var selectedResults = (await CardSelectCmd.FromSimpleGridForRewards(
            new BlockingPlayerChoiceContext(),
            packCardSample.Select(x => new CardCreationResult(x.SampleCard)).ToList(),
            player,
            cardSelectorPrefs)).ToList();

        var selectedPackNames = selectedResults
            .Select(r => packCardSample.FirstOrDefault(x => x.SampleCard.Id == r.Id).PackName)
            .Where(name => name != null)
            .ToList();

        SelectedPacks = selectedPackNames;
        _allowedCardIds = null;

        foreach (var p in player.RunState.Players)
        {
            foreach (var m in p.RunState.Modifiers)
            {
                if (m is CardPackSelectionModifier mod)
                {
                    mod.SelectedPacks = selectedPackNames;
                    mod._allowedCardIds = null;
                }
            }
        }

        var packSummary = string.Join(", ", selectedPackNames);
        MegaCrit.Sts2.Core.Logging.Log.Info($"[Notira] Selected packs: {packSummary}");
    }
}
