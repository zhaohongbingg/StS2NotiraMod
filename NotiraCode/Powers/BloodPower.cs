using BaseLib.Hooks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notira.Notira.Powers;




public sealed class BloodPower : NotiraPower
{
    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Counter;


    public override IEnumerable<HealthBarForecastSegment> GetHealthBarForecastSegments(HealthBarForecastContext context)
    {
        if (base.Amount <= 0)
            yield break;

 
        yield return new HealthBarForecastSegment(
            Amount: base.Amount * 2,
            Color: new Color("#FA6600"), //数字颜色
            Direction: HealthBarForecastDirection.FromRight, //从右往左
            Order: 0,
            OverlayMaterial: null,
            OverlaySelfModulate: new Color("#FF0000") //橙色血条
        );
    }


    private int TriggerCount
    {
        get
        {
            IEnumerable<Creature> source = from c in base.Owner.CombatState.GetOpponentsOf(base.Owner)
                                           where c.IsAlive
                                           select c;
            return Math.Min(base.Amount, 1 + source.Sum((Creature a) => a.GetPowerAmount<SDBPower>()));
        }
    }
    public int CalculateTotalDamageNextTurn()
    {
        decimal num = default(decimal);
        int num2 = Math.Min(base.Amount, TriggerCount);
        for (int i = 0; i < num2; i++)
        {
            decimal damage = base.Amount - i;
            damage = Hook.ModifyDamage(base.Owner.CombatState.RunState, base.Owner.CombatState, base.Owner, null, damage, ValueProp.Unblockable | ValueProp.Unpowered, null, ModifyDamageHookType.All, CardPreviewMode.None, out IEnumerable<AbstractModel> _);
            num += damage;
        }
        return (int)num;
    }



   
    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (side != base.Owner.Side)
        {
            return;
        }
        int iterations = TriggerCount;
        for (int i = 0; i < iterations; i++)
        {
            await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner, base.Amount*2, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
          
        }
    }



}



