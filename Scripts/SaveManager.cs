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
	public static List<Player> LoadPlayers(){
		return Playerlist;
	}
	public static Board LoadBoard(){
		return Board;
	}
}
