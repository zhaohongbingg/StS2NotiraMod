using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;
using System.Threading.Tasks;


namespace Notira.Notira.Relics;



public class NotiraNewbie : NotiraRelics
 
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        if(room is CombatRoom)
        {
             Flash();
            await PowerCmd.Apply<Notira.Powers.XPoint>(Owner.Creature, 10m, Owner.Creature, null);
            await PowerCmd.Apply<Notira.Powers.KichikuPower>(Owner.Creature, 1m, Owner.Creature, null);
            
            
        }

    }
    public override RelicModel? GetUpgradeReplacement() => ModelDb.Relic<NotiraSuperior>();






}

     


