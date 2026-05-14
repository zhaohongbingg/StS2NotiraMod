using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;


public class Hokejo() : NotiraCard(2,
    CardType.Power, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
    HoverTipFactory.FromPower<SakuraPower>()

];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var a = cardPlay.Target.GetPowerAmount<SakuraPower>();
        var maxHp = cardPlay.Target.MaxHp;
        var raw = maxHp * 0.15m * a;

        Console.WriteLine($"maxHp={maxHp}, a={a}, raw={raw}");
        await CreatureCmd.Damage(
      new ThrowingPlayerChoiceContext(),
      cardPlay.Target,
      raw,
      ValueProp.Unblockable | ValueProp.Unpowered,
      null,
      null
  );
        await PowerCmd.Remove<SakuraPower>( cardPlay.Target );

    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);

    }
}