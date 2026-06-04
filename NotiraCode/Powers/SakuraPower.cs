using BaseLib.Hooks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Powers;
 
namespace Notira.Notira.Powers;



public sealed class SakuraPower : NotiraPower
{
    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Counter;

  


    public override IEnumerable<HealthBarForecastSegment> GetHealthBarForecastSegments(HealthBarForecastContext context)
    {
        if (base.Amount <= 0)
            yield break;


        //橙色血条
        yield return new HealthBarForecastSegment(
            Amount:  (int)(base.Owner.MaxHp * 0.1m),
            Color: new Color("#FA6600"), //数字颜色
            Direction: HealthBarForecastDirection.FromRight, //从右往左
            Order: 0,
            OverlayMaterial: null,
            OverlaySelfModulate: new Color("#FF78A0") //橙色血条
        );
    }

  




    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side != base.Owner.Side)
            return;

        if (!base.Owner.IsAlive)
            return;

        decimal DrawHp = (decimal)(base.Owner.MaxHp * 0.1m);
        int hardenedShellAmount = base.Owner.GetPowerAmount<HardenedShellPower>();
        int hardToKillAmount = base.Owner.GetPowerAmount<HardToKillPower>();
        int intangibleAmount = base.Owner.GetPowerAmount<IntangiblePower>();

        //移除这两个Power
        if (hardenedShellAmount > 0)
        {
            await PowerCmd.Remove<HardenedShellPower>(base.Owner);
        }
        if (hardToKillAmount > 0)
        {
            await PowerCmd.Remove<HardToKillPower>(base.Owner);
        }
        if (intangibleAmount > 0)
        {
            await PowerCmd.Remove<IntangiblePower>(base.Owner);
        }

        await CreatureCmd.Damage(
        new ThrowingPlayerChoiceContext(),
        base.Owner,
        DrawHp,
        ValueProp.Unblockable | ValueProp.Unpowered,
        null,
        null
    );
        //恢复原 Power 层数
        if (hardenedShellAmount > 0)
        {
            await PowerCmd.Apply<HardenedShellPower>(base.Owner, hardenedShellAmount, null, null);
        }
        if (hardToKillAmount > 0)
        {
            await PowerCmd.Apply<HardToKillPower>(base.Owner, hardToKillAmount, null, null);
        }
        if (intangibleAmount > 0)
        {
            await PowerCmd.Apply<IntangiblePower>(base.Owner, intangibleAmount, null, null);
        }



    }
  



    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Enemy)
        {
            await PowerCmd.TickDownDuration(this);
        }
    }


}


    
