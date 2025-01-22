using Godot;
using System;

public partial class Item : Control
{
    protected string itemName;
    protected string Desc;
    protected int Price;

    protected enum Rarity {
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

    public virtual void Use(Player player)
    {

    }
}
