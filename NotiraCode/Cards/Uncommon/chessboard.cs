using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Keywords;
using System.Formats.Asn1;

namespace Notira.Notira.Cards;


public class Chessboard() : NotiraCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{

    public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
{
        CardKeyword.Exhaust,
        NotiraKeyWords.rebellious
};

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {    var HPdamage =Owner.Creature.CurrentHp;

        await CreatureCmd.Heal(Owner.Creature, Owner.Creature.MaxHp - Owner.Creature.CurrentHp);
        await CreatureCmd.Damage(choiceContext,
            base.Owner.Creature,
            HPdamage,
            ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move,
      this);
    }
    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}





    




