using Game.Tiles;
using Godot;
using System;

namespace Game;

public partial class Main : Node
{
    private TilesManager tilesManager;
    private Sprite2D cursor;

    public override void _Ready()
    {
        tilesManager = GetNode<TilesManager>("%TilesManager");
    }
}
