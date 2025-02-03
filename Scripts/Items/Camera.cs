using Godot;
using System;

public partial class Camera : Camera2D
{
    Vector2 viewportSize;

    public override void _Ready()
    {
        Board board = GetNode<Board>("../../Board");
        TextureRect Background = board.GetNode<TextureRect>("TextureRect");
        Vector2 rectPosition = Background.GlobalPosition;
        Vector2 rectSize = Background.Size * board.Scale;
        // Set the background boundaries
        LimitLeft = (int)rectPosition.X;
        LimitRight = (int)(rectPosition.X + rectSize.X);
        LimitTop = (int)rectPosition.Y;
        LimitBottom = (int)(rectPosition.Y + rectSize.Y);
        viewportSize = GetViewportRect().Size;

    }
    private Vector2 dragStart;

    // Check when the key is pressed or released
    private Vector2 cameraMovement = Vector2.Zero;
    private Vector2 velocity = Vector2.Zero;  // For smooth deceleration

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent && keyEvent.Pressed)
        {
            if (keyEvent.Keycode == Key.W || keyEvent.Keycode == Key.Up)
            {
                velocity.Y = -1; // Move up
            }
            if (keyEvent.Keycode == Key.S || keyEvent.Keycode == Key.Down)
            {
                velocity.Y = 1; // Move down
            }
            if (keyEvent.Keycode == Key.A || keyEvent.Keycode == Key.Left)
            {
                velocity.X = -1; // Move left
            }
            if (keyEvent.Keycode == Key.D || keyEvent.Keycode == Key.Right)
            {
                velocity.X = 1; // Move right
            }
        }
        else if (@event is InputEventKey keyReleaseEvent && !keyReleaseEvent.Pressed)
        {
            if (keyReleaseEvent.Keycode == Key.W || keyReleaseEvent.Keycode == Key.Up ||
                keyReleaseEvent.Keycode == Key.S || keyReleaseEvent.Keycode == Key.Down)
            {
                velocity.Y = 0; // Stop vertical movement
            }

            if (keyReleaseEvent.Keycode == Key.A || keyReleaseEvent.Keycode == Key.Left ||
                keyReleaseEvent.Keycode == Key.D || keyReleaseEvent.Keycode == Key.Right)
            {
                velocity.X = 0; // Stop horizontal movement
            }
        }
    }

    public override void _Process(double delta)
    {
        // Normalize the velocity to avoid faster movement on diagonals
        Vector2 movementDirection = velocity.Normalized();

        // Smooth deceleration (velocity gradually decreases to zero)
        cameraMovement = movementDirection.Lerp(Vector2.Zero, 0.1f); // Adjust 0.1f to control deceleration speed

        // Apply movement
        Vector2 newPosition = Position + cameraMovement * 300 * (float)delta;

        // Clamp position to stay within the board's boundaries
        newPosition.X = Mathf.Clamp(newPosition.X, LimitLeft, LimitRight - (int)viewportSize.X);
        newPosition.Y = Mathf.Clamp(newPosition.Y, LimitTop, LimitBottom - (int)viewportSize.Y);

        Position = newPosition; // Apply the clamped position
        GD.Print(Position);
    }



}
