using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Metadata;
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
    int whatPlayer;
    Player currentPlayer;
    private int TurnCount;
    private enum InputMode
    {
        Dice,
        Item,
        Shop
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
        if (player1 != null)
        {
            GD.Print("player 1 exists");
        }
        whatPlayer = 0;
        currentPlayer = PList[whatPlayer];
        Turn(currentPlayer);
    }

    private void Turn(Player player)
    {
        currentInputMode = InputMode.Dice;

    }
    private async void UseDice()
    {
        GD.Print("in use dice");
        int target = currentPlayer.currSpace + 6;
        if (await currentPlayer.Movement(Board, target))
        {
            Turn(PList[whatPlayer+1]);
        }
    }
    private void SetPlayerPos(Player player, Vector2 space)
    {
        player.Position = space;
    }
    public int CreatePlayers(int amount)
    {
        int players = 0;
        if (amount >= 1)
        {
            player1 = (Player)Playerscene.Instantiate();
            AddChild(player1);
            player1.Position = Board.spacesInfo[0].SpacePos;
            player1.currSpace = 1;
            players++;            
        }
        if (amount >= 2)
        {
            player2 = (Player)Playerscene.Instantiate();
            AddChild(player2);
            player2.Position = Board.spacesInfo[0].SpacePos;
            player2.currSpace = 1;
            players++;
        }
        if (amount >= 3)
        {
            player3 = (Player)Playerscene.Instantiate();
            AddChild(player3);
            player3.Position = Board.spacesInfo[0].SpacePos;
            player3.currSpace = 1;
            players++;
        }
        if (amount >= 4)
        {
            player4 = (Player)Playerscene.Instantiate();
            AddChild(player4);
            player4.Position = Board.spacesInfo[0].SpacePos;
            player4.currSpace = 1;
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
            case InputMode.Shop:
                ShopInputs(@event);
                break;

            case InputMode.Dice:
                DiceMenuInputs(@event);
                break;
        }
    }

    private void ShopInputs(InputEvent @event)
    {
        if (@event.IsActionPressed("yes"))
        {
            UseDice();

        }
        else if (@event.IsActionPressed("no"))
        {
            GD.Print("No pressed");

        }
    }

    private void DiceMenuInputs(InputEvent @event)
    {
        if (@event.IsActionPressed("yes"))
        {
            UseDice();

        }
        else if (@event.IsActionPressed("no"))
        {


        }

    }

    private async Task rolDice(Player player)
    {
        Dice dice = new Dice(1,7);
        
        await player.Movement(Board, dice.Roll());
    }

    private void askUseItem(Player player)
    {
        //asks if and what item the player wants to use
    }

    private void checkStartItems(Player player)
    {

    }

    private void checkMidItems(Player player)
    {
        
    }


    private void checkEndItems(Player player)
    {
        
    }
    private async Task turn()
    {
        foreach (Player player in PList)
        {
            if (player.isAlive && !player.SkipTurn)
            {
                checkStartItems(player);
                askUseItem(player);
                await rolDice(player);
            }
        }
    }
    


}
