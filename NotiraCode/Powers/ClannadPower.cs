using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Random;
using Notira.Notira.Characters;
using Notira.Notira.Keywords;
using Notira.Notira.Powers;
namespace Notira.Notira.Powers;


public sealed class ClannadPower : NotiraPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public bool isLightRrb()
    {
        return base.Owner.HasPower<LightOrb>() && base.Owner.GetPower<LightOrb>().Amount > 13;
    }

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
    {
        if (player != base.Owner.Player)
        {
            return;
        }
        IReadOnlyList<CardModel> readOnlyList =
   ModelDb.CardPool<NotiraCardPool>()
          .GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint)
          .Where(c => c.Rarity == CardRarity.Token && c.Keywords.Contains(NotiraKeyWords.CL))         
          .ToList();
        if (readOnlyList.Count > 0)
        {
            CardModel[] array = new CardModel[base.Amount];
            Rng combatCardGeneration = base.Owner.Player.RunState.Rng.CombatCardGeneration;
            for (int num = 0; num < base.Amount; num++)
            {
                CardCmd.ApplyKeyword(array[num] = CardFactory.GetDistinctForCombat(player, readOnlyList, 1, combatCardGeneration).FirstOrDefault(), CardKeyword.Exhaust);
            }
            Flash();
            await CardPileCmd.AddGeneratedCardsToCombat(array, PileType.Hand, addedByPlayer: true);
        }
    }
}