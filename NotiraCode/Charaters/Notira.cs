using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.RelicPools;
using Notira.Notira.Extensions;
using Notira.Notira.Cards;
using System;
using Notira.Notira.Characters;
using Notira.Notira.Relics;

namespace Notira.Notira.Characters;

public class Notira : PlaceholderCharacterModel
{
	public const string CharacterId = "Notira";

	public override string CustomCharacterSelectBg => "res://Notira/Scences/NotiraBG.tscn";

	public override string PlaceholderID => "necrobinder";

	public static readonly Color Color = new Color("5DA9FF");

	public override Color NameColor => Color;
	public override CharacterGender Gender => CharacterGender.Feminine;
	public override int StartingHp => 77;

	public override IEnumerable<CardModel> StartingDeck => [
		ModelDb.Card<NotiraAttack>(),
		ModelDb.Card<NotiraAttack>(),
		ModelDb.Card<NotiraAttack>(),
		ModelDb.Card<NotiraAttack>(),
		ModelDb.Card<NotiraAttack>(),
		ModelDb.Card<NotiraBlock>(),
		ModelDb.Card<NotiraBlock>(),
		ModelDb.Card<NotiraBlock>(),
		ModelDb.Card<NotiraBlock>(),
		ModelDb.Card<Classic>()

		 
	];

	 
	public override IReadOnlyList<RelicModel> StartingRelics => [ ModelDb.Relic<NotiraNewbie>(),];

	public override CardPoolModel CardPool => ModelDb.CardPool<NotiraCardPool>();
	public override RelicPoolModel RelicPool => ModelDb.RelicPool<NotiraRelicPool>();
	public override PotionPoolModel PotionPool => ModelDb.PotionPool<SharedPotionPool>();

	/*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
		override all the other methods that define those assets.
		These are just some of the simplest assets, given some placeholders to differentiate your character with.
		You don't have to, but you're suggested to rename these Images. */
	public override string CustomVisualPath => "res://Notira/Scences/NotiraCh.tscn";
	public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
	public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
	public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
	public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();

	public override string CustomIconPath => "res://Notira/Scences/notira_icon.tscn";
	public override string CustomEnergyCounterPath => "res://Notira/Scences/notira_energy_counter.tscn";
	public override string CustomRestSiteAnimPath => "res://Notira/Scences/notira_rest_site.tscn";
	public override string CustomMerchantAnimPath => "res://Notira/Scences/notira_merchant.tscn";
	// 多人模式-手指。
	public override string CustomArmPointingTexturePath => "res://Notira/Scences/hand/point.png";
	// 多人模式剪刀石头布-石头。
	 public override string CustomArmRockTexturePath => "res://Notira/Scences/hand/stone.png";
	// 多人模式剪刀石头布-布。
	 public override string CustomArmPaperTexturePath => "res://Notira/Scences/hand/paper.png";
	// 多人模式剪刀石头布-剪刀。
	 public override string CustomArmScissorsTexturePath => "res://Notira/Scences/hand/Scissors.png";
}
