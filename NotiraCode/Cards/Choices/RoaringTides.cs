using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Cards;
using Notira.Notira.Powers;
using Notira.Notira.VFX;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;

public sealed class RoaringTides : BitterChoiceOptionCard
{
    public RoaringTides() : base(1, CardType.Skill, CardRarity.Token, TargetType.AllEnemies)
    { }


    public override bool CanBeGeneratedInCombat => false;


    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override IEnumerable<DynamicVar> CanonicalVars=> new DynamicVar[]
    {
       new DynamicVar("RoaringTidesPower",2),
       new EnergyVar(1)
    };
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<RoaringTidesPower>()

];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
    await PowerCmd.Apply<RoaringTidesPower>(Owner.Creature, DynamicVars["RoaringTidesPower"].BaseValue,Owner.Creature,this);
        SfxHelper.Play("res://Notira/music/chaoming.ogg");


    }




    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);

    }
}
