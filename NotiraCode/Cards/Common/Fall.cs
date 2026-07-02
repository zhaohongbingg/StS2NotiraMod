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
using Notira.Notira.Keywords;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Godot.OpenXRCompositionLayer;

namespace Notira.Notira.Cards;





public class Fall() : NotiraCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
         new DynamicVar("XPoint",10m)


    };
    public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
    {
       
        NotiraKeyWords.Gungun
    };
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        CardModel cardModel = (await CardSelectCmd.FromHand(prefs: new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1), context: choiceContext, player: base.Owner, filter: null, source: this)).FirstOrDefault();
        if (cardModel != null)
        {
            await CardCmd.Exhaust(choiceContext, cardModel);
        }
        await PowerCmd.Apply<XPoint>(choiceContext, base.Owner.Creature, DynamicVars["XPoint"].BaseValue, base.Owner.Creature, this, false);

    }
  
    protected override void OnUpgrade()
    {

        base.DynamicVars["XPoint"].UpgradeValueBy(5m);
    }

}
