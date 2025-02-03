using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public partial class ButtonMash : Minigame
{
    // Renamed signal to avoid conflict with method name
    [Signal] public delegate void ButtonReleasedSignalEventHandler(CharacterBody2D player, bool RightButton);

    bool GameFinished = false;
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

    public override void _Ready()
    {
        player1 = GetNode<CharacterBody2D>("CharacterBody2D");
        players.Add(player1);

        // Connect the signal to the method
        Connect("ButtonReleasedSignal", Callable.From((CharacterBody2D player, bool RightButton) => ButtonReleased(player, RightButton)));
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

    public override async void _Process(double delta)
    {
        await checkKeyInput(player1);
    }

    private void chooseRndButton()
    {
        currentKey newkey = currentKey.A;
        for (int i = 0; i < 10; i++)
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
        GD.Print(keyList.Count);
    }

    private Dictionary<CharacterBody2D, bool> lastPressedState = new();
    private Dictionary<CharacterBody2D, bool> canPressAgain = new();

    private async Task checkKeyInput(CharacterBody2D player)
    {
        if (GameFinished) return;

        string inputName = "";
        string playerName = "";

        if (player == player1) playerName = "PL1_";
        else if (player == player2) playerName = "PL2_";
        else if (player == player3) playerName = "PL3_";
        else if (player == player4) playerName = "PL4_";

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
            player.Position += new Vector2(30, 0);
            EmitSignal(nameof(ButtonReleasedSignal), player, true);

            lastPressedState[player] = true;
            canPressAgain[player] = false;
        }

        // **Detect wrong button press**
        if (Input.IsAnythingPressed() && !isCorrectPressed && !lastPressedState[player] && canPressAgain[player])
        {
            GD.Print($"{player.Name} pressed the wrong button!");

            player.Position -= new Vector2(30, 0); // Move player backward as penalty
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



    private void ButtonReleased(CharacterBody2D player, bool RightButton)
    {
        GD.Print($"{player.Name} pressed the {RightButton} button");
    }
}