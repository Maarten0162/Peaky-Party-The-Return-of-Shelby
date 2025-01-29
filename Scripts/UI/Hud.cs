using Godot;
using System;
using System.Threading.Tasks;

public partial class Hud : Control
{

	[Signal]
    public delegate void HudSelectionEventHandler(string InputModeText);

	TextureButton DiceButton;
	TextureButton ItemButton;
	TextureButton PlayerButton;
	TextureButton MapButton;

	TextureButton HighlightedButton;
	private VBoxContainer VBox;
	private Vector2 _normalScale = new Vector2(1, 1);  // Default scale
	private Vector2 _focusedScale = new Vector2(1.3f, 1f);

	public override void _Ready()
	{
		VBox = GetNode<VBoxContainer>("VBoxContainer");
		DiceButton = GetNode<TextureButton>("VBoxContainer/DiceButton");
		ItemButton = GetNode<TextureButton>("VBoxContainer/ItemButton");
		PlayerButton = GetNode<TextureButton>("VBoxContainer/PlayerButton");
		MapButton = GetNode<TextureButton>("VBoxContainer/MapButton");
		

		DiceButton.GrabFocus();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Check if the button is focused
		foreach (TextureButton button in VBox.GetChildren())
		{
			if (button.Disabled)
			{
				button.Modulate = new Color(0.2f, 0.2f, 0.2f, 1);
			}
			else button.Modulate = Colors.White;

			if (button.HasFocus())
			{
				button.TextureNormal = GD.Load<Texture2D>("res://Assets/UI/TurnUIButtonFocused.png");
				HighlightedButton = button;
			}
			else
			{
				button.TextureNormal = GD.Load<Texture2D>("res://Assets/UI/TurnUIButton.png");
			}
			
		}

	}

    public override void _Input(InputEvent @event)
    {
        if(@event.IsActionReleased("ui_accept"))
		{
			GD.Print("in ui input accept");
			if (HighlightedButton == DiceButton)
			{
				GD.Print("Dice Button clicked");
				EmitSignal(nameof(HudSelection), "DICE");
			}

			if (HighlightedButton == ItemButton)
			{
				GD.Print("Item Button clicked");
				EmitSignal(nameof(HudSelection), "ITEM");
			}

			if (HighlightedButton == PlayerButton)
			{
				GD.Print("Player Button clicked");
				EmitSignal(nameof(HudSelection), "PLAYERS");
			}

			if (HighlightedButton == MapButton)
			{
				GD.Print("Map Button clicked");
				EmitSignal(nameof(HudSelection), "MAP");
			}	
		}
    }
}
