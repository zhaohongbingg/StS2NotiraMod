
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using Notira.Notira.Cards;

using Notira.Notira.Keywords;
namespace Notira.Notira.Cards;

 
public sealed class WHITEALBUM2 : BitterChoiceTemplateCard<ToumaKazusa, OgisoSetsuna>
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
    {
     
        NotiraKeyWords.Choice
    };

  

    public WHITEALBUM2() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }
    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}
