using Godot;
using System;

public partial class Player : Node2D
{
    private int health;
    private int currency;
    private int rollAdj;
    private int spacesWalked;
    private int minigamesWon;
    private int gainedCurr;
    private int lostCurr;
    private Vector2 currPos;
    public bool hasCap;
    public bool hasKnuck;
    public bool hasGoldKnuck;
    public Sprite2D skin;
    public Player(Sprite2D _skin)
    {
        this.skin = _skin;
        health = 100;
        currency = 10;
    }
    public int Movement()
    {

        return 1;
    }
    public void Attack()
    {

    }
    public void ReceiveAttack(Vector2 attackerpos) //animation based on from where you get attacked    
    {

    }

}
