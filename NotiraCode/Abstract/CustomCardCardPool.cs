using BaseLib.Abstracts;
using BaseLib.Patches.UI;
using Godot;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notira.Notira.Abstract;
public abstract class CustomCardPoolModel : CardPoolModel, ICustomModel, ICustomEnergyIconPool
{
    public override string CardFrameMaterialPath => "card_frame_red";

    public virtual Color ShaderColor => new Color("FFFFFF");

    public virtual float H => ShaderColor.H;

    public virtual float S => ShaderColor.S;

    public virtual float V => ShaderColor.V;

    public virtual bool IsShared => false;

    public override string EnergyColorName => CustomEnergyIconPatches.GetEnergyColorName(base.Id);

    public virtual string? BigEnergyIconPath => null;

    public virtual string? TextEnergyIconPath => null;

    public CustomCardPoolModel()
    {
         
    }

    public virtual Texture2D? CustomFrame(CustomCardModel card)
    {
        return null;
    }

    protected override CardModel[] GenerateAllCards()
    {
        return Array.Empty<CardModel>();
    }
}