using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Cards;
using Notira.Notira.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;




public class SakuraMoyu() : NotiraCard(
    2, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    public override bool GainsBlock => true;
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DynamicVar("XPoint",20m),

        new DynamicVar("SakuraMoyo", 18m)


    };
    private bool isUsed = false;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<XPoint>(),
   HoverTipFactory.FromPower<SakuraMoyoPower>()

];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {

         
        await PowerCmd.Apply<XPoint>(choiceContext, Owner.Creature, DynamicVars["XPoint"].BaseValue, Owner.Creature, this, false);
        await PowerCmd.Apply<SakuraMoyoPower>(choiceContext, Owner.Creature, DynamicVars["SakuraMoyo"].BaseValue, Owner.Creature, this, false);
       

    }

    protected override void OnUpgrade()
    {
        DynamicVars["XPoint"].UpgradeValueBy(10m);
        
    }
}