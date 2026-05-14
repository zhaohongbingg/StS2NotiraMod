
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using Notira.Notira.Cards;

using Notira.Notira.Keywords;
namespace Notira.Notira.Cards;






public sealed class Classic : BitterChoiceTemplateCard<ClassicCream,ClassicPuff>, ITranscendenceCard
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
    {
     
        NotiraKeyWords.Choice
    };
    public CardModel GetTranscendenceTransformedCard() => ModelDb.Card<Classic2>();

    public Classic() : base(1, CardType.Skill, CardRarity.Basic, TargetType.Self) { }
    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }





}