using Godot;
using System;

public partial class Blood_Gold : PassiveItem
{
    public Blood_Gold()
    {
        itemName = "Blood Gold";
        Desc = "Whenever you take damage, gain gold.";
        WhenToRun.Add(WhenActive.Pickup);
        WhenToRun.Add(WhenActive.EndofTurn);
        Texturepath = "res://Assets/Items/Passive/GamblersWealth.png";
    }
    public override void RunOnTakingDamage(int takendamage)
    {
        POwner.Currency += takendamage;
        GD.Print($"you gain {takendamage} moneys from your blood.");
    }
}
