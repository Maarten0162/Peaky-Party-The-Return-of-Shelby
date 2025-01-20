using Godot;
using System;

public partial class Dice : Node
{
	private int Min;
	private int Max;
	public Dice(int _min, int _max)
	{
		this.Min = _min;
		this.Max = _max;
	}
	public int Roll()
	{
		Random rand = new Random();
		return rand.Next(Min, Max);
	}
}
