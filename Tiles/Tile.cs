using Godot;
using System;
using System.Collections.Generic;

public partial class Tile : Node2D
{
    public enum Connector { Road, Field, City }

    [Export]
    public Connector TopConnector { get; private set; }

    [Export]
    public Connector RightConnector { get; private set; }

    [Export]
    public Connector BottomConnector { get; private set; }

    [Export]
    public Connector LeftConnector { get; private set; }

    [Export]
    public bool IsMonastery { get; private set; }

    [Export]
    public bool HasCoatOfArms { get; private set; }

    [Export]
    public int TotalCount { get; private set; }

    [Export]
    public Node2D[] MeeplesPositions { get; private set; }
}
