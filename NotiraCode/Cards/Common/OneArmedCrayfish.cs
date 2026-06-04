using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Keywords;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;



public class  oneArmedCrayfish() : NotiraCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{

    public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
  {
       
        NotiraKeyWords.rebellious
  };
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<KichikuPower>(),
   HoverTipFactory.FromPower<XPoint>()

];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var a = base.Owner.Creature.GetPowerAmount<XPoint>();
        var b = base.Owner.Creature.GetPowerAmount<KichikuPower>();

        await PowerCmd.Remove<XPoint>(base.Owner.Creature);
        await PowerCmd.Remove<KichikuPower>(base.Owner.Creature);

        await PowerCmd.Apply<XPoint>(choiceContext, Owner.Creature, new DynamicVar("XPiont", b).BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<KichikuPower>(choiceContext, Owner.Creature, new DynamicVar("KichikuPower", a).BaseValue, Owner.Creature, this);


    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);


    }
}
