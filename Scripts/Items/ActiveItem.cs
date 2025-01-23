using Godot;
using System;

public partial class ActiveItem : Control
{

    protected string itemName;
    protected string Desc;
    [Export]
    protected int Price;
    public virtual void Use(Player player)
    {

    }

}
