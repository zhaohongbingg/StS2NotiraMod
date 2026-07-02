using System.Collections.Generic;
using System.Threading.Tasks;
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
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;


namespace Notira.Notira.Relics;


public class Mangekyoui : NotiraRelics
{
    public override RelicRarity Rarity => RelicRarity.Ancient;
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
{
        new DynamicVar("Turns", 6m),
        
};
    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (side == base.Owner.Creature.Side)
        {
            if ((decimal)combatState.RoundNumber <= base.DynamicVars["Turns"].BaseValue)
            {
                if (combatState.RoundNumber % 2 == 0)
                {
                    await PowerCmd.Apply<AnticipatePower>(choiceContext, base.Owner.Creature, 6, base.Owner.Creature, null, false);

                }
                else
                {
                    await PowerCmd.Apply<SetupStrikePower>(choiceContext, base.Owner.Creature, 6, base.Owner.Creature, null, false);

                }
            }

        }
    }
    
}
