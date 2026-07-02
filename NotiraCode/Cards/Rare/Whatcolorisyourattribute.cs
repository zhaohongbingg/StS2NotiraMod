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
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;


public class Whatcolorisyourattribute() : NotiraCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies)
{

    public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[] { CardKeyword.Ethereal };
    protected override bool IsPlayable => base.Owner.Creature.GetPowerAmount<XPoint>() >= 20;
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        foreach (CardModel item in await CardSelectCmd.FromHand(choiceContext, base.Owner, new CardSelectorPrefs(base.SelectionScreenPrompt, 1), null, this))
        {
           
            item.BaseReplayCount++;
        }
        await PowerCmd.Apply<XPoint>(choiceContext, Owner.Creature, -20m, Owner.Creature, this, false);
    }
    
    protected override void OnUpgrade()
    {
        base.RemoveKeyword(CardKeyword.Ethereal);
    }


}