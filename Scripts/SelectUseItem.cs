using Godot;
using System;

public partial class SelectUseItem : Control
{
	[Signal]
	public delegate void customSignalEventHandler();

	private Control _itemContainer;
	private int _currentIndex = 0; // The index of the currently selected item
	private float _spacing = 200f; // Distance between items
	private Vector2 _centerPosition = Vector2.Zero; // Center of the screen
	Player currentPlayer;



	public override void _Ready()
	{
		
		_itemContainer = GetNode<Control>("ItemContainer");

		// Set the center position to the middle of the screen
		_centerPosition = GetViewport().GetVisibleRect().Size / 2;
		
		UpdateCarousel();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustReleased("ui_right"))
		{
			_currentIndex++;
			if (_currentIndex >= _itemContainer.GetChildCount())
				_currentIndex = 0;
			UpdateCarousel();
			
		}else if (Input.IsActionJustReleased("ui_left"))
		{
			_currentIndex--;
			if (_currentIndex < 0)
				_currentIndex = _itemContainer.GetChildCount() - 1;
			UpdateCarousel();
			
		}else if (Input.IsActionJustReleased("select")) 
		{
			GD.Print($"selected item {_currentIndex}");
			currentPlayer.itemList[_currentIndex].Use(currentPlayer);
			currentPlayer.itemList.RemoveAt(_currentIndex);
			
			EmitSignal(nameof(customSignal));
		}
	}

	public void Initialize(Player player)
	{
		currentPlayer = player;
		foreach (ActiveItem item in player.itemList) 
		{
			PackedScene itemScene;
			Node itemInstance;
			switch (item) 
			{
				case BallAndChain:
					GD.Print("ball and chain item");
					
					itemScene = (PackedScene)ResourceLoader.Load(item.GetScenePath());
					itemInstance = itemScene.Instantiate();
					_itemContainer.AddChild(itemInstance);
					break;
				default:
					GD.Print("no item");
					break;
			}
					
		}
	}

	// public void getInput(InputEvent @event)
	// {
	// 	if (@event.IsActionPressed("ui_right"))
	// 	{
	// 		_currentIndex++;
	// 		if (_currentIndex >= _itemContainer.GetChildCount())
	// 			_currentIndex = 0;
	// 		UpdateCarousel();
			
	// 	}else if (@event.IsActionPressed("ui_left"))
	// 	{
	// 		_currentIndex--;
	// 		if (_currentIndex < 0)
	// 			_currentIndex = _itemContainer.GetChildCount() - 1;
	// 		UpdateCarousel();
			
	// 	}else if (@event.IsAction("select")) 
	// 	{
			
	// 	}
		
	// }

	private void UpdateCarousel()
	{
		for (int i = 0; i < _itemContainer.GetChildCount(); i++)
		{
			var item = _itemContainer.GetChild<Control>(i);

			// Set the pivot offset to the center of the item so scaling happens from the center
			item.PivotOffset = item.Size / 2;

			// Calculate offset from the center based on index difference
			float offset = (i - _currentIndex) * _spacing;

			// Position item relative to the center
			item.Position = _centerPosition + new Vector2(offset, 0);

			
			if (i == _currentIndex)
			{
				item.Scale = new Vector2(1.5f, 1.5f);
				item.Modulate = Colors.White;
			}
			else
			{
				item.Scale = new Vector2(1.0f, 1.0f);
				item.Modulate = new Color(0.2f, 0.2f, 0.2f, 1);
			}
		}
	}

	private void hasSelectedItem()
	{
		
	}
}
