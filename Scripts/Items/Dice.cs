using Godot;
using System;

public partial class Dice : ActiveItem
{
	public int Min { get; private set; }
	public int Max { get; private set; }

	public Label NameLabel;
	public Label DescLabel;

	public Dice(int _min, int _max)
	{
		this.Min = _min;
		this.Max = _max;
        Path = "res://Scenes/Items/Dice.tscn";
		Desc = $"A Dice with a min of {this.Min} and a max of {this.Max}";

		switch (this.Max)
		{
			case 4:
				Name = "Normal Dice";
				break;
			case 7:
				Name = "Better Dice";
				break;
			case 8:
				Name = "Super Dice";
				break;
			case 12:
				Name = "Peaky Dice";
				break;
			default:
				Name = "Dice";
				break;
		}
	}

    public Dice()
	{
	}


    public int Roll()
	{
		Random rand = new Random();
		return rand.Next(Min, Max);
	}
}
