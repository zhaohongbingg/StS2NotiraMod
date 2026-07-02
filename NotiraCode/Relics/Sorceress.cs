using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.ValueProps;


namespace Notira.Notira.Relics;


public class Sorceress : NotiraRelics

{
    public override RelicRarity Rarity => RelicRarity.Ancient;
    private int firstRoundTotalDamage = 0;




    public override async Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side == CombatSide.Player && base.Owner.Creature.Player.Creature.CombatState.RoundNumber == 1)
        {
          
                
            await CreatureCmd.Damage(choiceContext, base.Owner.Creature.CombatState.HittableEnemies,new DamageVar(firstRoundTotalDamage,ValueProp.Unpowered), base.Owner.Creature);
            firstRoundTotalDamage = 0;
        }
    }
    public override Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature dealer, DamageResult result, ValueProp props, Creature target, CardModel cardSource)
    {
          firstRoundTotalDamage += result.UnblockedDamage+result.OverkillDamage+result.BlockedDamage;
        return Task.CompletedTask;
    }
  
   

    
}