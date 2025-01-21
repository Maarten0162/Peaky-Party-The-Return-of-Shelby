using Godot;
using System;
using System.Threading.Tasks;

public partial class Board : Node2D
{

	public (Vector2 SpacePos, int Number, string Name, string OriginalName)[] spacesInfo;
	private Line2D path;
	private Player tempplayer;
	private bool isMoving = false; // To control when to move the player
	int target;

	public override void _Ready()
	{
		path = GetNode<Line2D>("Path");
		if (path != null)
		{
			GD.Print("FOUND PATH");
		}

		int count = 0;
		foreach (Node child in path.GetChildren())
		{
			if (child is Marker2D)
			{
				count++;
			}
		}

		spacesInfo = new (Vector2, int, string, string)[count];

		int x = 0;
		foreach (Node Child in path.GetChildren())
		{
			if (Child is Marker2D marker)
			{

				spacesInfo[x] = (marker.Position, x + 1, Child.Name, Child.Name);
				GD.Print($"Point {x}: {spacesInfo[x].SpacePos}");

				x++;
			}
		}
		path.Points = new Vector2[spacesInfo.Length];
		for (int i = 0; i < spacesInfo.Length; i++)
		{
			path.Points[i] = spacesInfo[i].SpacePos;
		}


		
		foreach (var space in spacesInfo)
		{

			GD.Print($"Space position: {space.SpacePos}, space Number: {space.Number} Name: {space.Name}, Original Name: {space.OriginalName}");
		}

	}


}
