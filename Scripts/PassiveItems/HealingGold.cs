using Godot;
using System;

public partial class HealingGold : PassiveItem
{       Random random = new();
       public HealingGold()
    {
        itemName = "Healing Gold";
        Desc = "Whenever you earn gold, Heal.";
        WhenToRun.Add(WhenActive.ObtainCurrency);
        Texturepath = "res://Assets/Items/Passive/GamblersWealth.png";
    }
    public override void RunOnObtainCurrency(int amount)
    {
        int healing = random.Next(1, amount);
        POwner.Heal(healing);
        GD.Print($"You healed for {healing} thanks to Healing Gold");
    }
}
