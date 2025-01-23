using Godot;
using System;

public partial class PassiveItem : Node
{
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
    
    protected string itemName;
    protected string Desc;
    protected int Price;

}
