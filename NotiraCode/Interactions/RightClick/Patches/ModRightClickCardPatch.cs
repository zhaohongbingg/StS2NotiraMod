using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.ControllerInput;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Nodes.Cards.Holders;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace Notira.Notira.Interactions.RightClick.Patches;

/// <summary>
///     Connects right-click dispatch to hand-card holders.
///     将右键分发接入手牌 holder。
/// </summary>
[HarmonyPatch(typeof(NPlayerHand), "AddCardHolder")]
public static class ModRightClickCardPatch
{
    public static void Postfix(NHandCardHolder holder)
    {
        MainFile.Logger.Info($"[RightClick-Patch] AddCardHolder Postfix called, connecting signals...");
        
        holder.Connect(Control.SignalName.GuiInput,
            Callable.From<InputEvent>(inputEvent => OnHolderGuiInput(holder, inputEvent)));
        holder.Hitbox.Connect(Control.SignalName.GuiInput,
            Callable.From<InputEvent>(inputEvent => OnHitboxGuiInput(holder, inputEvent)));
    }

    private static void OnHolderGuiInput(NCardHolder holder, InputEvent inputEvent)
    {
        MainFile.Logger.Info($"[RightClick-Patch] OnHolderGuiInput called, inputEvent type: {inputEvent.GetType().Name}");
        
        var triggeredByController =
            inputEvent is InputEventAction { Action: var action } actionEvent &&
            action == MegaInput.cancel &&
            actionEvent.IsPressed() &&
            holder.HasFocus();

        if (triggeredByController)
        {
            MainFile.Logger.Info($"[RightClick-Patch] Controller input detected!");
            TryHandle(holder, new(true));
        }
    }

    private static void OnHitboxGuiInput(NCardHolder holder, InputEvent inputEvent)
    {
        MainFile.Logger.Info($"[RightClick-Patch] OnHitboxGuiInput called, inputEvent type: {inputEvent.GetType().Name}");
        
        if (inputEvent is InputEventMouseButton mouseButton)
        {
            MainFile.Logger.Info($"[RightClick-Patch] MouseButton event - ButtonIndex: {mouseButton.ButtonIndex}, IsPressed: {mouseButton.IsPressed()}");
        }
        
        var triggeredByMouse =
            inputEvent is InputEventMouseButton { ButtonIndex: MouseButton.Right } rightClick &&
            rightClick.IsPressed();

        if (triggeredByMouse)
        {
            MainFile.Logger.Info($"[RightClick-Patch] Right mouse button detected!");
            TryHandle(holder, new(false));
        }
    }

    private static void TryHandle(NCardHolder holder, ModRightClickTrigger trigger)
    {
        MainFile.Logger.Info($"[RightClick-Patch] TryHandle called, trigger.IsController: {trigger.IsController}");
        
        var viewport = holder.GetViewport();
        if (viewport.IsInputHandled())
        {
            MainFile.Logger.Info($"[RightClick-Patch] Input already handled, returning");
            return;
        }

        var hand = NPlayerHand.Instance;
        if (hand == null || hand.InCardPlay || NTargetManager.Instance.IsInSelection)
        {
            MainFile.Logger.Info($"[RightClick-Patch] Hand state check failed - hand: {hand != null}, InCardPlay: {hand?.InCardPlay}, IsInSelection: {NTargetManager.Instance?.IsInSelection}");
            return;
        }

        var card = holder.CardModel;
        if (card == null)
        {
            MainFile.Logger.Info($"[RightClick-Patch] Card is null, returning");
            return;
        }

        var player = LocalContext.GetMe(card.CombatState);
        if (player == null)
        {
            MainFile.Logger.Info($"[RightClick-Patch] Player is null, returning");
            return;
        }

        MainFile.Logger.Info($"[RightClick-Patch] Dispatching right-click for card: {card.Id}");
        
        if (ModRightClickRegistry.TryDispatch(new(player, card, trigger)))
        {
            MainFile.Logger.Info($"[RightClick-Patch] Right-click dispatched successfully, setting input as handled");
            viewport.SetInputAsHandled();
        }
        else
        {
            MainFile.Logger.Info($"[RightClick-Patch] Right-click dispatch returned false");
        }
    }
}