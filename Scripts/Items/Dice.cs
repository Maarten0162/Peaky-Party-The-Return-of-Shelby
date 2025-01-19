using Godot;
using System;

public partial class Dice : Node
{
	int Min;
	int Max;
	public Dice(int min, int max)
	{
		Min = min;
		Max = max;
	}
	public int Roll()
	{
		Random rand = new Random();
		return rand.Next(Min, Max);
	}
}
