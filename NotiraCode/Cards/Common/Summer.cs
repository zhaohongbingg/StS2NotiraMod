using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Keywords;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;





public class Summer() : NotiraCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new EnergyVar(1)
    };



    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
    {
        
        NotiraKeyWords.Gungun
    };
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);

    }
    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
    {
        CardPile? pile = base.Pile;
        if (pile != null && pile.Type == PileType.Exhaust && player == base.Owner)
        {
            await CardCmd.AutoPlay(choiceContext, this, null);
        }
    }
    protected override void OnUpgrade()
    {
        base.DynamicVars.Energy.UpgradeValueBy(1);

    }

}
