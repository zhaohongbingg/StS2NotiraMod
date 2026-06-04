using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;





public class Eden() : NotiraCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new PowerVar<DexterityPower>(2)
    };
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<DexterityPower>()

];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        List<PowerModel> originalDebuffs = (from p in base.Owner.Creature.Powers                                        
                                            select (PowerModel)p.ClonePreservingMutability()).ToList();
        
        foreach (PowerModel item in originalDebuffs)
        {
            await PowerCmd.Apply<DexterityPower>(Owner.Creature,base.DynamicVars.Dexterity.BaseValue, Owner.Creature, null);
        }
       
    }
 



    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}