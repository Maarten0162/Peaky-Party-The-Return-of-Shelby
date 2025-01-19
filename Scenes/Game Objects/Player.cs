using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private int Index;
	private int Health;
	private bool isAlive;
	private bool SkipTurn;

	public Player(int _index, int _health)
	{
		Index = _index;
		Health = _health;
		isAlive = true;
		SkipTurn = false;
	}
	public override void _PhysicsProcess(double delta)
	{
		if (Health <= 0)
		{
			isAlive = false;
		}
		
	}
}
