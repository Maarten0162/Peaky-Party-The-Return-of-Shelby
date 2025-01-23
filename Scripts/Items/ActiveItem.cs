using Godot;
using System;
using System.Net.Http.Headers;

public partial class ActiveItem : Control
{

    protected string itemName;
    protected string Desc;
    protected string Path;
    [Export]
    protected int Price;
    public virtual void Use(Player player)
    {

    }
    public string GetScenePath()
    {
        return Path;
    }

}
