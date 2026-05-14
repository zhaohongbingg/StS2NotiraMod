using BaseLib.Abstracts;
using Godot;
using Notira.Notira.Characters;
using System;

namespace Notira.Notira.Characters;

public partial class NotiraRelicPool : CustomRelicPoolModel
{
    public override string EnergyColorName => Notira.CharacterId;

    public override Color LabOutlineColor => Notira.Color;
}