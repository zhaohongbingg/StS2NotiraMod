using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Keywords;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;






public class Sakuretto() : NotiraCard(1,
	CardType.Skill, CardRarity.Rare,
	TargetType.AnyEnemy)
{

	protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
	{
		new DynamicVar("Tantie", 2.5m),
		new BlockVar(10,ValueProp.Move)
	};
	public override bool GainsBlock => true;
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [
   HoverTipFactory.FromPower<TantiePower>()

];
	public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[]
{
		CardKeyword.Exhaust,
		
};


	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
	   
		if (cardPlay.Target.Monster.IntendsToAttack)
		{
			await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
		}
		else
		{
			await PowerCmd.Apply<TantiePower>(cardPlay.Target, DynamicVars["Tantie"].BaseValue, Owner.Creature, this);
		}
	}

	protected override void OnUpgrade()
	{
		base.DynamicVars.Block.UpgradeValueBy(3);
	}
}
