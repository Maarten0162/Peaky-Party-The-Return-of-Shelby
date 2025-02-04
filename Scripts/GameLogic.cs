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


    Camera camera;
    private PackedScene Playerscene = (PackedScene)GD.Load("res://Scenes/Game Objects/Player.tscn");
    PackedScene itemScene = (PackedScene)ResourceLoader.Load("res://Scenes/selectUseItem.tscn");
    PackedScene diceScene = (PackedScene)ResourceLoader.Load("res://Scenes/selectUseDice.tscn");

    //hud scenes
    PackedScene TurnHudScene = (PackedScene)ResourceLoader.Load("res://Scenes/UI/TurnHUD.tscn");
    Node turnhud;
    Hud turnHudClass;
    TextureButton ItemButton;

    List<Dice> DiceList = new();

    SelectUseItem itemScript;
    SelectUseDice diceScript;
    Node itemInstance;
    Node diceInstance;
    Board Board;
    private Player player1;
    private Player player2;
    private Player player3;
    private Player player4;
    private List<Player> PList
    {
        get { return GlobalVar.Plist; }
        set { GlobalVar.Plist = value; }
    }
    int whatPlayer;
    Player currentPlayer;
    private int TurnCount
    {
        get
        {
            return GlobalVar.TurnCount;
        }
        set
        {
            GlobalVar.TurnCount = value;
        }
    }

    //dices
    Dice normalDice = new(1, 4);
    Dice betterDice = new(1, 7);
    Dice SuperDice = new(1, 8);
    Dice peakyDice = new(1, 12);

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
        camera = GetNode<Camera>("Camera/Camera2D");
        Board = GetNode<Board>("Board");
        camera.SetBoard(Board);
        DiceList.Add(normalDice);
        DiceList.Add(betterDice);
        DiceList.Add(SuperDice);
        DiceList.Add(peakyDice);



        
        HBoxContainer hud = GetNode<HBoxContainer>("Camera/CanvasLayer/AllHuds/HBoxContainer");
        if (TurnCount == 0)
        {

            int playeramount = CreatePlayers(4); // hier moet aantal spelers dat gekozen is, wrs global variable, maar natuurlijk proberen die zo min mogelijk te gebruiken.
            CreatePlayerOrder(playeramount);
            for (int i = 0; i < PList.Count; i++)
            {
                SetPlayerPos(PList[i], Board.spacesInfo[50].SpacePos);
                PlayerHud phud = (PlayerHud)hud.GetChild(i);
                phud.AddPlayer(PList[i]);
                PList[i].Sethud(phud);
                PList[i].hud.Update();
            }
            for (int i = PList.Count; i < hud.GetChildCount(); i++)
            {
                Node child = hud.GetChild(i);
                child.QueueFree();

            }

        }
        else
        {
            Board = SaveManager.LoadBoard();
            PList = SaveManager.LoadPlayers();
            CreatePlayers(PList.Count);
            for (int i = 0; i < PList.Count; i++)
            {
                SetPlayerPos(PList[i], Board.spacesInfo[50].SpacePos);
                PlayerHud phud = (PlayerHud)hud.GetChild(i);
                phud.AddPlayer(PList[i]);
                PList[i].Sethud(phud);
                PList[i].hud.Update();
            }
            for (int i = PList.Count; i < hud.GetChildCount(); i++)
            {
                Node child = hud.GetChild(i);
                child.QueueFree();

            }
        }

        whatPlayer = 0;
        currentPlayer = PList[whatPlayer];

        Turn();

    }
    private void SetPlayerPos(Player player, Vector2 space)
    {   
        player.Position = space;
        GD.Print(player.Position);
        
        GD.Print(player.Position);
        player.currSpace = 51;
       
    }
    public int CreatePlayers(int amount)
    {
        int players = 0;
        // foreach (Player player in PList) later ff uitzoeken omdit met een foreach te doen.
        // {
        //     PList[players] = (Player)Playerscene.Instantiate();
        //     AddChild(player);
        //     BallAndChain bac = new();
        //     BallAndChain bac2 = new();
        //     BallAndChain bac3 = new();
        //     BallAndChain bac4 = new();
        //     BallAndChain bac5 = new();
        //     PList[players].itemList.Add(bac);
        //     player.itemList.Add(bac2);
        //     PList[players].itemList.Add(bac3);
        //     PList[players].itemList.Add(bac4);
        //     PList[players].itemList.Add(bac5);
        //     players++;
        // }

        if (amount >= 1)
        {
            player1 = (Player)Playerscene.Instantiate();
            AddChild(player1);
            BallAndChain bac = new();
            BallAndChain bac2 = new();
            BallAndChain bac3 = new();
            BallAndChain bac4 = new();
            BallAndChain bac5 = new();
            player1.itemList.Add(bac);
            player1.itemList.Add(bac2);
            player1.itemList.Add(bac3);
            player1.itemList.Add(bac4);
            player1.itemList.Add(bac5);
            players++;
            
        }
        if (amount >= 2)
        {
            player2 = (Player)Playerscene.Instantiate();
            AddChild(player2);
            BallAndChain bac = new();
            BallAndChain bac2 = new();
            BallAndChain bac3 = new();
            BallAndChain bac4 = new();
            BallAndChain bac5 = new();
            player2.itemList.Add(bac);
            player2.itemList.Add(bac2);
            player2.itemList.Add(bac3);
            player2.itemList.Add(bac4);
            player2.itemList.Add(bac5);
            players++;
        }
        if (amount >= 3)
        {
            player3 = (Player)Playerscene.Instantiate();
            AddChild(player3); ;
            BallAndChain bac = new();
            BallAndChain bac2 = new();
            BallAndChain bac3 = new();
            BallAndChain bac4 = new();
            BallAndChain bac5 = new();
            player3.itemList.Add(bac);
            player3.itemList.Add(bac2);
            player3.itemList.Add(bac3);
            player3.itemList.Add(bac4);
            player3.itemList.Add(bac5);
            players++;
        }
        if (amount >= 4)
        {
            player4 = (Player)Playerscene.Instantiate();
            AddChild(player4);
            BallAndChain bac = new();
            BallAndChain bac2 = new();
            BallAndChain bac3 = new();
            BallAndChain bac4 = new();
            BallAndChain bac5 = new();
            player4.itemList.Add(bac);
            player4.itemList.Add(bac2);
            player4.itemList.Add(bac3);
            player4.itemList.Add(bac4);
            player4.itemList.Add(bac5);
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
        GlobalVar.Plist = PList;
    }



    private void Turn()
    {   camera.FollowPlayer(currentPlayer);
        currentPlayer.AddPassiveItem(new Gamblers_Wealth());
        currentPlayer.EarnIncome();
        currentPlayer.UseStartPItems();
        
        if (currentPlayer.isAlive && !currentPlayer.SkipTurn)
        {
            GD.Print("do you want to use an item?");
            Label label = GetNode<Label>($"{currentPlayer.Name}/Label");
            label.Text = $"{whatPlayer + 1}";
            openTurnHudMenu();

        }
        else GD.Print("player is dead or has to skip a turn");
    }
    private void NextTurn()
    {
        whatPlayer++;
        if (whatPlayer >= PList.Count)
        {
            whatPlayer = 0;
        }
        currentPlayer = PList[whatPlayer];
        Turn();
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
                //\\selectUseItem.getInput(@event);
                break;
        }
    }
    private void DiceMenuInputs(InputEvent @event)
    {

        diceInstance = diceScene.Instantiate();

        // Voeg de instantie van de scène toe aan de hoofdscene
        AddChild(diceInstance);

        // Verkrijg toegang tot het script van de geïnstantieerde scène
        diceScript = (SelectUseDice)diceInstance;

        diceScript.Connect("SelectionMade", Callable.From((Dice dice) => OnDiceSelected(dice)));
        // Roep de Initialize-methode aan om gegevens in te stellen
        diceScript.Initialize(DiceList);
        currentInputMode = InputMode.None;

    }
    private async void UseDice()
    {
        GD.Print("in use dice");
        int roll = normalDice.Roll();
        if (currentPlayer.CheckRollAdjust(roll))
        {
            GD.Print("hier");

            roll += currentPlayer.RollAdjust;
            int target = currentPlayer.currSpace + roll;
            GD.Print($"You threw {target - currentPlayer.currSpace}");
            await currentPlayer.Movement(Board, roll);

            NextTurn();

        }
        else
        {
            GD.Print("Sorry you cant move with these legs");
            NextTurn();
        }
    }

    private void ShopInputs(InputEvent @event)
    {
        if (@event.IsActionPressed("yes"))
        {


        }
        else if (@event.IsActionPressed("no"))
        {
            GD.Print("No pressed");

        }
    }



    private void ItemMenuInputs(InputEvent @event)
    {
        bool hasItems = currentPlayer.itemList.Any();
        if (hasItems)
        {

            GD.Print("What item do you wanna use:");

            // Instantiëren van de scène
            itemInstance = itemScene.Instantiate();

            // Voeg de instantie van de scène toe aan de hoofdscene
            AddChild(itemInstance);

            // Verkrijg toegang tot het script van de geïnstantieerde scène
            itemScript = (SelectUseItem)itemInstance;

            itemScript.Connect("customSignal", Callable.From(OnItemUsed));
            // Roep de Initialize-methode aan om gegevens in te stellen
            itemScript.Initialize(currentPlayer);
            currentInputMode = InputMode.None;



        }
        else
        {
            GD.Print("You have no items to use");
            currentInputMode = InputMode.Dice;
        }

    }

    private async void OnItemUsed()
    {
        await Task.Delay(100);
        GD.Print("Item has been used.");
        itemInstance.Disconnect("customSignal", Callable.From(OnItemUsed));
        itemInstance.QueueFree();
        currentInputMode = InputMode.None;
        openTurnHudMenu();
        ItemButton.Disabled = true;


    }


    //hudcode
    public void openTurnHudMenu()
    {
        turnhud = (Hud)TurnHudScene.Instantiate();
        GetNode("Camera/CanvasLayer").AddChild(turnhud);

        ItemButton = turnhud.GetNode<TextureButton>("VBoxContainer/ItemButton");
        turnhud.Connect("HudSelection", Callable.From((string message) => OnHudSelection(message)));

        currentInputMode = InputMode.None;

    }

    private async Task OnHudSelection(string message)
    {
        switch (message)
        {
            case "DICE":
                currentInputMode = InputMode.Dice;
                turnhud.QueueFree();
                break;
            case "ITEM":
                GD.Print($"Received signal with message: {message} ");
                currentInputMode = InputMode.Item;
                break;
            case "PLAYERS":
                currentInputMode = InputMode.Dice;
                break;
            case "MAP":
            turnhud.QueueFree();
               await camera.Freecam();
                
                break;
        }


    }

    private async void OnDiceSelected(Dice dice)
    {

        GD.Print("in use dice");
        int roll = dice.Roll();
        if (currentPlayer.CheckRollAdjust(roll))
        {


            GD.Print($"You threw {roll}");
            diceInstance.QueueFree();
            await currentPlayer.Movement(Board, roll);
            NextTurn();

        }
        else
        {
            GD.Print("Sorry you cant move with these legs");
            diceInstance.QueueFree();
            await Task.Delay(100);
            NextTurn();
        }
    }

}
