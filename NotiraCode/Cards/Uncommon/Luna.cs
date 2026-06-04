using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Formats.Asn1;

namespace Notira.Notira.Cards;


public class Luna() : NotiraCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{



    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
 {  new BlockVar(20,ValueProp.Move),
 
 };
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Creature creature = base.Owner.Creature;
        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
        await PowerCmd.Apply<BlockNextTurnPower>(creature, creature.Block, creature, this);
        SfxHelper.Play("res://Notira/music/luna.ogg");


    }
    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
         
    }


};



