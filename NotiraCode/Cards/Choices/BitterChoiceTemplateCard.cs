using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using Notira.Notira.Cards;
using Notira.Notira.Characters;


public abstract class BitterChoiceTemplateCard<TLeft, TRight> : BitterChoiceCardBase where TLeft : CardModel where TRight : CardModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    new IHoverTip[]
    {
        CreatePreviewHoverTip<TLeft>(),
        CreatePreviewHoverTip<TRight>()
    };

    protected BitterChoiceTemplateCard(int baseCost, CardType type, CardRarity rarity, TargetType target, bool showInCardLibrary = true)
        : base(baseCost, type, rarity, target, showInCardLibrary) { }
    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var left = CreateChoiceCardWithUpgrade<TLeft>();
        var right = CreateChoiceCardWithUpgrade<TRight>();
        return PresentChoiceCards(
            choiceContext,
            left,
            right

        );
    }

    protected override void OnUpgrade()
    {
        // 可选：以后扩展
    }
    protected CardModel CreateChoiceCardWithUpgrade<TCard>() where TCard : CardModel
    {
        var card = CreateChoiceCard<TCard>();

        if (IsUpgraded && card.IsUpgradable)
        {
            card.UpgradeInternal();
        }

        return card;
    }

    private IHoverTip CreatePreviewHoverTip<TCard>() where TCard : CardModel
    {
        var val = ModelDb.Card<TCard>().ToMutable();

        if (IsUpgraded && val.IsUpgradable)
        {
            val.UpgradeInternal();
            val.UpgradePreviewType = CardUpgradePreviewType.Deck;
        }

        return new CardHoverTip(val);
    }
}
