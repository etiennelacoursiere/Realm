using Game.Tiles;
using Godot;

namespace Game;

public partial class CursorController : Node2D
{
    private TilesManager tilesManager;

    public override void _Ready()
    {
        tilesManager = GetNode<TilesManager>("%TilesManager");
    }

    public override void _Input(InputEvent evt)
    {
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("move_up"))
        {
            Position = new Vector2(Position.X, Position.Y - tilesManager.TileSize);
        }

        if (Input.IsActionJustPressed("move_down"))
        {
            Position = new Vector2(Position.X, Position.Y + tilesManager.TileSize);
        }

        if (Input.IsActionJustPressed("move_left"))
        {
            Position = new Vector2(Position.X - tilesManager.TileSize, Position.Y);
        }

        if (Input.IsActionJustPressed("move_right"))
        {
            Position = new Vector2(Position.X + tilesManager.TileSize, Position.Y);
        }
    }
}
