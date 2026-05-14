using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using Notira.Notira.Cards;
using Notira.Notira.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;

public class Notira0721():NotiraCard(
    1, CardType.Skill, CardRarity.Common, TargetType.Self)
{   protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DynamicVar("XPoint",10m),
        new CardsVar(2)
         
    };
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CardPileCmd.Draw(choiceContext,base.DynamicVars.Cards.IntValue,base.Owner);
        await PowerCmd.Apply<XPoint>(Owner.Creature,DynamicVars["XPoint"].BaseValue, Owner.Creature, this);

         
    }
    protected override void OnUpgrade()
    {
        DynamicVars["XPoint"].UpgradeValueBy(5m);
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}