using MegaCrit.Sts2.Core.Nodes.Screens.Shops;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.RestSite;
using Notira.Notira.Characters;



public partial class NotiraShop : NMerchantCharacter
{
	public override void _Ready() {
		PlayAnimation("shop", loop: true);
		
	}

  
}
