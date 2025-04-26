using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Tiles;


public partial class TilesManager : Node
{
    private static Random rng = new();

    [Export]
    public int TileSize { get; private set; } = 128;

    [Export]
    public Tile StartingTile { get; private set; }

    [Export]
    public TileTypeResource[] TileTypes { get; private set; }

    private Stack<PackedScene> drawPile = new();

    private Dictionary<Vector2, Tile> placedTiles = new();

    public Tile currentTile { get; private set; }

    public override void _Ready()
    {
        placedTiles.Add(StartingTile.Position, StartingTile);
        InitDrawPile();
        DrawTile();
    }

    public void PlaceTile(Vector2 position, float rotation)
    {
        if (CanPlaceTile(position, rotation))
        {
            var newTile = currentTile.Duplicate() as Tile;
            newTile.Position = position;
            newTile.Rotation = rotation;
            placedTiles.Add(position, newTile);
            AddChild(newTile);
            DrawTile();

            // CallDeffered will call our Callable after all other godot process are done. 
            // This will then ben called after the globale position of our tile has been set.
            Callable.From(() => GameEvents.EmitTilePlaced(newTile)).CallDeferred();
        }
    }

    public bool IsCellOccupied(Vector2 position)
    {
        return placedTiles.ContainsKey(position);
    }

    public bool CanPlaceTile(Vector2 position, float rotation)
    {
        if (IsCellOccupied(position)) return false;

        Vector2 abovePosition = position + new Vector2(0, -TileSize);
        if (placedTiles.ContainsKey(abovePosition))
        {
            Tile aboveTile = placedTiles[abovePosition];
            GD.Print("Above tile Bottom Connector: " + aboveTile.BottomConnector);
            GD.Print("Above tile Rotation: " + aboveTile.Rotation);
            GD.Print("Current tile Top Connector: " + currentTile.GetRotatedTopConnector(rotation));
            if (aboveTile.GetRotatedBottomConnector(aboveTile.Rotation) != currentTile.GetRotatedTopConnector(rotation)) return false;
        }

        Vector2 rightPosition = position + new Vector2(TileSize, 0);
        if (placedTiles.ContainsKey(rightPosition))
        {
            Tile rightTile = placedTiles[rightPosition];
            GD.Print("Right tile Left Connector: " + rightTile.LeftConnector);
            GD.Print("Right tile Rotation: " + rightTile.Rotation);
            GD.Print("Current tile Right Connector: " + currentTile.GetRotatedRightConnector(rotation));
            if (rightTile.GetRotatedLeftConnector(rightTile.Rotation) != currentTile.GetRotatedRightConnector(rotation)) return false;
        }

        Vector2 belowPosition = position + new Vector2(0, TileSize);
        if (placedTiles.ContainsKey(belowPosition))
        {
            Tile belowTile = placedTiles[belowPosition];
            GD.Print("Below tile Top Connector: " + belowTile.TopConnector);
            GD.Print("Below tile Rotation: " + belowTile.Rotation);
            GD.Print("Current tile Bottom Connector: " + currentTile.GetRotatedBottomConnector(rotation));
            if (belowTile.GetRotatedTopConnector(belowTile.Rotation) != currentTile.GetRotatedBottomConnector(rotation)) return false;
        }

        Vector2 leftPosition = position + new Vector2(-TileSize, 0);
        if (placedTiles.ContainsKey(leftPosition))
        {
            Tile leftTile = placedTiles[leftPosition];
            GD.Print("Left tile Right Connector: " + leftTile.RightConnector);
            GD.Print("Left tile Rotation: " + leftTile.Rotation);
            GD.Print("Current tile Left Connector: " + currentTile.GetRotatedLeftConnector(rotation));
            if (leftTile.GetRotatedRightConnector(leftTile.Rotation) != currentTile.GetRotatedLeftConnector(rotation)) return false;
        }

        if (!IsCellOccupied(abovePosition) && !IsCellOccupied(rightPosition) && !IsCellOccupied(belowPosition) && !IsCellOccupied(leftPosition))
        {
            return false;
        }

        return true;
    }

    private void InitDrawPile()
    {
        foreach (var tileType in TileTypes)
        {
            for (int x = 0; x < tileType.Count; x++)
            {
                drawPile.Push(tileType.TileScene);
            }
        }

        ShuffleDrawPile();
    }

    private void DrawTile()
    {
        if (drawPile.Count == 0)
        {
            GD.Print("No more tiles to draw!");
            return;
        }

        currentTile = drawPile.Pop().Instantiate<Tile>();
    }

    private void ShuffleDrawPile()
    {
        // Fisher-Yates shuffle algorithm

        var list = drawPile.ToList();

        int n = drawPile.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            PackedScene value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        drawPile.Clear();

        foreach (var item in list)
        {
            drawPile.Push(item);
        }
    }
}
