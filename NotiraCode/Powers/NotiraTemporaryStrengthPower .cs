using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot; 
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using Notira.Notira.Extensions;

namespace Notira.Notira.Powers;

public abstract class NotiraStrengthTempower : CustomTemporaryPowerModel
{


    public override PowerModel InternallyAppliedPower
        => new StrengthPower();

    public override AbstractModel OriginModel => this;

    protected override bool UntilEndOfOtherSideTurn => true;

    protected override int LastForXExtraTurns => 0;

}