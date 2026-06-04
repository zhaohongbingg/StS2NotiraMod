using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Notira.Notira.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Notira.Notira.Powers;

 

public sealed class KisainotePower : NotiraPower
{

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (Owner.CurrentHp != Owner.MaxHp)
        {   decimal hp= Owner.MaxHp - Owner.CurrentHp;
          
            await CreatureCmd.Heal(this.Owner,hp);

        }
        await PowerCmd.TickDownDuration(this);
    }
}
