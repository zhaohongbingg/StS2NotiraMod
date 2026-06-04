using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Keywords;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;



public class ChronoBox() : NotiraCard(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{


    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
{
        new DamageVar(6,ValueProp.Unpowered),


};
    public override IEnumerable<CardKeyword> CanonicalKeywords =>new CardKeyword[] { NotiraKeyWords.Gift };


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (base.Owner.HasPower<KichikuPower>())
        {
            int a=base.Owner.Creature.GetPowerAmount<KichikuPower>();
            AttackCommand attackCommand = await DamageCmd.Attack(base.DynamicVars.Damage.IntValue)
                .WithHitCount(a).FromCard(this)
                .Targeting(cardPlay.Target)
                .Execute(choiceContext);
            int unblockedHits = attackCommand.Results
             .Count(r => r.UnblockedDamage > 0);

            if (unblockedHits > 0)
            {
                await PowerCmd.Apply<BloodPower>(cardPlay.Target, unblockedHits, null, this);
            }
        }
        else
        {
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);

        }
       
    }

    protected override void OnUpgrade()
    {
       base.DynamicVars.Damage.UpgradeValueBy(3m);
    }


}