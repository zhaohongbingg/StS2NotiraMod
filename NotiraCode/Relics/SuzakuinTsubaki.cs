using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using System.Threading.Tasks;


namespace Notira.Notira.Relics;


public class SuzakuinTsubaki : NotiraRelics
{
    public override RelicRarity Rarity => RelicRarity.Ancient;
    private CardModel? _cardBeingPlayed;
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DynamicVar("Turns", 4m),
        new BlockVar(10m, ValueProp.Unpowered)
    };

    private CardModel? CardBeingPlayed
    {
        get
        {
            return _cardBeingPlayed;
        }
        set
        {
            AssertMutable();
            _cardBeingPlayed = value;
        }
    }
    public override int ModifyCardPlayCount(CardModel card, Creature? target, int playCount)
    {
        int num = CombatManager.Instance.History.CardPlaysFinished
            .Count(e => e.HappenedThisTurn(base.Owner.Creature.CombatState));
        if ((decimal)base.Owner.Creature.CombatState.RoundNumber <= base.DynamicVars["Turns"].BaseValue)
        {
            if (num == 0)
            {
                if (card.Type == CardType.Attack)
                {
                    return playCount + 1;
                }
                else
                {
                    CreatureCmd.GainBlock(base.Owner.Creature, DynamicVars.Block, null);
                }
            }
         
        }
        return playCount;
    }
}