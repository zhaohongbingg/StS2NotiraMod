using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Nodes.RestSite;
using System;
using static Godot.Node;

[HarmonyPatch(typeof(NRestSiteCharacter), "_Ready")]
public static class  Restpatch
{
    public static void Postfix(NRestSiteCharacter __instance)
    {
        Log.Info(">>>[NotiraMod]PlayerCharacterIs" + (object)__instance.Player.Character, 2);
        if (!(__instance.Player.Character is Notira.Notira.Characters.Notira))
        {
            return;
        }
        Log.Info(">>>[NotiraMod]ReplacingSpineSpriteWithYiwaScene", 2);
        foreach (Node child in ((Node)__instance).GetChildren(false))
        {
            if (child.Name == "SpineSprite")
            {
                child.QueueFree();
                break;
            }
        }
        PackedScene scene = PreloadManager.Cache.GetScene("res://Notira/Scences/notira_rest_site.tscn");
        if (scene != null)
        {
            Node val = scene.Instantiate();
            val.Name = "Notira";
            ((Node)__instance).AddChild(val, false, (InternalMode.Disabled));
        }
    }
}
