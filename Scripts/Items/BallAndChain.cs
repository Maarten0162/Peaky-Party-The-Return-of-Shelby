using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class BallAndChain : Active
{
    public BallAndChain(string _name, string _desc, int _price)
    {
        itemName = _name;
        Desc = _desc;
        Price = _price;
    }

    public BallAndChain()
    {
        
    }

    public override void Use(Player player)
    {
        player.rollAdjust =+ 1;
    }
}
