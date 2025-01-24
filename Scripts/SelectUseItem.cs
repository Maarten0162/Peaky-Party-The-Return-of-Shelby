using Godot;
using System;
using System.Collections.Generic;

public partial class SelectUseItem : Control
{
    [Signal]
    public delegate void customSignalEventHandler();
    

    private Control _itemContainer;
    private int _currentIndex = 0;
    private float _spacing = 200f; // Distance between items
    private Vector2 _centerPosition = Vector2.Zero; // Center of the screen
    Player currentPlayer;

    List<Control> ItemsInContainer = new();

    // Store initial positions of items
    private Vector2[] initialPositions;

    public override void _Ready()
    {
        _itemContainer = GetNode<Control>("ItemContainer");

        // Set the center position to the middle of the screen
        _centerPosition = GetViewport().GetVisibleRect().Size / 2;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustReleased("ui_right"))
        {
            _currentIndex++;
            if (_currentIndex >= _itemContainer.GetChildCount())
                _currentIndex = 0;
            UpdateCarousel();
        }
        else if (Input.IsActionJustReleased("ui_left"))
        {
            _currentIndex--;
            if (_currentIndex < 0)
            {
                _currentIndex = _itemContainer.GetChildCount() - 1;
            }
            UpdateCarousel();
        }
        else if (Input.IsActionJustReleased("select"))
        {
            GD.Print($"selected item {_currentIndex}");
            currentPlayer.itemList[_currentIndex].Use(currentPlayer);
            currentPlayer.itemList.RemoveAt(_currentIndex);
            foreach (Node child in _itemContainer.GetChildren())
            {
                child.QueueFree();
            }
            _itemContainer.QueueFree();
            EmitSignal(nameof(customSignal));

        }
    }

    public void Initialize(Player player)
    {
        currentPlayer = player;

        // Initialize initial positions
        initialPositions = new Vector2[player.itemList.Count];

        foreach (ActiveItem item in player.itemList)
        {
            PackedScene itemScene;
            Node itemInstance;
            switch (item)
            {
                case BallAndChain:
                    GD.Print("ball and chain item");
                    itemScene = (PackedScene)ResourceLoader.Load("res://Scenes/Items/ball_and_chain.tscn");
                    itemInstance = itemScene.Instantiate();
                    _itemContainer.AddChild(itemInstance);
                    break;
                default:
                    GD.Print("no item");
                    break;
            }

        }

        // Store the initial positions after all items are instantiated
        StoreInitialPositions();

        
        
        for (int i = 0; i < player.itemList.Count; i++)
        {
            ItemsInContainer.Add(_itemContainer.GetChild<Control>(i));
        }
        ItemsInContainer[0].Scale = new Vector2(1.5f, 1.5f);//waarom de fuckkkk doet dit NIKSSSS!!!!!!!!!!!!!
        ItemsInContainer[0].Modulate = Colors.White;
        int itemCount = _itemContainer.GetChildCount();
        for (int i = 1; i < itemCount; i++)//i = 1 zodat de geselected(item 0) niet veranderd
        {
            _itemContainer.GetChild<Control>(i).Scale = new Vector2(2.0f, 1.0f);
            GD.Print(_itemContainer.GetChild<Control>(i).Size);
            _itemContainer.GetChild<Control>(i).Modulate = new Color(0.2f, 0.2f, 0.2f, 1);
        }
    }

    // Store the initial positions of the items in the container
    private void StoreInitialPositions()
    {
        int itemCount = _itemContainer.GetChildCount();
        initialPositions = new Vector2[itemCount];

        for (int i = 0; i < itemCount; i++)
        {
            var item = _itemContainer.GetChild<Control>(i);
            initialPositions[i] = item.Position;
        }
    }

    private void UpdateCarousel()
    {
        int itemCount = _itemContainer.GetChildCount();

        // Iterate through all items in the container
        for (int i = 0; i < itemCount; i++)
        {
            var item = _itemContainer.GetChild<Control>(i);

            // Set the pivot offset to the center of the item so scaling happens from the center
            item.PivotOffset = item.Size / 2;

            if (i == _currentIndex)
            {
                item.Scale = new Vector2(1.5f, 1.5f);
                item.Modulate = Colors.White;
                item.Position = initialPositions[i]; // Keep the Y, center on X
            }
            else
            {
                item.Scale = new Vector2(1.0f, 1.0f);
                item.Modulate = new Color(0.2f, 0.2f, 0.2f, 1);

                // Calculate offset to move left or right
                float offset = (i - _currentIndex) * _spacing;
                item.Position = initialPositions[i] + new Vector2(offset, 0); // Maintain Y, update X with offset
            }
        }
    }
}
