using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Cards;
using Notira.Notira.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;


public sealed class Cutthroat() : NotiraCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.AllAllies)
{
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
      {
       new DynamicVar("Kichiku",2)


      };
    public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
{
        CardKeyword.Exhaust

};
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
  HoverTipFactory.FromPower<KichikuPower>()

];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        IEnumerable<Creature> enumerable = from c in base.CombatState.GetTeammatesOf(base.Owner.Creature)
                                           where c != null && c.IsAlive && c.IsPlayer
                                           select c;
        foreach (Creature item in enumerable)
        {
            await PowerCmd.Apply<KichikuPower>(choiceContext, item, base.DynamicVars["Kichiku"].BaseValue, base.Owner.Creature, this);
        }
    }
    protected override void OnUpgrade()
    {

        RemoveKeyword(CardKeyword.Exhaust);
    }
}