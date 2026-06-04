using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;


public class EnkiDekina() : NotiraCard(3, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    private bool _usedThisCombat;
    private bool _shouldTakeExtraTurn;
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new IntVar("PlayMax", 2)
    };




    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (UsedThisCombat)
            return Task.CompletedTask;

        int num = CombatManager.Instance.History.CardPlaysFinished
            .Count(e => e.HappenedThisTurn(base.CombatState)
                     && e.CardPlay.Card.Owner == base.Owner);
           

        if (num < base.DynamicVars["PlayMax"].IntValue)
        {
            _shouldTakeExtraTurn = true;
        }

        return Task.CompletedTask;
    }


    private bool UsedThisCombat
    {
        get
        {
            return _usedThisCombat;
        }
        set
        {
            AssertMutable();
            _usedThisCombat = value;
        }
    }

    public override Task AfterTakingExtraTurn(Player player)
    {
        if (player != base.Owner)
        {
            return Task.CompletedTask;
        }
       
      
        UsedThisCombat = true;
        _shouldTakeExtraTurn = false;
        return Task.CompletedTask;
    }



    public override Task AfterCombatEnd(CombatRoom _)
    {
       
        UsedThisCombat = false;
        _shouldTakeExtraTurn = false;
        return Task.CompletedTask;
    }
    public override bool ShouldTakeExtraTurn(Player player)
    {
        return player == base.Owner && _shouldTakeExtraTurn && !UsedThisCombat;
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
        

    }
}

