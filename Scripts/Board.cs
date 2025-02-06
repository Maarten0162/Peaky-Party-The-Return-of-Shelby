using Godot;
using System;
using System.Threading.Tasks;

public partial class Board : Node2D
{

	public (Vector2 SpacePos, Marker2D Node, int Number, string Name, string OriginalName)[] spacesInfo;
	public Line2D path;


	public override void _Ready()
	{
		path = GetNode<Line2D>("Path");
		

		int count = 0;
		foreach (Node child in path.GetChildren())
		{
			if (child is Marker2D)
			{
				count++;
			}
		}

		spacesInfo = new (Vector2,Marker2D, int, string, string)[count];

		int x = 0;
		foreach (Node Child in path.GetChildren())
		{
			if (Child is Marker2D marker)
			{

				spacesInfo[x] = (this.ToGlobal(marker.Position), marker, x + 1, Child.Name, Child.Name);
				GD.Print(spacesInfo[x]);


				x++;
			}
		}
		path.Points = new Vector2[spacesInfo.Length];
		for (int i = 0; i < spacesInfo.Length; i++)
		{
			path.Points[i] = spacesInfo[i].SpacePos;
		}
	}

 

}
