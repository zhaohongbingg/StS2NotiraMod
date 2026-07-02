using HarmonyLib;
using Godot;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Localization.Formatters;
using SmartFormat.Core.Extensions;

namespace Notira.Notira.Patches;

public static class EnergyIconFallbackPatch
{
    private const string FallbackPrefix = "colorless";

    [HarmonyPatch(typeof(EnergyIconsFormatter), nameof(EnergyIconsFormatter.TryEvaluateFormat))]
    public static class PatchEnergyIconsFormatter
    {
        static void Prefix(IFormattingInfo formattingInfo)
        {
            if (formattingInfo.CurrentValue is EnergyVar energyVar)
            {
                string prefix = energyVar.ColorPrefix;
                if (!string.IsNullOrEmpty(prefix) && prefix != FallbackPrefix)
                {
                    string path = "res://images/packed/sprite_fonts/" + prefix + "_energy_icon.png";
                    if (!ResourceLoader.Exists(path))
                    {
                        energyVar.ColorPrefix = FallbackPrefix;
                    }
                }
            }
        }
    }
}
