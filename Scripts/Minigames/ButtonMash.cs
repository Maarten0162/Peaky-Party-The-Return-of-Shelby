using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

public partial class ButtonMash : Minigame
{
    // Renamed signal to avoid conflict with method name
    [Signal] public delegate void ButtonReleasedSignalEventHandler(CharacterBody2D player, bool RightButton);

    bool GameFinished = false;
    int playerCount;
    private Random rnd = new();
    private enum currentKey
    {
        A,
        B,
        X,
        Y
    }
    private currentKey CurrentKey;

    private List<currentKey> keyList = new();

    private List<CharacterBody2D> players = new();
    CharacterBody2D player1;
    int ScorePL1 = 0;
    CharacterBody2D player2;
    int ScorePL2 = 0;
    CharacterBody2D player3;
    int ScorePL3 = 0;
    CharacterBody2D player4;
    int ScorePL4 = 0;
    CollisionShape2D coliShape;

    float movementAmount;
    public ButtonMash()
    {
        playerCount = PList.Count;
    }
    public override void _Ready()
    {
        CreatePlayer();
        coliShape = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
        chooseRndButton();
        switch (CurrentKey)
        {
            case currentKey.A:
                break;
            case currentKey.B:
                break;
            case currentKey.X:
                break;
            case currentKey.Y:
                break;
        }
    }

    public override void _Process(double delta)
    {
        checkKeyInput(player1);
        checkKeyInput(player2);
        // checkKeyInput(player3);
        // checkKeyInput(player4);
    }

    private void chooseRndButton()
    {
        currentKey newkey = currentKey.A;
        keyList.Add(newkey);

        for (int i = 0; i < 9; i++)
        {
            int ii = rnd.Next(0, 4);

            switch (ii)
            {
                case 0:
                    newkey = currentKey.A;
                    break;
                case 1:
                    newkey = currentKey.B;
                    break;
                case 2:
                    newkey = currentKey.X;
                    break;
                case 3:
                    newkey = currentKey.Y;
                    break;
            }
            keyList.Add(newkey);

        }
        for (int i = 0; i < 9; i++)
        {
            GD.Print(keyList[i]);
        }

        movementAmount = (coliShape.Position.X - player1.Position.X) / keyList.Count;

    }

    private Dictionary<CharacterBody2D, bool> lastPressedState = new();
    private Dictionary<CharacterBody2D, bool> canPressAgain = new();

    private async void checkKeyInput(CharacterBody2D player)
    {
        if (GameFinished) return;

        string inputName = "";
        string playerName = "";
        int Score = 0;

        if (player == player1)
        {
            playerName = "PL1_";
            Score = ScorePL1;
        }
        else if (player == player2)
        {
            playerName = "PL2_";
            Score = ScorePL2;
        }
        else if (player == player3)
        {
            playerName = "PL3_";
            Score = ScorePL3;
        }
        else if (player == player4)
        {
            playerName = "PL4_";
            Score = ScorePL4;
        }

        CurrentKey = keyList[Score];


        switch (CurrentKey)
        {
            case currentKey.A: inputName = "A"; break;
            case currentKey.B: inputName = "A"; break;
            case currentKey.X: inputName = "A"; break;
            case currentKey.Y: inputName = "A"; break;
        }

        string correctInput = playerName + inputName;
        bool isCorrectPressed = Input.IsActionPressed(correctInput);

        // Ensure the player is in tracking dictionaries
        if (!lastPressedState.ContainsKey(player)) lastPressedState[player] = false;
        if (!canPressAgain.ContainsKey(player)) canPressAgain[player] = true;

        // **Detect correct button press**
        if (isCorrectPressed && !lastPressedState[player] && canPressAgain[player])
        {
            if (Score < keyList.Count - 1) Score++;
            if (player == player1) ScorePL1 = Score;
            else if (player == player2) ScorePL2 = Score;
            else if (player == player3) ScorePL3 = Score;
            else if (player == player4) ScorePL4 = Score;
            player.Position += new Vector2(movementAmount, 0);
            EmitSignal(nameof(ButtonReleasedSignal), player, true);

            lastPressedState[player] = true;
            canPressAgain[player] = false;
        }

        // **Detect wrong button press**
        if (Input.IsAnythingPressed() && !isCorrectPressed && !lastPressedState[player] && canPressAgain[player])
        {      
            if (!Input.IsActionJustPressed(playerName + "A") || !Input.IsActionJustPressed(playerName + "_B") ||
             !Input.IsActionJustPressed(playerName + "X") || !Input.IsActionJustPressed(playerName + "_Y"))
            {
                return;
            }
            if (Score >= 1)
            {
                Score--;
                player.Position -= new Vector2(movementAmount, 0); // Move player backward as penalty
            }
            if (player == player1) ScorePL1 = Score;
            else if (player == player2) ScorePL2 = Score;
            else if (player == player3) ScorePL3 = Score;
            else if (player == player4) ScorePL4 = Score;

            EmitSignal(nameof(ButtonReleasedSignal), player, false);

            lastPressedState[player] = true;
            canPressAgain[player] = false;
            await Task.Delay(1500); // Small delay to prevent spam
            canPressAgain[player] = true;
        }

        // Handle key release
        if (!isCorrectPressed && lastPressedState[player])
        {
            lastPressedState[player] = false;
            await Task.Delay(200);
            canPressAgain[player] = true;
        }
    }

    private void OnFinishEntered(Node2D node)
    {
        if (node == player1) PList[0].isWinner = true;
        else if (node == player2) PList[1].isWinner = true;
        else if (node == player3) PList[2].isWinner = true;
        else if (node == player4) PList[3].isWinner = true;
        GameFinished = true;

        GD.Print($"The winner is {node}");
    
        CallDeferred("ChangeToSession");


    }
    private void ChangeToSession()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Session.tscn");
    }

    private void CreatePlayer()
    {
        if (playerCount >= 1)
        {
            player1 = GetNode<CharacterBody2D>("CharacterBody2D");
            players.Add(player1);
            player1.Position = new Vector2(58, 200);
        }
        if (playerCount >= 2)
        {
            player2 = GetNode<CharacterBody2D>("CharacterBody2D2");
            players.Add(player2);
            player2.Position = new Vector2(58, player1.Position.Y + 200);
        }
        if (playerCount >= 3)
        {
            player3 = GetNode<CharacterBody2D>("CharacterBody2D3");
            players.Add(player3);
            player3.Position = new Vector2(58, player1.Position.Y + 400);
        }
        if (playerCount >= 4)
        {
            player4 = GetNode<CharacterBody2D>("CharacterBody2D4");
            players.Add(player4);
            player4.Position = new Vector2(58, player1.Position.Y + 600);
        }
    }




}