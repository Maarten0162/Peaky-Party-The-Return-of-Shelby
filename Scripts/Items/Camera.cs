using Godot;
using System;
using System.Threading.Tasks;

public partial class Camera : Camera2D
{
    Vector2 viewportSize;
    bool freemove = false;
    GameLogic Session;
    Player activePlayer;
    Board Board;
    Vector2 Startzoom;
    [Export]
    float ZoomOut = 0.8f;
    public override void _Ready()
    {
        Startzoom = Zoom;
        Session = GetNode<GameLogic>("/root/Node");
        Board board = GetNode<Board>("../../Board");
        TextureRect Background = board.GetNode<TextureRect>("TextureRect");
        Vector2 rectPosition = Background.GlobalPosition;
        Vector2 rectSize = Background.Size * board.Scale;
        // Set the background boundaries
        LimitLeft = (int)rectPosition.X;
        LimitRight = (int)(rectPosition.X + rectSize.X);
        LimitTop = (int)rectPosition.Y;
        LimitBottom = (int)(rectPosition.Y + rectSize.Y);
        LimitBottom *= (int)Startzoom.Y;
        GD.Print(LimitBottom + "is limitbottom");
        viewportSize = GetViewportRect().Size;

        Vector2 boardCenter = rectPosition + (rectSize / 2);
        GlobalPosition = boardCenter;

    }
    private Vector2 dragStart;

    // Check when the key is pressed or released
    private Vector2 cameraMovement = Vector2.Zero;
    private Vector2 velocity = Vector2.Zero;  // For smooth deceleration

    public override void _Input(InputEvent @event)
    {
        if (freemove)
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
            if (Input.IsActionJustPressed("Return"))
            {

                freemove = false;
                Session.openTurnHudMenu();
                ChangeZoom(Startzoom);
            }
        }
    }

    public override void _Process(double delta)
    {
        if (freemove)
        {
            // Normalize the velocity to avoid faster movement on diagonals
            Vector2 movementDirection = velocity.Normalized();

            // Smooth deceleration (velocity gradually decreases to zero)
            cameraMovement = movementDirection.Lerp(Vector2.Zero, 0.01f); // Adjust 0.1f to control deceleration speed

            // Apply movement
            Vector2 newPosition = Position + cameraMovement * 500 * (float)delta;

          

            // Clamp position to stay within the board's boundaries
            newPosition.X = Mathf.Clamp(newPosition.X, LimitLeft, LimitRight - (viewportSize.X / ZoomOut));
            newPosition.Y = Mathf.Clamp(newPosition.Y, LimitTop, LimitBottom - (viewportSize.Y * 0.5f * ZoomOut));

            GD.Print($"Viewport Size: {viewportSize}");
GD.Print($"ZoomOut: {ZoomOut}");
GD.Print($"LimitTop: {LimitTop}, LimitBottom: {LimitBottom}");
GD.Print($"Computed Bottom Clamp: {LimitBottom - (viewportSize.Y / ZoomOut)}");

            Position = newPosition; // Apply the clamped position
            GD.Print(Position);
        }
        else
        {
            // Calculate the desired camera position (center the camera on the player)
            Vector2 desiredPosition = activePlayer.Position;

            GlobalPosition = GlobalPosition.MoveToward(desiredPosition, 500 * (float)delta);


        }

    }
    public void Freecam()
    {
        freemove = true;
        ChangeZoom(new(ZoomOut, ZoomOut));
    }
    public void FollowPlayer(Player player)
    {

        activePlayer = player;

    }
    public void SetBoard(Board board)
    {
        this.Board = board;
    }
    private async void ChangeZoom(Vector2 target)
    {

        float zoomSpeed = 0.1f; // Adjust speed as needed

        while (Zoom.DistanceTo(target) > 0.01f) // Stop when close to target
        {
            Zoom = Zoom.Lerp(target, zoomSpeed);
            await Task.Delay(16); // Wait ~1 frame (16ms for ~60 FPS)
        }

        Zoom = target; // Ensure exact final value
    }

}
