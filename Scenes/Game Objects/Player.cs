using Godot;
using System;

public partial class Player : CharacterBody2D
{
	int Index;
	int Health;
	public Player(int _index, int _health)
	{
		Index = _index;
		Health = _health;
	}
	public override void _PhysicsProcess(double delta)
	{
		
	}
}
