using Godot;
using System;
using System.Collections.Generic;

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
	public int rollAdjust;
	private List<Item> itemList;

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
		BallAndChain BAC = new ();
		Skin = _skin;
		Currency = 100;
		Health = 100;
		isAlive = true;
		SkipTurn = false;
		itemList.Add(BAC);
	}

	public Player()
	{
		BallAndChain BAC = new ();
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

	private void useItem(Item item, Player player)
	{
		if (player.itemList.Contains(item))
		{
			item.Use(player);
		}
		
	}
	
}
