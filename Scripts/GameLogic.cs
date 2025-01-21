using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class GameLogic : Node
{
    private PackedScene Playerscene = (PackedScene)GD.Load("res://Scenes/Game Objects/player.tscn");
    Board Board;
    private Player player1;
    private Player player2;
    private Player player3;
    private Player player4;
    private List<Player> PList;
    private int TurnCount;
    private enum InputMode
    {
        YesNo,
        Dpad,

    }
    private InputMode currentInputMode;

    public override void _Ready()
    {
        Board = GetNode<Board>("Board");
        if (TurnCount == 0)
        {
            int playeramount = CreatePlayers(4); // hier moet aantal spelers dat gekozen is, wrs global variable, maar natuurlijk proberen die zo min mogelijk te gebruiken.
            CreatePlayerOrder(playeramount);
            for (int i = 0; i < PList.Count; i++)
            {
                SetPlayerPos(PList[i], Board.spacesInfo[0].SpacePos);
            }

        }
        Game();
    }
    private void Game()
    {
        for (int i = 0; i < PList.Count; i++)
        {
            Turn(PList[i]);
        }
    }
    private void Turn(Player player)
    {
        int target = player.currSpace + new Dice(1, 6).Roll();
        player.Movement(Board, target);
    }
    private void SetPlayerPos(Player player, Vector2 space)
    {

    }
    public int CreatePlayers(int amount)
    {
        int players = 0;
        if (amount >= 1)
        {
            player1 = (Player)Playerscene.Instantiate();
            players++;
            player1.Position = Board.spacesInfo[0].SpacePos;
        }
        if (amount >= 2)
        {
            player2 = (Player)Playerscene.Instantiate();
            players++;
        }
        if (amount >= 3)
        {
            player3 = (Player)Playerscene.Instantiate();
            players++;
        }
        if (amount >= 4)
        {
            player4 = (Player)Playerscene.Instantiate();
            players++;
        }
        return players;
    }
    public void CreatePlayerOrder(int amount)
    {
        PList = new();
        //hier moet een playerordering scene komen, voor nu gwn niks.
        if (amount == 2)
        {
            PList.Add(player1);
            PList.Add(player2);
        }
        if (amount == 3)
        {
            PList.Add(player1);
            PList.Add(player2);
            PList.Add(player3);

        }
        if (amount == 4)
        {
            PList.Add(player1);
            PList.Add(player2);
            PList.Add(player3);
            PList.Add(player4);
        }

    }

    public override void _Input(InputEvent @event)
    {
        switch (currentInputMode)
        {
            case InputMode.YesNo:
                ShopInputs(@event);
                break;

            case InputMode.Dpad:
                DiceMenuInputs(@event);
                break;
        }
    }

    private void ShopInputs(InputEvent @event)
    {
        if (@event.IsActionPressed("yes"))
        {
            GD.Print("Yes pressed");
            // Handle "yes" action
        }
        else if (@event.IsActionPressed("no"))
        {
            GD.Print("No pressed");
            // Handle "no" action
        }
    }

    private void DiceMenuInputs(InputEvent @event)
    {
        if (@event.IsActionPressed("DpadLeft"))
        {
            GD.Print("use left dice");

        }
        else if (@event.IsActionPressed("DpadRigtht"))
        {
            GD.Print("use right dice");

        }

    }

}
