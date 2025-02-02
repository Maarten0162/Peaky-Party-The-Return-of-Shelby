using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class ButtonMash : Minigame
{
    // Renamed signal to avoid conflict with method name
    [Signal] public delegate void ButtonReleasedSignalEventHandler(CharacterBody2D player, bool RightButton);

    private Random rnd = new();
    private enum currentKey
    {
        A,
        B,
        X,
        Y
    }
    private currentKey CurrentKey;

    private List<CharacterBody2D> players = new();
    CharacterBody2D player1;

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

    public override void _Process(double delta)
    {
        checkKeyInput(player1);
    }

    private void chooseRndButton()
    {
        int i = rnd.Next(0, 4);

        switch (i)
        {
            case 0:
                CurrentKey = currentKey.A;
                break;
            case 1:
                CurrentKey = currentKey.B;
                break;
            case 2:
                CurrentKey = currentKey.X;
                break;
            case 3:
                CurrentKey = currentKey.Y;
                break;
        }
    }

    private async void checkKeyInput(CharacterBody2D player)
    {
        string inputName = "";
        string playerName = "";
        if (player == player1)
        {
            playerName = "PL1_";
        }

        switch (CurrentKey)
        {
            case currentKey.A:
                inputName = "A";
                break;
            default:
                inputName = "A";
                break;
        }



        string input = playerName + inputName;
        
        if (Input.IsAnythingPressed())
        {
            if (Input.IsActionJustPressed(input))
            {
                player.Position += new Vector2(30, 0);          
                EmitSignal(nameof(ButtonReleasedSignal), player, true);
            }
            else
            {
                EmitSignal(nameof(ButtonReleasedSignal), player, false);
                player.Position += new Vector2(-30, 0);
                await Task.Delay(500);
            }
        }
    }
    private void ButtonReleased(CharacterBody2D player, bool RightButton)
    {
        GD.Print($"{player.Name} pressed the {RightButton} button");
    }
}