using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
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
using System.Threading.Tasks;

namespace Notira.Notira.Cards;

public class NimiSora() : NotiraCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self) {

    public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
{
      CardKeyword.Exhaust
};
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
 {
       new DynamicVar("XPoint", 5m),
       new PowerVar<BufferPower>(1)


 };
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<XPoint>(),
   HoverTipFactory.FromPower<BufferPower>(),


];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<XPoint>(choiceContext, Owner.Creature, DynamicVars["XPoint"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<BufferPower>(choiceContext, base.Owner.Creature, base.DynamicVars["BufferPower"].BaseValue, base.Owner.Creature, this);
    }
    protected override void OnUpgrade()
    {
        base.DynamicVars["XPoint"].UpgradeValueBy(5m);
     
    }


}