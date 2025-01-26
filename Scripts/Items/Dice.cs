using Godot;
using System;

public partial class Dice : ActiveItem
{
	public int Min { get; private set; }
	public int Max { get; private set; }

	Label NameLabel;
	Label DescLabel;
	public Dice(int _min, int _max)
	{
		this.Min = _min;
		this.Max = _max;
		itemName = "Ball and Chain";
        Desc = $"A Dice with a min of {Min} and a max of {Min}";
        Path = "res://Scenes/Items/ball_and_chain.tscn";
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
