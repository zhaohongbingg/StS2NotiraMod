using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using Notira.Notira.Cards;
using Notira.Notira.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;

public class STEINSGATE() : NotiraCard(
    1, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
 
    private int Rngnumber()
    {
        return base.Owner.RunState.Rng.CombatEnergyCosts.NextInt(10);


    }
    private bool isPassed()
    {
        int num=Rngnumber();
        return num < 5;
    }
    private bool isBoosroom()
    {
        return base.Owner.Creature.Player.RunState.CurrentRoom.RoomType != RoomType.Boss;

    }
    private bool isPass = false;
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {

        if (isPassed()&&isBoosroom())
        {
            foreach (Creature hittableEnemy in base.CombatState.HittableEnemies)
            {

                await CreatureCmd.Escape(hittableEnemy);
                isPass= true;
            }

        }
        else
        {
            SfxHelper.Play("res://Notira/music/failure.ogg");
            CardModel card = base.CombatState.CreateCard<Failure>(base.Owner);
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Discard, addedByPlayer: true));
            await Cmd.Wait(0.5f);

        }
    }
    public override bool TryModifyRewards(Player player, List<Reward> rewards, AbstractRoom? room)
    {
        if (!isPass)
        {
            return false;
        }
        if (!isPassed())
        {
            return false;
        }
        if (player != base.Owner)
        {
            return false;
        }
        if (room == null ||
       (room.RoomType != RoomType.Monster && room.RoomType != RoomType.Elite))
        {
            return false;
        }
        rewards.Add(new CardReward(CardCreationOptions.ForRoom(base.Owner, RoomType.Monster), 3, player));
        rewards.Add(new RelicReward(player));
        return true;
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
       
    }
}