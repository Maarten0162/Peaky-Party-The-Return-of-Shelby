using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class PlayerHud : Control
{
	Player player;
	Label Income;
	Label Currency;
	new Label Name;
	Label Health;
	Label Rolladjust;


	public override void _Ready()
	{

		Income = GetNode<Label>("IncomeLabel");       // Adjust the path if needed
		Currency = GetNode<Label>("CurrencyLabel");   // Adjust the path if needed
		Name = GetNode<Label>("NameLabel");           // Adjust the path if needed
		Health = GetNode<Label>("HealthLabel");       // Adjust the path if needed
		Rolladjust = GetNode<Label>("RolladjustLabel");
	}
	public void AddPlayer(Player _player)
	{
		this.player = _player;
		
			

	}

	public void Update()
	{	if(player != null){
		GD.Print("player not null");
	}
		GD.Print(player.Income + "is income");
		
		Income.Text = player.Income.ToString();
		Currency.Text = player.Currency.ToString();
		Health.Text = player.Health.ToString();
		Rolladjust.Text = player.RollAdjust.ToString();
	}
}
