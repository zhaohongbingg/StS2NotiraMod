using BaseLib.Abstracts;
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


public   class MajoNikkiPower : CustomTemporaryPowerModelWrapper<Euthanasia, StrengthPower>
{
    protected override bool InvertInternalPowerAmount => true;
    protected override bool UntilEndOfOtherSideTurn => false;

    public override PowerType Type => PowerType.Debuff;
    public override string CustomPackedIconPath
       => "res://Notira/Images/Powers/majo_nikki_power.png";

    public override string CustomBigIconPath
        => "res://Notira/Images/Powers/majo_nikki_power.png";
}









