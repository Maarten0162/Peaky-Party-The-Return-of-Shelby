using Godot;
using System;
using System.Net.Http.Headers;

public partial class ActiveItem : Control
{

    public string itemName {get; protected set;}
    public string Desc {get; protected set;}
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
