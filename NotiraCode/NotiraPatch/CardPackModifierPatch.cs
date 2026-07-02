using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;
using Notira.Notira.Cards;
using Notira.Notira.Modifiers;

namespace Notira.Notira.Patches;

[HarmonyPatch(typeof(RunState))]
public static class CardPackModifierPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(RunState.CreateForNewRun))]
    public static void AddModifierForNotira(
        RunState __result,
        IReadOnlyList<Player> players,
        IReadOnlyList<ActModel> acts,
        IReadOnlyList<ModifierModel> modifiers,
        int ascensionLevel,
        string seed)
    {
        if (!players.Any(p => p.Character.Id.Entry == "notira"))
            return;

        if (__result.Modifiers.Any(m => m is CardPackSelectionModifier))
            return;

        CardPackRegistry.Initialize();
        if (CardPackRegistry.AllPackNames.Count == 0)
            return;

        ModelDb.Inject(typeof(CardPackSelectionModifier));
        __result.AddModifierDebug(ModelDb.Modifier<CardPackSelectionModifier>());
    }
}
