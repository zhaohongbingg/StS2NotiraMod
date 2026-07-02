using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Random;
using Notira.Notira.Cards;
using System.Linq;
using System.Reflection;
namespace Notira.Notira.Powers;


public sealed class ClannadPower : NotiraPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public bool isLightRrb()
    {
        return base.Owner.HasPower<LightOrb>() && base.Owner.GetPower<LightOrb>().Amount > 13;
    }

    private static readonly Type[] ClannadCardTypes =
    [
        typeof(FurukawaNagisa), typeof(FujibayashiKyo), typeof(FujibayashiRyo),
        typeof(IchinoseKotomi), typeof(SakagamiTomoyo), typeof(IbukiFuko),
        typeof(MiyazawaYukine), typeof(SagaraMisae), typeof(SunoharaYohei),
        typeof(SunoharaMei), typeof(KoumuraToshio), typeof(YoshinoYusuke),
        typeof(FurukawaAkio)
    ];

    private static CardModel? GetCard(Type type)
    {
        var method = typeof(ModelDb).GetMethod("Card", System.Type.EmptyTypes);
        return method?.MakeGenericMethod(type).Invoke(null, null) as CardModel;
    }

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)
    {
        if (player != base.Owner.Player)
        {
            return;
        }

        var models = ClannadCardTypes.Select(GetCard).Where(c => c != null).ToList();
        if (models.Count == 0)
            return;

        CardModel[] array = new CardModel[base.Amount];
        Rng rng = base.Owner.Player.RunState.Rng.CombatCardGeneration;
        for (int i = 0; i < base.Amount; i++)
        {
            var card = CardFactory.GetDistinctForCombat(player, models, 1, rng).FirstOrDefault();
            if (card == null)
                continue;
            CardCmd.ApplyKeyword(card, CardKeyword.Exhaust);
            array[i] = card;
        }

        Flash();
        await CardPileCmd.AddGeneratedCardsToCombat(array, PileType.Hand, base.Owner.Player, CardPilePosition.Random);
    }
}