using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;




public class Sacle() : NotiraCard(1,
	CardType.Skill, CardRarity.Rare,
	TargetType.AnyEnemy)
{





	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		List<PowerModel> originalDebuffs = (from p in cardPlay.Target.Powers
											where p.TypeForCurrentAmount == PowerType.Debuff
											select (PowerModel)p.ClonePreservingMutability()).ToList();
		decimal count = 0;
		foreach (PowerModel item in originalDebuffs)
		{
			count += item.Amount;
		}
		await PowerCmd.Apply<StrengthPower>(choiceContext, Owner.Creature, count*2, Owner.Creature, null);
	}

	


	protected override void OnUpgrade()
	{
		AddKeyword(CardKeyword.Retain);
	}
}
