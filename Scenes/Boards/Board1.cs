using Godot;
using System;

public partial class Board1 : Node2D
{

	private (Vector2 SpacePos, int Number, string Name, string OriginalName)[] spacesInfo;
	private Line2D path;
	private Player tempplayer;
	private int currentSpaceIndex; // Track which marker the player is at
	private float moveSpeed = 200f; // Movement speed (pixels per second)
	private bool isMoving = false; // To control when to move the player
	int target;
	[Export]
	int playerstartposition;
	public override void _Ready()
	{
		tempplayer = GetNode<Player>("Player");

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
				spacesInfo[x] = (marker.Position, x + 1, marker.Name, marker.Name);
				GD.Print($"Point {x}: {spacesInfo[x].SpacePos}");
				
				x++;
			}
		}
		path.Points = new Vector2[spacesInfo.Length];
		for (int i = 0; i < spacesInfo.Length; i++)
		{
			path.Points[i] = spacesInfo[i].SpacePos;
		}


		path.DefaultColor = Colors.Red;
		path.Width = 1;
		path.Visible = true;
		path.ZIndex = 1;
		foreach (var space in spacesInfo)
		{

			GD.Print($"Space position: {space.SpacePos}, space Number: {space.Number} Name: {space.Name}, Original Name: {space.OriginalName}");
		}
		tempplayer.Position = spacesInfo[playerstartposition].SpacePos;
		tempplayer.currSpace = playerstartposition+ 1;
		GD.Print(tempplayer.Position);
		tempplayer.GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
		
		// Movement(4, tempplayer);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (isMoving && tempplayer.currSpace < target)
		{


			Vector2 targetPosition = spacesInfo[tempplayer.currSpace].SpacePos;

			Vector2 direction = targetPosition - tempplayer.Position;
			float distance = direction.Length();
			if (distance > 1f) // Stop moving once we are close enough
			{
				tempplayer.Position += direction.Normalized() * moveSpeed * (float)GetProcessDeltaTime();
			}
			else
			{
				// Once we reach the target marker, stop moving and update to the next marker
				tempplayer.Position = targetPosition; // Correct any small offset
				tempplayer.currSpace++; // Move to the next marker
				GD.Print("player current position is" + tempplayer.currSpace);
				// If we reached the last marker, stop the movement
				if (tempplayer.currSpace == target)
				{
					GD.Print(tempplayer.currSpace);
					isMoving = false; // Stop moving after reaching the last marker
					GD.Print($"Player Global Position: {tempplayer.GlobalPosition}");
GD.Print($"Marker Global Position: {spacesInfo[tempplayer.currSpace -1 ].SpacePos}");
				}

			}
		}
	}
	private void Movement(int diceroll, Player player)
	{
		isMoving = true;
		target = player.currSpace + diceroll;
	}
	public override void _Input(InputEvent @event)
{
   // Check if the input event is a key press
    if (@event is InputEventKey keyEvent)
    {
        // Check if the '1' key is pressed
        if (keyEvent.Keycode == Key.Key1 && keyEvent.Pressed)
        {
            GD.Print("Key 1 Pressed");
            // Call the movement function with a sample diceroll (e.g., 1)
            Movement(1, tempplayer); 
        }
    }
}
}
