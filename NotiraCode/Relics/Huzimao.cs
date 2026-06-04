using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Map;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Runs.History;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Powers;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Notira.Notira.Relics;



 
public class Huzimao : NotiraRelics
{

    public override RelicRarity Rarity => RelicRarity.Ancient;
    private int eventRoomVisitCount = 0;
    private int nextChestAfter = 2;
    public override bool IsAllowed(IRunState runState)
    {
        return RelicModel.IsBeforeAct3TreasureChest(runState);
    }
    public override IReadOnlySet<RoomType> ModifyUnknownMapPointRoomTypes(IReadOnlySet<RoomType> roomTypes)
    {
        HashSet<RoomType> hashSet = new HashSet<RoomType>();
        foreach (RoomType roomType in roomTypes)
        {
            hashSet.Add(roomType);
        }
        HashSet<RoomType> hashSet2 = hashSet;
        hashSet2.Remove(RoomType.Monster);
        hashSet2.Remove(RoomType.Shop);      
        hashSet2.Remove(RoomType.Elite);
        hashSet2.Remove(RoomType.Boss);
        hashSet2.Remove(RoomType.Event);

        return hashSet2;
    }

}