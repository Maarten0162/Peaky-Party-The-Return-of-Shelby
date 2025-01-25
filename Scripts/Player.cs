using Godot;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Threading.Tasks;

public partial class Player : CharacterBody2D
{
	//private vars
	private int Attribute;
	public int Health { get; private set; }
	public bool isAlive { get; private set; }
	public bool SkipTurn { get; private set; }
	private int Currency;
	public int RollAdjust {get; private set;}
	public int Income { get; private set; }
	public PlayerHud hud { get; private set; }


	private bool isMoving;

	[Export] private float moveSpeed; // Movement speed (pixels per second)


	//debuffs

	public List<ActiveItem> itemList { get; private set; } = new List<ActiveItem>();

	//stats
	private int spacedMoved;
	private int miniGamesWon;
	private int GainedCurr;
	private int LostCurr;
	private int DamageDealt;




	//public vars
	public Sprite2D Skin;
	public int currSpace;
	//Temp Variables
	[Export]
	int StartIncome = 10;
	[Export]
	int StartCurrency = 100;
	[Export]
	int StartHealth = 100;

	public override void _Ready()
	{
		BallAndChain BAC = new();
		Currency = StartCurrency;
		Health = StartHealth;
		Income = StartIncome;
		isAlive = true;
		SkipTurn = false;
		Name = "Jeff";
	}
	public Player(Sprite2D _skin, string _name)
	{
		BallAndChain BAC = new();
		Skin = _skin;
		Currency = StartCurrency;
		Income = StartIncome;
		Health = StartHealth;
		isAlive = true;
		SkipTurn = false;
		itemList.Add(BAC);
		Name = _name;
	}

	public Player()
	{
		BallAndChain BAC = new();
		Currency = StartCurrency;
		Health = StartHealth;
		Income = StartIncome;
		isAlive = true;
		SkipTurn = false;
		Name = "Jeff";
	}

	public void Sethud(PlayerHud _hud)
	{
		this.hud = _hud;
	}
	public void Update()
	{
		hud.Update();
	}
	public async Task<bool> Movement(Board board, int target)
	{


		while (currSpace < target)
		{

			Vector2 targetPosition = board.spacesInfo[currSpace].SpacePos;

			Vector2 direction = targetPosition - Position;
			float distance = direction.Length();
			if (distance > 1f) // Stop moving once he is close enough
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
		if (RollAdjust < 0)
		{
			if ((RollAdjust + diceroll) <= 0)
			{
				RollAdjust = 0;
				Update();
				return false;
			}
			else return true;
		}
		else
		{
			return true;
		}
	}
	public void RolladjustChange(int i)
	{
		RollAdjust += i;
		Update();
	}
	public void HealthChange(int i)
	{	
		Health += i;
		Update();
	}
	public void CurrencyChange(int i)
	{
		Currency += i;
		Update();
	}
	public void IncomeChange(int incomechange)
	{
		Income += incomechange;
		Update();
	}
	public void EarnIncome()
	{
		Currency += Income;
		Update();
	}

	public void Attack()
	{
		//
	}

	public void takeDamage()
	{
		//
		Update();
	}

	private void useItem(ActiveItem item, Player player)
	{
		if (player.itemList.Contains(item))
		{
			item.Use(player);
			Update();
		}

	}

}
