using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using Notira.Notira.Cards;
using Notira.Notira.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;


public class  Savior() : NotiraCard(
    1, CardType.Power, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DynamicVar("RITUAL",1m),
        new PowerVar<DexterityPower>(1m)
      
        

    };
    private bool isUsed = false;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<RitualPower>()

];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
         await PowerCmd.Apply<RitualPower>(choiceContext, Owner.Creature, DynamicVars["RITUAL"].BaseValue, Owner.Creature, this);
        isUsed = true;


    }
    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
      if (isUsed)
        {
            await PowerCmd.Apply<DexterityPower>(null, Owner.Creature, DynamicVars.Dexterity.BaseValue, Owner.Creature, this);
        }
    }
  
    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}