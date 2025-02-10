using Godot;
using System;
using System.Collections.Generic;

public static class SaveManager
{
	private static List<Player> Playerlist;
	private static Board Board;


	public static void Save(List<Player> _plist, Board _board)
	{
		Playerlist = _plist;
		Board = _board;
	}
	public static List<Player> LoadPlayers()
{
    List<Player> newPlayers = new List<Player>();

    PackedScene playerScene = GD.Load<PackedScene>("res://Scenes/Game Objects/Player.tscn");
    foreach (Player savedPlayer in Playerlist)
    {
        // Instance a new Player from Player.tscn
        Player newPlayer = (Player)playerScene.Instantiate();

        // Copy necessary attributes
        newPlayer.PlayerName = savedPlayer.PlayerName; // Copy player name (if needed)
        newPlayer.currSpace = savedPlayer.currSpace;
        newPlayer.Currency = savedPlayer.Currency;
        newPlayer.Health = savedPlayer.Health;
        newPlayer.Income = savedPlayer.Income;
        newPlayer.RollAdjust = savedPlayer.RollAdjust;
        newPlayer.isAlive = savedPlayer.isAlive;
        newPlayer.SkipTurn = savedPlayer.SkipTurn;

        // Copy passive and active items
        newPlayer.itemList = new List<ActiveItem>(savedPlayer.itemList);
        newPlayer.AllPassiveItems = new List<PassiveItem>(savedPlayer.AllPassiveItems);
        newPlayer.StartPassiveItems = new List<PassiveItem>(savedPlayer.StartPassiveItems);
        newPlayer.MovingPassiveItems = new List<PassiveItem>(savedPlayer.MovingPassiveItems);
        newPlayer.EndTurnPassiveItems = new List<PassiveItem>(savedPlayer.EndTurnPassiveItems);
        newPlayer.PassingPassiveItems = new List<PassiveItem>(savedPlayer.PassingPassiveItems);
        newPlayer.TakeDamagePassiveItems = new List<PassiveItem>(savedPlayer.TakeDamagePassiveItems);
        newPlayer.HealPassiveItems = new List<PassiveItem>(savedPlayer.HealPassiveItems);
        newPlayer.ObtainCurrencyPassiveItem = new List<PassiveItem>(savedPlayer.ObtainCurrencyPassiveItem);
        newPlayer.LoseCurrencyPassiveItem = new List<PassiveItem>(savedPlayer.LoseCurrencyPassiveItem);
        newPlayer.RollAdjustChangePassiveItem = new List<PassiveItem>(savedPlayer.RollAdjustChangePassiveItem);
        newPlayer.IncomeChangePassiveItem = new List<PassiveItem>(savedPlayer.IncomeChangePassiveItem);

      
        // Add to the new list
        newPlayers.Add(newPlayer);
    }

    return newPlayers;
}

	public static Board LoadBoard()
	{
		return Board;
	}
}
	