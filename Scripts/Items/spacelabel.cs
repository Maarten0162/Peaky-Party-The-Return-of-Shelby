using Godot;
using System;
using System.Text.RegularExpressions;

public partial class spacelabel : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Node parent = GetParent();
		Regex regex = new(@"\d+$");
		Match match = regex.Match(parent.Name);
		Text = match.ToString();
		AddThemeColorOverride("font_color", new Color(1.0f, 0.647f, 0)); // Green text
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
