using Godot;
using System;

public partial class Gamblers_Wealth : PassiveItem
{

    public Gamblers_Wealth(Player _player)
    {
        POwner = _player;
        itemName = "Gamblers Wealth";
        Desc = "Gain 5 income, when you have more than 20 income, take 5 damage every turn";
        WhenToRun.Add(WhenActive.Pickup);
        WhenToRun.Add(WhenActive.EndofTurn);
    }
    public Gamblers_Wealth()
    {
        itemName = "Gamblers Wealth";
        Desc = "Gain 5 income, when you have more than 20 income, take 5 damage every turn";
        WhenToRun.Add(WhenActive.Pickup);
        WhenToRun.Add(WhenActive.EndofTurn);
    }
    public override void RunOnPickup()
    {
        POwner.Income += 5;
    }
    public override void RunOnEndofTurn()
    {
        if (POwner.Income > 20)
        {
            POwner.Health -= 10;
            GD.Print("took damage because you are a rich cunt, eat the rich!");
        }
    }
}
