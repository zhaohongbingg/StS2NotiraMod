using BaseLib.Extensions;
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
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Cards;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;








public sealed class RupekariDead : BitterChoiceOptionCard
{

    public RupekariDead() : base(0, CardType.Attack, CardRarity.Token, TargetType.AllEnemies)
    { }

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    public override bool CanBeGeneratedInCombat => false;
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]{
       new DamageVar(10,ValueProp.Move),
       new RepeatVar(2)
        };


    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
            



];



    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await DamageCmd.Attack(base.DynamicVars.Damage.IntValue)
                 .WithHitCount(base.DynamicVars.Repeat.IntValue).FromCard(this)
                 .TargetingAllOpponents(CombatState)
                 .Execute(choiceContext);
        CardModel card = base.CombatState.CreateCard<Burn>(base.Owner);
        CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand,base.Owner ));
       


    }

    protected override void OnUpgrade()
    {
          base.DynamicVars.Repeat.UpgradeValueBy(1);



    }
}
