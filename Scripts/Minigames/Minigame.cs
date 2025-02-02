using Godot;
using System;
using System.Collections.Generic;

public partial class Minigame : Node
{
    protected List<Player> winners;
    protected PackedScene Playerscene;

    protected List<Player> PList
    {
        get { return GlobalVar.Plist; }
    }

    protected void Finish(List<Player> winnerslist)
    {
        foreach (Player player in winnerslist)
        {
            player.Currency += 333;
        }
      
    }

}
