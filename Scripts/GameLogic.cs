using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.SymbolStore;
using System.Linq;
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

    //dices
    Dice normalDice = new(1,4);
    Dice betterDice = new(1,7);
    Dice SuperDice = new(1,8);
    Dice peakyDice = new(1,12);
    private enum InputMode
    {
        Dice,
        Item,
        Shop,
        selectItem,
        None
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
        
        if (player.isAlive && !player.SkipTurn)
        {
            Label label = GetNode<Label>($"{player.Name}/Label");
            label.Text = $"{whatPlayer + 1}";
            checkStartItems(player);
            BallAndChain bac = new();
            BallAndChain bac2 = new();
            BallAndChain bac3 = new();
            BallAndChain bac4 = new();
            BallAndChain bac5 = new();
            BallAndChain bac6 = new();
            player.itemList.Add(bac);
            player.itemList.Add(bac2);
            player.itemList.Add(bac3);
            player.itemList.Add(bac4);
            player.itemList.Add(bac5);
            player.itemList.Add(bac6);
            currentInputMode = InputMode.Item;
            
        }
    }
    private async void UseDice()
    {
        GD.Print("in use dice");
        // int target = currentPlayer.currSpace + normalDice.Roll();
        int target = currentPlayer.currSpace + 15;
        GD.Print($"You threw {target - currentPlayer.currSpace}");
        if (await currentPlayer.Movement(Board, target))
        {
            whatPlayer++;
            if (whatPlayer >= PList.Count)
            {
                whatPlayer = 0;
            }
            currentPlayer = PList[whatPlayer];
            Turn(PList[whatPlayer]);
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
            player1.Position = Board.spacesInfo[120].SpacePos;
            player1.currSpace = 121;
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
            case InputMode.Item:
                ItemMenuInputs(@event);
                break;
            case InputMode.selectItem:
                passthroughInput(@event);
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
        GD.Print("Do you wanna use a Dice?");
        if (@event.IsActionPressed("yes"))
        {
            currentInputMode = InputMode.None;
            UseDice();

        }
        else if (@event.IsActionPressed("no"))
        {
            GD.Print($"Player doesn't wanna use an item?");
        }

    }
    
    private void passthroughInput(InputEvent @event)
    {

    }

    private void ItemMenuInputs(InputEvent @event)
    {
        GD.Print("Do you wanna use an item?");
        if (@event.IsActionPressed("yes"))
        {
            bool hasItems = currentPlayer.itemList.Any();
            if (hasItems)
            {
                GD.Print("What item do you wanna use:");
                PackedScene itemScene = (PackedScene)ResourceLoader.Load("res://Scenes/selectUseItem.tscn");

                // Instantiëren van de scène
                Node itemInstance = itemScene.Instantiate();

                // Voeg de instantie van de scène toe aan de hoofdscene
                AddChild(itemInstance);

                // Verkrijg toegang tot het script van de geïnstantieerde scène
                SelectUseItem itemScript = (SelectUseItem)itemInstance;

                // Roep de Initialize-methode aan om gegevens in te stellen
                itemScript.Initialize(currentPlayer);


            }
            else
            {
                GD.Print("You have no items to use");
                currentInputMode = InputMode.Dice;
            }

        }
        else if (@event.IsActionPressed("no"))
        {
            GD.Print("Pressed no");
            currentInputMode = InputMode.Dice;
        }
        
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
}
