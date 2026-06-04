using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
 
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
 

namespace Notira.Notira.Cards;

 
public class Ever17() : NotiraCard(6, CardType.Skill, CardRarity.Rare, TargetType.AllEnemies)
{

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (base.Owner.Creature.Player.RunState.CurrentRoom.RoomType != RoomType.Boss)
        {
            foreach (Creature hittableEnemy in base.CombatState.HittableEnemies)
            {

                await CreatureCmd.Escape(hittableEnemy);
            }

        }
       
    }
   
    protected override void OnUpgrade()
    {
       base.AddKeyword(CardKeyword.Retain);
    }
}