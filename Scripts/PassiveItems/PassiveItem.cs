using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

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
        PassingPlayer,
        takeDamage,
        Heal,
        ObtainCurrency,
        LostCurrency,
        RollAdjustChange,
        IncomeChanged
    }
    public List<WhenActive> WhenToRun = new();
    public string Texturepath;
    protected string itemName;
    protected string Desc;
    [Export]
    public int Price = 10;
    protected Player POwner;

    public virtual void SetOwner(Player player)
    {
        POwner = player;
    }
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
    public virtual void RunOnTakingDamage(int takendamage)
    {
    }
    public virtual void RunOnHeal(int amounthealed){

    }
    public virtual void RunOnObtainCurrency(int amount){

    }
    public virtual void RunOnLoseCurrency(int amount){

    }
    public virtual void RunOnChangeRolladjust(int amount){

    }
    public virtual void RunOnIncomeChange(int amount){

    }
}
