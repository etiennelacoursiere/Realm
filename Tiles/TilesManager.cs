using Godot;
using System.Collections.Generic;

namespace Game.Tiles;

public partial class TilesManager : Node
{
    [Export]
    public int TileSize { get; private set; } = 128;

    [Export]
    public TileTypeResource[] TileTypes { get; private set; }

    private List<Vector2I> drawPile = new();

    public override void _Ready()
    {
    }
}
