using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using Notira.Notira.Powers;
using System.Threading.Tasks;

namespace Notira.Notira.Powers;


public sealed class RoaringTidesPower : NotiraPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPlayerTurnStartLate(PlayerChoiceContext choiceContext, Player player)
    {
       
        foreach (Creature enemy in base.CombatState.HittableEnemies)
        {
            if (enemy == null || enemy.IsDead)
                continue;
            await CreatureCmd.Stun(enemy);

        }
        await PowerCmd.TickDownDuration(this);

    }
  
}
