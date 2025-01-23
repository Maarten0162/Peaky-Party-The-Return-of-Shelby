using Godot;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Threading.Tasks;

public partial class Player : CharacterBody2D
{
	//private vars
	private int Attrubute;
	private int Health;
	public bool isAlive { get; private set; }
	public bool SkipTurn { get; private set; }
	private int Currency;
	public int currSpace;
	private bool isMoving;

	[Export] private float moveSpeed; // Movement speed (pixels per second)


	//debuffs
	public int rollAdjust = 0;
	public List<ActiveItem> itemList { get; private set; } = new List<ActiveItem>();

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
		BallAndChain BAC = new();
		Skin = _skin;
		Currency = 100;
		Health = 100;
		isAlive = true;
		SkipTurn = false;
		itemList.Add(BAC);
	}

	public Player()
	{
		BallAndChain BAC = new();
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

	public async Task<bool> Movement(Board board, int target)
	{


		while (currSpace < target)
		{

			Vector2 targetPosition = board.spacesInfo[currSpace].SpacePos;

			Vector2 direction = targetPosition - Position;
			float distance = direction.Length();
			if (distance > 1f) // Stop moving once we are close enough
			{
				Position += direction.Normalized() * moveSpeed * (float)GetProcessDeltaTime();
			}
			else
			{
				Position = targetPosition;
				currSpace++;

				if (currSpace == board.spacesInfo.Length && currSpace < target)
				{
					target = target - board.spacesInfo.Length;
					currSpace = 0;
				}


				GD.Print("player current position is" + currSpace);
			}
			await Task.Delay(10);
		}


		return true;
	}
	public bool CheckRollAdjust(int diceroll)
	{
		if (rollAdjust < 0)
		{
			if ((rollAdjust + diceroll) <= 0)
			{
				rollAdjust = 0;
				return false;
			}
			else return true;
		}
		else
		{
			return true;
		}
	}

	public void Attack()
	{
		//
	}

	public void takeDamage()
	{
		//
	}

	private void useItem(ActiveItem item, Player player)
	{
		if (player.itemList.Contains(item))
		{
			item.Use(player);
		}

	}

}
