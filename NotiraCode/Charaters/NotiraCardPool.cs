using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using Notira.Notira.Characters;
using Notira.Notira.Extensions;
using MegaCrit.Sts2.Core.Logging;

namespace Notira.Notira.Characters;

public sealed class NotiraCardPool : CustomCardPoolModel 
{
    public override string Title => Notira.CharacterId; //This is not a display name.



    public override string BigEnergyIconPath => "res://Notira/Images/Charui/big_energy.png";
    public override  string TextEnergyIconPath => "res://Notira/Images/Charui/text_energy.png";

    

    



    /* These HSV values will determine the color of your card back.
	They are applied as a shader onto an already colored image,
	so it may take some experimentation to find a color you like.
	Generally they should be values between 0 and 1. */
    public override float H => 0.60f;
    public override float S => 0.55f;
    public override float V => 0.80f;

    //Alternatively, leave these values at 1 and provide a custom frame image.
    /*public override Texture2D CustomFrame(CustomCardModel card)
	{
		//This will attempt to load Oddmelt/Images/cards/frame.png
		return PreloadManager.Cache.GetTexture2D("cards/frame.png".ImagePath());
	}*/

    //Color of small card icons
    public override Color DeckEntryCardColor => new("1C2A44");
    public override Color EnergyOutlineColor => new("5DA9FF");

    public override bool IsColorless => false;
}
