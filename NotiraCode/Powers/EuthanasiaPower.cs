using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Random;
using Notira.Notira.Cards;
using Notira.Notira.Extensions;
using Notira.Notira.Powers;
namespace Notira.Notira.Powers;


public class  EuthanasiaPower : NotiraPower
{
    public override PowerType Type => PowerType.Debuff;   // 减力量是 Debuff
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool ShouldReceiveCombatHooks => true;




    private int Sign => -1;   // 固定为负，表示减力量



    // 忽略下一次应用（可选）
    private bool _shouldIgnoreNextInstance;

    public void IgnoreNextInstance() => _shouldIgnoreNextInstance = true;

    public override async Task BeforeApplied(Creature target, decimal amount, Creature? applier, CardModel? cardSource)
    {
        // 设置标志，让 AfterPowerAmountChanged 跳过本次变化
        _shouldIgnoreNextInstance = true;
        await PowerCmd.Apply<StrengthPower>(target, Sign * amount, applier, cardSource, silent: true);
        Log.Info($"BeforeApplied called: amount {amount}");
    }

    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power == this)
        {
            if (_shouldIgnoreNextInstance)
            {
                _shouldIgnoreNextInstance = false;
                Log.Info("AfterPowerAmountChanged skipped (first add)");
            }
            else if (amount == Amount)  // 只在叠加时触发（数量变化量等于当前总层数）
            {
                await PowerCmd.Apply<StrengthPower>(Owner, Sign * amount, applier, cardSource, silent: true);
                Log.Info($"AfterPowerAmountChanged applied extra: amount {amount}");
            }
        }
        Log.Info($"AfterPowerAmountChanged called: amount {amount}");
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == Owner.Side)
        {
            Flash();
            await PowerCmd.Remove(this);
            await PowerCmd.Apply<StrengthPower>(Owner, -Sign * Amount, Owner, null);
            Log.Info($"AfterTurnEnd restored strength: {-Sign * Amount}");
        }
    }
}




    

