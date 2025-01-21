using Godot;
using System;

public partial class Item : Sprite2D
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
