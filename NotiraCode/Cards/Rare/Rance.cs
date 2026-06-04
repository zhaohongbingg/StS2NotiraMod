using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using Notira.Notira.Cards;
using Notira.Notira.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;


public class  Rance() : NotiraCard(
    3, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    new DynamicVar[]
    {
        new DynamicVar("Kichiku",20),
        new DynamicVar("Rance",10)
    };
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<RancePower>()

];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<KichikuPower>(Owner.Creature, DynamicVars["Kichiku"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<RancePower>(Owner.Creature, DynamicVars["Rance"].BaseValue, Owner.Creature, this);

    }
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
        
    }

}

