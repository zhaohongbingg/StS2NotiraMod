using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Rooms;
using System.Threading.Tasks;


namespace Notira.Notira.Relics;



public class NotiraSuperior : NotiraRelics

{
    public override RelicRarity Rarity => RelicRarity.Starter;

    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        if (room is CombatRoom)
        {
            Flash();
            await PowerCmd.Apply<Notira.Powers.XPoint>(  null,Owner.Creature, 50m, Owner.Creature, null);
            await PowerCmd.Apply<Notira.Powers.KichikuPower>(  null,Owner.Creature, 3m, Owner.Creature, null);
        }

    }






}




