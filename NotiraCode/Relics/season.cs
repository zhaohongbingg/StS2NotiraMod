using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Rewards;

using MegaCrit.Sts2.Core.Runs;
using System.Threading.Tasks;


namespace Notira.Notira.Relics;




public class Season : NotiraRelics

{
    public override RelicRarity Rarity => RelicRarity.Ancient;

 protected override IEnumerable<DynamicVar> CanonicalVars =>  new DynamicVar[]
 {
    new CardsVar(3)
 };
    public override async Task AfterObtained()
    {
        CharacterModel character = base.Owner.Character;
        // 获取所有已解锁的卡池（包括 mod 角色）
        List<CardPoolModel> allUnlockedPools = base.Owner.UnlockState.CharacterCardPools.ToList();

        // 筛选出其他角色的卡池（排除当前角色）
        var otherPools = allUnlockedPools.Where(pool => pool != character.CardPool).ToList();
        List<CardPoolModel> List = otherPools;
        int num = Mathf.Min(4, otherPools.Count);
        while (List.Count > num)
        {
            List.RemoveAt(base.Owner.PlayerRng.Shops.NextInt(List.Count));
        }



        var rewards = new List<Reward>();



        foreach (var pool in List)
        {
            CardCreationOptions options = new CardCreationOptions(
                new[] { pool },
                CardCreationSource.Other,
                CardRarityOddsType.Uniform,
                (CardModel c) => c.Rarity == CardRarity.Rare
            ).WithFlags(CardCreationFlags.NoRarityModification);

            rewards.Add(new CardReward(options, 3, base.Owner));
        }

        await RewardsCmd.OfferCustom(base.Owner, rewards);
    }
   
}


