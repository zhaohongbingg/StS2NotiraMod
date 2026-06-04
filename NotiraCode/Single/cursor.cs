using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Characters;
using System.Reflection.Metadata;

namespace Notira.Notira.Single;

public class CursorSingleton : CustomSingletonModel
{
    public CursorSingleton(bool receiveCombatHooks, bool receiveRunHooks) : base(receiveCombatHooks, receiveRunHooks)
    { }
    public CursorSingleton() : base(receiveCombatHooks: true, receiveRunHooks: false)
    {
    }

    public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
    {
        if (dealer == null)
            return 1m;

        if (dealer.Player == null)
            return 1m;

        if (dealer.ModelId == null)
            return 1m;

        string characterId = dealer.ModelId.Entry;

        Log.Info(characterId);

        if (!characterId.Contains("NOTIRA"))
            return 1m;

        CardPile handPile = PileType.Hand.GetPile(dealer.Player);

        if (handPile == null)
            return 1m;

        bool hasCurse = handPile.Cards.Any(c => c.Type == CardType.Curse);

        if (hasCurse)
        {
            Log.Info("CursorSingleton: Curse card found in hand, reducing damage by 25%");
            return 0.75m;
        }

        return 1m;
    }
}
