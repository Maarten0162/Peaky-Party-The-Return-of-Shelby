using Godot;
using System;
using System.Collections.Generic;

public partial class PassiveItem : Node2D
{
    protected enum Rarity
    {
        GoodCommon,
        GoodUncommon,
        GoodRare,
        GoodEpic,
        GoodLegendary,
        GoodMythical,
        BadCommon,
        BadUncommon,
        BadRare,
        BadEpic,
        BadLegendary,
        BadMythical
    }
    public enum WhenActive
    {
        Pickup,
        StartofTurn,
        WhenMoving,
        EndofTurn,
        PassingPlayer
    }
    public List<WhenActive> WhenToRun;

    protected string itemName;
    protected string Desc;
    [Export]
    public int Price = 10;
    protected Player POwner;

    public virtual void RunOnPickup()
    {
    }

    public virtual void RunOnStartofTurn()
    {
    }

    public virtual void RunOnMoving()
    {
    }

    public virtual void RunOnEndofTurn()
    {
    }

    public virtual void RunOnPassingPlayer()
    {
    }
}
