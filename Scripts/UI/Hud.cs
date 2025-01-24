using Godot;
using System;

public partial class Hud : Control
{
	// Called when the node enters the scene tree for the first time.
	TextureButton DiceButton;
	TextureButton ItemButton;
	TextureButton PlayerButton;
	TextureButton MapButton;
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
			if (button.HasFocus())
			{
				button.TextureNormal = GD.Load<Texture2D>("res://Assets/UI/TurnUIButtonFocused.png");
			}
			else
			{
				button.TextureNormal = GD.Load<Texture2D>("res://Assets/UI/TurnUIButton.png");
			}
		}

	}
}
