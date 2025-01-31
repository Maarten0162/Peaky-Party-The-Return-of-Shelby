using Godot;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

public partial class Player : CharacterBody2D
{
	//private vars
	private int Attribute;
	private int health;

	public int Health
	{
		get { return health; }
		set
		{
			health = value;
			if (hud != null)
			{
				Update();
			}
		}
	}
	private int rollAdjust;
	public int RollAdjust
	{
		get { return rollAdjust; }
		set
		{
			rollAdjust = value;
			if (hud != null)
			{
				Update();
			}
		}
	}
	private int income;
	public int Income
	{
		get { return income; }
		set
		{
			income = value;
			if (hud != null)
			{
				Update();
			}
		}
	}
	private int currency;
	public int Currency
	{
		get { return currency; }
		set
		{
			currency = value;
			if (hud != null)
			{
				Update();
			}
		}
	}

	public bool isAlive { get; private set; }
	public bool SkipTurn { get; private set; }
	public PlayerHud hud { get; private set; }


	private bool isMoving;

	[Export] private float moveSpeed; // Movement speed (pixels per second)


	//debuffs

	// Active & Passive items
	public List<ActiveItem> itemList { get; private set; } = new();
	public List<PassiveItem> AllPassiveItems { get; private set; } = new();
	public List<PassiveItem> StartPassiveItems = new();
	public List<PassiveItem> MovingPassiveItems = new();
	public List<PassiveItem> EndTurnPassiveItems = new();
	public List<PassiveItem> PassingPassiveItems = new();
	public List<PassiveItem> TakeDamagePassiveItems = new();
	public List<PassiveItem> HealPassiveItems = new();
	public List<PassiveItem> ObtainCurrencyPassiveItem = new();
	public List<PassiveItem> LoseCurrencyPassiveItem = new();
	public List<PassiveItem> RollAdjustChangePassiveItem = new();
	public List <PassiveItem> IncomeChangePassiveItem = new();
	public void AddPassiveItem(PassiveItem item)
	{
		AllPassiveItems.Add(item);
		item.SetOwner(this);
		item.RunOnPickup();
		hud.AddPassiveToHud(item);
		foreach (PassiveItem.WhenActive action in item.WhenToRun)
		{
			switch (action)
			{
				case PassiveItem.WhenActive.EndofTurn:
					EndTurnPassiveItems.Add(item);
					break;

				case PassiveItem.WhenActive.StartofTurn:
					StartPassiveItems.Add(item);
					break;
				case PassiveItem.WhenActive.WhenMoving:
					MovingPassiveItems.Add(item);
					break;
				case PassiveItem.WhenActive.PassingPlayer:
					PassingPassiveItems.Add(item);
					break;
				case PassiveItem.WhenActive.takeDamage:
					TakeDamagePassiveItems.Add(item);
					break;
				case PassiveItem.WhenActive.Heal:
					HealPassiveItems.Add(item);
					break;
				case PassiveItem.WhenActive.ObtainCurrency:
					ObtainCurrencyPassiveItem.Add(item);
					break;
				case PassiveItem.WhenActive.LostCurrency:
					LoseCurrencyPassiveItem.Add(item);
					break;
				case PassiveItem.WhenActive.RollAdjustChange:
					RollAdjustChangePassiveItem.Add(item);
					break;
				case PassiveItem.WhenActive.IncomeChanged:
					IncomeChangePassiveItem.Add(item);
					break;


			}
		}
	}
	public void UseStartPItems()
	{
		foreach (PassiveItem item in StartPassiveItems)
		{
			item.RunOnStartofTurn();
		}
	}
	public void UseMovingPItems()
	{
		foreach (PassiveItem item in MovingPassiveItems)
		{
			item.RunOnMoving();
		}
	}
	public void UseEndTurnPItems()
	{
		foreach (PassiveItem item in EndTurnPassiveItems)
		{
			item.RunOnEndofTurn();
		}
	}
	public void UsePassingPItems()
	{
		foreach (PassiveItem item in PassingPassiveItems)
		{
			item.RunOnPassingPlayer();
		}
	}
	public void UseTakenDamageItems(int damage)
	{
		foreach (PassiveItem item in TakeDamagePassiveItems)
		{
			item.RunOnTakingDamage(damage);
		}
	}
	public void UseHealPassiveItems(int amounthealed)
	{
		foreach (PassiveItem item in HealPassiveItems)
		{
			item.RunOnHeal(amounthealed);
		}
	}
	public void UseObtainCurrencyPassiveItems(int amount)
	{
		foreach (PassiveItem item in ObtainCurrencyPassiveItem)
		{
			item.RunOnObtainCurrency(amount);
		}
	}
	public void UseLoseCurrencyPassiveItems(int amount){
		foreach(PassiveItem item in LoseCurrencyPassiveItem){
			item.RunOnLoseCurrency(amount);
		}
	}
	public void UseRollAdjustChangePassiveItems(int amount){
		foreach(PassiveItem item in RollAdjustChangePassiveItem){
			item.RunOnChangeRolladjust(amount);
		}
	}
	public void UseIncomeChangePassiveItem(int amount){
		foreach(PassiveItem item in IncomeChangePassiveItem){
			item.RunOnIncomeChange(amount);
		}
	}

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
	public async Task Movement(Board board, int roll)
	{
		roll += rollAdjust;

		if (roll > 0)
		{
			if (roll > board.spacesInfo.Length)
			{
				roll = roll - board.spacesInfo.Length;
			}
			roll += currSpace;
			await PositiveMovement(board, roll);
		}
		else
		{
			roll += currSpace;
			if (roll < 0)
			{
				roll = board.spacesInfo.Length + roll;
			}
			await NegativeMovement(board, roll);
		}
	}
	public async Task PositiveMovement(Board board, int target)
	{


		while (currSpace != target)
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

					currSpace = 0;
				}


				GD.Print("player current position is" + currSpace);
			}
			await Task.Delay(10);
		}
	}
	public async Task NegativeMovement(Board board, int target)
	{
		while (currSpace != target - 1)
		{
			GD.Print("target is " + target);
			GD.Print("currspace is" + currSpace);
			Vector2 targetPosition = board.spacesInfo[currSpace - 1].SpacePos;
			Vector2 direction = targetPosition - Position;
			float distance = direction.Length();

			if (distance > 1f) // Stop moving once he is close enough
			{
				Position += direction.Normalized() * moveSpeed * (float)GetProcessDeltaTime();
			}
			else
			{
				Position = targetPosition;
				if (currSpace == 1)
				{
					currSpace = board.spacesInfo.Length;
				}
				else
				{
					currSpace--;
				}



				GD.Print("player current position is" + currSpace);
			}
			await Task.Delay(10);
		}
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
	public void EarnIncome()
	{
		ObtainCurrency(Income);
		Update();
	}

	public void Attack()
	{
		//
	}

	public void takeDamage(int amount)
	{
		health -= amount;
		UseTakenDamageItems(amount);
		Update();
	}
	public void Heal(int amount)
	{
		Health += amount;
		UseHealPassiveItems(amount);
	}
	public void ObtainCurrency(int amount)
	{
		currency += amount;
		UseObtainCurrencyPassiveItems(amount);
	}
	public void LoseCurrency(int amount){
		currency -= amount;
		UseLoseCurrencyPassiveItems(amount);
	}
	public void ChangeIncome(int amount){
		income += amount;
		UseIncomeChangePassiveItem(amount);
	}
	public void ChangeRollAdjust(int amount){
		rollAdjust += amount;
		UseRollAdjustChangePassiveItems(amount);
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
