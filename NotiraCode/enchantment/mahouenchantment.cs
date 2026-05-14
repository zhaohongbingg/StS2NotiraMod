using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Enchantments;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.ValueProps;
namespace Notira.Notira.enchantment;
public class MahouEnchantment: CustomEnchantmentModel
{
 // 是否在卡牌上显示数值
    public override bool ShowAmount => true;

// 重载这个以改变显示的数字
// public override int DisplayAmount => DynamicVars.Cards.IntValue;

// 是否会添加额外的卡牌描述文本
public override bool HasExtraCardText => true;

// 像卡牌、遗物、药水等一样，可以使用DynamicVars和ExtraHoverTips
//protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(2)];
//protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromKeyword(CardKeyword.Retain)];

// 图标位置。大小1:1就行，原版是64x64
protected override string? CustomIconPath => "res://Notira/Images/enchantments/mahou_enchantments.png";

// 决定是否可以附魔到某张卡牌上， 。
public override bool CanEnchant(CardModel card)
{

        if (base.CanEnchant(card) && !card.Keywords.Contains(CardKeyword.Unplayable) && !card.EnergyCost.CostsX)
        {
            return card.EnergyCost.GetWithModifiers(CostModifiers.None) > 0;
        }
        return false;
    }

// 当附魔被应用时调用，这里我们给卡牌添加保留。
protected override void OnEnchant()
{
        base.Card.EnergyCost.SetCustomBaseCost(0);
}
 


}

 