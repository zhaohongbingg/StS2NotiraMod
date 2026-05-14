using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Nodes;
using Notira.Notira.enchantment;



namespace Notira.Notira.Relics;




public class Mahou : NotiraRelics
{

    public override RelicRarity Rarity => RelicRarity.Ancient;
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
        {
          new CardsVar(1)

        };
    protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromEnchantment<MahouEnchantment>();

    public override async Task AfterObtained()
    {
        foreach (CardModel item in await CardSelectCmd.FromDeckForEnchantment(
            prefs: new CardSelectorPrefs(base.SelectionScreenPrompt, base.DynamicVars.Cards.IntValue),
            player: base.Owner, enchantment: ModelDb.Enchantment<MahouEnchantment>(), amount: 1))
        {
            CardCmd.Enchant<MahouEnchantment>(item, 1m);
 

        }
    }
}
