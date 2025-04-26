using Game.Tiles;
using Godot;

namespace Game;

public partial class CursorController : Node2D
{
    private TilesManager tilesManager;
    private Sprite2D cursorSprite;
    private Sprite2D cursorSelectSprite;

    public override void _Ready()
    {
        GameEvents.Instance.TilePlaced += OnTilePlaced;
        tilesManager = GetNode<TilesManager>("%TilesManager");
        cursorSprite = GetNode<Sprite2D>("Sprite2D");
        cursorSelectSprite = GetNode<Sprite2D>("CursorSelectSprite");

        cursorSprite.RegionRect = tilesManager.currentTile.sprite2D.RegionRect;
        UpdateCursorSelectValidState();
    }

    public override void _Process(double delta)
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.IsActionJustPressed("place"))
        {
            if (tilesManager.CanPlaceTile(Position, cursorSprite.Rotation))
            {
                tilesManager.PlaceTile(Position, cursorSprite.Rotation);
            }
        }

        if (Input.IsActionJustPressed("move_up"))
        {
            Vector2 newPosition = new Vector2(Position.X, Position.Y - tilesManager.TileSize);
            Position = newPosition;
            UpdateCursorSelectValidState();
        }

        if (Input.IsActionJustPressed("move_down"))
        {
            Vector2 newPosition = new Vector2(Position.X, Position.Y + tilesManager.TileSize);
            Position = newPosition;
            UpdateCursorSelectValidState();
        }

        if (Input.IsActionJustPressed("move_left"))
        {
            Vector2 newPosition = new Vector2(Position.X - tilesManager.TileSize, Position.Y);
            Position = newPosition;
            UpdateCursorSelectValidState();
        }

        if (Input.IsActionJustPressed("move_right"))
        {
            Vector2 newPosition = new Vector2(Position.X + tilesManager.TileSize, Position.Y);
            Position = newPosition;
            UpdateCursorSelectValidState();
        }

        if (Input.IsActionJustPressed("rotate_cw"))
        {
            cursorSprite.Rotation = (cursorSprite.Rotation + Mathf.Pi / 2) % Mathf.Tau;
            UpdateCursorSelectValidState();
        }

        if (Input.IsActionJustPressed("rotate_ccw"))
        {
            cursorSprite.Rotation = (cursorSprite.Rotation - Mathf.Pi / 2) % Mathf.Tau;
            UpdateCursorSelectValidState();
        }
    }

    private void UpdateCursorSelectValidState()
    {
        if (tilesManager.CanPlaceTile(Position, cursorSprite.Rotation))
        {
            cursorSelectSprite.Modulate = new Color(0, 1, 0, 1); // Green
        }
        else
        {
            cursorSelectSprite.Modulate = new Color(1, 0, 0, 1); // Red
        }
    }

    private void OnTilePlaced(Tile _tile)
    {
        cursorSprite.RegionRect = tilesManager.currentTile.sprite2D.RegionRect;
        cursorSprite.Rotation = 0;
        UpdateCursorSelectValidState();
    }
}
