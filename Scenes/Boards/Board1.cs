using Godot;
using System;

public partial class Board1 : Node2D
{

	private (Node2D Space, string Name, string OriginalName)[] spacesInfo;
	private Line2D path;
	private Player tempplayer;
	public override void _Ready()
	{	
		tempplayer = new(0,100);

		path = GetNode<Line2D>("Path");

		int count = 1;
		foreach (Node child in path.GetChildren())
		{
			if (child is Marker2D)
			{
				count++;
			}
		}

		spacesInfo = new (Node2D, string, string)[count];

		int x = 1;
		foreach (Node Child in path.GetChildren())
		{
			if (Child is Marker2D marker)
			{
				spacesInfo[x] = (marker, marker.Name, marker.Name);
				x++;
			}
		}
		path.Points = new Vector2[spacesInfo.Length];
		for (int i = 0; i < spacesInfo.Length; i++)
		{
			path.Points[i] = spacesInfo[i].Space.Position;
		}
		foreach (var space in spacesInfo)
		{

			GD.Print($"Space: {space.Space}, Name: {space.Name}, Original Name: {space.OriginalName}");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
