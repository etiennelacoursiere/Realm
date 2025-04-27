using Game.Tiles;
using Godot;

namespace Game;

public partial class CursorController : Node2D
{
    private readonly StringName ACTION_ROTATE_CW = "rotate_cw";
    private readonly StringName ACTION_ROTATE_CCW = "rotate_ccw";
    private readonly StringName ACTION_PLACE = "place";

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
        UpdateCursorPositionWithMouse();
    }

    private void UpdateCursorPositionWithMouse()
    {
        Vector2 mousePosition = GetGlobalMousePosition();

        float gridSize = tilesManager.TileSize;
        Vector2 snappedPosition = new Vector2(
            Mathf.Round(mousePosition.X / gridSize) * gridSize,
            Mathf.Round(mousePosition.Y / gridSize) * gridSize
        );

        Position = snappedPosition;

        UpdateCursorSelectValidState();
    }

    private void ProcessInput()
    {
        if (Input.IsActionJustPressed(ACTION_PLACE))
        {
            if (tilesManager.CanPlaceTile(Position, cursorSprite.Rotation))
            {
                tilesManager.PlaceTile(Position, cursorSprite.Rotation);
            }
        }

        if (Input.IsActionJustPressed(ACTION_ROTATE_CW))
        {
            var rotation = (Mathf.RadToDeg(cursorSprite.Rotation) + 90) % 360;
            cursorSprite.Rotation = Mathf.DegToRad(rotation);
            UpdateCursorSelectValidState();
        }

        if (Input.IsActionJustPressed(ACTION_ROTATE_CCW))
        {
            var rotation = (Mathf.RadToDeg(cursorSprite.Rotation) - 90) % 360;
            cursorSprite.Rotation = Mathf.DegToRad(rotation);
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
