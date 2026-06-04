using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using System.Threading.Tasks;


namespace Notira.Notira.Relics;






public class FataMorgananoYakata : NotiraRelics

{
    public override RelicRarity Rarity => RelicRarity.Ancient;
    private bool _usedThisCombat;
    private bool _shouldTakeExtraTurn;
    private bool UsedThisCombat
    {
        get
        {
            return _usedThisCombat;
        }
        set
        {
            AssertMutable();
            _usedThisCombat = value;
        }
    }
    public override Task AfterTakingExtraTurn(Player player)
    {
        if (player != base.Owner)
        {
            return Task.CompletedTask;
        }


        UsedThisCombat = true;
        _shouldTakeExtraTurn = false;
        return Task.CompletedTask;
    }
    public override Task AfterCombatEnd(CombatRoom _)
    {

        UsedThisCombat = false;
        _shouldTakeExtraTurn = false;
        return Task.CompletedTask;
    }
    public override bool ShouldTakeExtraTurn(Player player)
    {
        return player == base.Owner && _shouldTakeExtraTurn && !UsedThisCombat;
    }
    public override Task   AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == base.Owner.Creature.Side
        && base.Owner.Creature.Player.Creature.CombatState.RoundNumber == 1  // 只检测第一回合
        && !UsedThisCombat)
        {
            _shouldTakeExtraTurn = true;

        }
        return Task.CompletedTask;
    }


}