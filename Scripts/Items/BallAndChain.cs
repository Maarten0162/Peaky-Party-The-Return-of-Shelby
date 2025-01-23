using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class BallAndChain : ActiveItem
{   [Export]
    private int MaxRollAdjust;
    public BallAndChain()
    {
        itemName = "Ball and Chain";
        Desc = "Use this to slow down an opponent";
    }


    public override void Use(Player player)
    {
        int userloc = GlobalVar.Plist.IndexOf(player);
        Random rnd = new Random();
        int target = ChooseTarget(GlobalVar.Plist, userloc, rnd);
        Player ptarget = GlobalVar.Plist[target];
        ptarget.rollAdjust -= rnd.Next(1, MaxRollAdjust);
    }
    private int ChooseTarget(List<Player> plist, int userloc, Random rnd)
    {
        
        int target = rnd.Next(0, plist.Count);
        if (target == userloc)
        {
            return ChooseTarget(plist, userloc, rnd);
        }
        return target;

    }

}
