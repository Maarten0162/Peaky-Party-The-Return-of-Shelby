using Godot;
using System;

public partial class Player : CharacterBody2D
{
	//private vars
	private int Attrubute;
	private int Health;
	private bool isAlive;
	private bool SkipTurn;
	private int Currency;
	private Vector2 currPos;
	public int currSpace;

	//debuffs
	private int rollAdjust;

	//stats
	private int spacedMoved;
	private int miniGamesWon;
	private int GainedCurr;
	private int LostCurr;
	private int DamageDealt;

	
	//public vars
	public Sprite2D Skin;

	public Player(Sprite2D _skin)
	{
		Skin = _skin;
		Currency = 100;
		Health = 100;
		isAlive = true;
		SkipTurn = false;
	}
	public Player()
	{

		Currency = 100;
		Health = 100;
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

	public int Movement()
	{
		return 1;
	}	

	public void Attack()
	{
		//
	}

	public void takeDamage()
	{
		//
	}
	
}
