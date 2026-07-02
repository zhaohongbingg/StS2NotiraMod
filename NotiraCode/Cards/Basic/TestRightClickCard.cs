/*using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using Notira.Notira.Interactions.RightClick;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notira.Notira.Cards;

/// <summary>
///     测试右键功能的卡片
///     右键点击时获得1点能量
/// </summary>
public class TestRightClickCard() : NotiraCard(1,
	CardType.Attack, CardRarity.Basic,
	TargetType.AnyEnemy), IModRightClickableCard
{
	protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
	protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move)];

	// 右键点击时的本地过滤（可选）
	public bool CanHandleRightClickLocal(ModRightClickContext context)
	{
		// 只在战斗中允许右键
		return CombatManager.Instance != null && CombatManager.Instance.IsInProgress;
	}

	// 右键点击时的执行判定（可选）
	public bool CanExecuteRightClick(ModRightClickExecutionContext context)
	{
		// 检查是否在战斗中
		return CombatManager.Instance != null && CombatManager.Instance.IsInProgress;
	}

	// 右键点击时执行的效果
	public async Task OnRightClick(ModRightClickExecutionContext context)
	{
		MainFile.Logger.Info("[TestCard] Right-clicked! Gaining 1 energy...");

		try
		{
			// 尝试调用游戏命令
			await PlayerCmd.GainEnergy(1, context.Player);
			MainFile.Logger.Info("[TestCard] Energy gained successfully!");
		}
		catch (Exception ex)
		{
			MainFile.Logger.Warn($"[TestCard] Failed to gain energy: {ex.Message}");
			MainFile.Logger.Info("[TestCard] Right-click system is working, but game commands need special handling.");
		}
	}

	// 卡片正常播放效果
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await CommonActions.CardAttack(this, cardPlay.Target).Execute(choiceContext);
	}

	// 升级效果
	protected override void OnUpgrade()
	{
		DynamicVars.Damage.UpgradeValueBy(2m);
	}
}*/