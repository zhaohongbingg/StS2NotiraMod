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
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Powers;
using System.Reflection;
namespace Notira.Notira.Powers;




public sealed class SakuraMoyoPower : NotiraPower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
 


    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card is Notira.Cards.SakuraMoyu)
        {
            await CreatureCmd.GainMaxHp(base.Owner, base.Owner.GetPowerAmount<SakuraMoyoPower>());


        }
    }
 


    public override async Task  AfterCombatEnd(CombatRoom room)
    {      
        BlockingPlayerChoiceContext context = new BlockingPlayerChoiceContext();
        await  CreatureCmd.LoseMaxHp(context, base.Owner, base.Owner.GetPowerAmount<SakuraMoyoPower>(), isFromCard: false);
     
    }

    public override IEnumerable<HealthBarForecastSegment> GetHealthBarForecastSegments(HealthBarForecastContext context)
    {
        if (base.Amount <= 0)
            yield break;


        //橙色血条
        yield return new HealthBarForecastSegment(
            Amount: base.Owner.GetPowerAmount<SakuraMoyoPower>(),
            Color: new Color("#FA6600"), //数字颜色
            Direction: HealthBarForecastDirection.FromRight, //从右往左
            Order: 2,
            OverlayMaterial: null,
            OverlaySelfModulate: new Color("#EEE732") //橙色血条
        );
    }

}


