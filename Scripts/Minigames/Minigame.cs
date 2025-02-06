using Godot;
using System;
using System.Collections.Generic;

public partial class Minigame : Node
{
    protected List<Player> winners = new();
    protected PackedScene Playerscene;

    protected List<Player> PList
    {
        get { return GlobalVar.Plist; }
    }
    protected List<Player> Minigamers = new();

    

    protected void Finish(List<Player> winnerslist)
    {
        if (winnerslist.Count > 0)
        {
            foreach (Player player in winnerslist)
        {
            player.Currency += 333;
        }
        }     
      
    }

}
