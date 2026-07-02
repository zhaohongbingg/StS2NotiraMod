using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using Notira.Notira.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notira.Notira.Powers;


public sealed class  SaviorPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
     HoverTipFactory.FromPower<StrengthPower>(),
     HoverTipFactory.FromPower<DexterityPower>()

  ];

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner == base.Owner.Player && cardPlay.Card.VisualCardPool.IsColorless)
        {
            Flash();
            await PowerCmd.Apply<StrengthPower>(context, base.Owner, base.Amount, base.Owner, null, false);
            await PowerCmd.Apply<StrengthPower>(context, base.Owner, base.Amount, base.Owner, null, false);
        }
    }
}
