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
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Cards;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;




public sealed class KamimahoDayisiki: BitterChoiceOptionCard
{

    public KamimahoDayisiki() : base(0, CardType.Skill, CardRarity.Token, TargetType.Self)
    { }
    public override bool CanBeGeneratedInCombat => false;
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];


    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]{
        new DynamicVar("XPoint",5m)

    };
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
  


];



    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int count = 0;
        foreach (CardModel item in await CardSelectCmd.FromHand(prefs: new CardSelectorPrefs(base.SelectionScreenPrompt, 0, 999999999), context: choiceContext, player: base.Owner, filter: null, source: this))
        {
            await CardCmd.Exhaust(choiceContext, item);
            count++;

        }
        await PowerCmd.Apply<XPoint>(choiceContext, base.Owner.Creature, DynamicVars["XPoint"].BaseValue * count, base.Owner.Creature, this);


    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["XPoint"].UpgradeValueBy(5m);


    }
}