using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;


namespace Notira.Notira.Relics;





public class HoukagoCinderella : NotiraRelics

{
    public override RelicRarity Rarity => RelicRarity.Ancient;
    public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != base.Owner.Creature.Side) return;

        int cardsPlayed = 0;
        const int targetCount = 5;

        while (cardsPlayed < targetCount)
        {
            // 每次循环重新获取当前手牌（因为打出牌后手牌会变化）
            CardPile hand = PileType.Hand.GetPile(base.Owner.Creature.Player);
            List<CardModel> playableCards = hand.Cards
                .Where(c => !c.Keywords.Contains(CardKeyword.Unplayable))
                .ToList();

            if (playableCards.Count == 0) break; // 无可打出的牌，终止

            // 随机选择一张可打出的牌
            CardModel cardToPlay = base.Owner.Creature.Player.RunState.Rng.Shuffle.NextItem(playableCards);

            // 自动打出，等待完成
            await CardCmd.AutoPlay(choiceContext, cardToPlay, null);

            cardsPlayed++;
        }
    }

}
