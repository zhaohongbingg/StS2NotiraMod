using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System.Formats.Asn1;

namespace Notira.Notira.Cards;

public class BadEND() : NotiraCard(-1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{

    protected override bool HasEnergyCostX => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
 {     new CalculationBaseVar(0m),
        new ExtraDamageVar(1m),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel card, Creature? _) => card.Owner.Creature.Block+card.Owner.Creature.CurrentHp*0.5m) };
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int num = ResolveEnergyXValue();
        if (base.IsUpgraded) { num++; }
        if (num > 0)
        {
            await CreatureCmd.Damage(
      choiceContext,
      base.Owner.Creature,
      base.Owner.Creature.CurrentHp * 0.5m,
      ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move,
      this
  );
            await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).WithHitCount(num).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
        }
    }


};



  