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
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Random;
using Notira.Notira.Cards;
using Notira.Notira.Extensions;
using Notira.Notira.Powers;
using System.Drawing;
namespace Notira.Notira.Powers;


public   class MajoNikkiPower : NotiraPower
{
    public override PowerType Type => PowerType.Debuff;   // 减力量是 Debuff
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool ShouldReceiveCombatHooks => true;

    // 你需要根据实际情况实现这个属性，例如从构造函数传入
    public  AbstractModel OriginModel => ModelDb.Card<MajoNikki>();

    private int Sign => -1;   // 固定为负，表示减力量



    // 忽略下一次应用（可选）
    private bool _shouldIgnoreNextInstance;

    public void IgnoreNextInstance() => _shouldIgnoreNextInstance = true;

    public override async Task BeforeApplied(Creature target, decimal amount, Creature? applier, CardModel? cardSource)
    {
        
        if (_shouldIgnoreNextInstance)
            _shouldIgnoreNextInstance =true;
        else
            await PowerCmd.Apply<StrengthPower>(target, Sign * amount, applier, cardSource, silent: true);
        Log.Info($"BeforeApplied called:amount{amount}");
    }



    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == Owner.Side)
        {
            Flash();
            await PowerCmd.Remove(this);
            await PowerCmd.Apply<StrengthPower>(Owner, -Sign * Amount, Owner, null);
        }
        Log.Info($"AfterTurnEnd called:amount{-Sign * Amount}");
    }




}




