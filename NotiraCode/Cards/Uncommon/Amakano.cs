using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;



public class Amakano() : NotiraCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new BlockVar(6,ValueProp.Move),


    };
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<XPoint>()

];
    protected override async   Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (base.Owner.Creature.GetPowerAmount<XPoint>() > 50)
        {
            for (int i = 0; i < 3; i++)
            {
                await  CreatureCmd.GainBlock(base.Owner.Creature, DynamicVars.Block, cardPlay);
            }
        }
        else
        {
            await CreatureCmd.GainBlock(base.Owner.Creature, DynamicVars.Block, cardPlay);
        }
    }
    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3);

    }
}
