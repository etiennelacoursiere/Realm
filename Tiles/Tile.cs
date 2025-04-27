using Godot;
using System;
using System.Collections.Generic;

public partial class Tile : Node2D
{
    public enum Connector { Road, Field, City }
    public enum Feature { Road, Field, City, Monastery }

    [Export]
    public Connector TopConnector { get; private set; }

    [Export]
    public Connector RightConnector { get; private set; }

    [Export]
    public Connector BottomConnector { get; private set; }

    [Export]
    public Connector LeftConnector { get; private set; }

    [Export]
    public bool HasCoatOfArms { get; private set; }

    [Export]
    public Node2D[] MeeplesPositions { get; private set; }

    [Export]
    public Sprite2D sprite2D { get; private set; }

    public Connector GetRotatedTopConnector(float rotation)
    {
        if (rotation == 0) return TopConnector;
        if (rotation == Mathf.DegToRad(90) || rotation == Mathf.DegToRad(-270)) return LeftConnector;
        if (rotation == Mathf.DegToRad(270) || rotation == Mathf.DegToRad(-90)) return RightConnector;
        if (Mathf.Abs(rotation) == Mathf.DegToRad(180)) return BottomConnector;
        throw new ArgumentOutOfRangeException(nameof(rotation), "Rotation must be a multiple of 90. Got: " + Mathf.RadToDeg(rotation));
    }

    public Connector GetRotatedRightConnector(float rotation)
    {
        if (rotation == 0) return RightConnector;
        if (rotation == Mathf.DegToRad(90) || rotation == Mathf.DegToRad(-270)) return TopConnector;
        if (rotation == Mathf.DegToRad(270) || rotation == Mathf.DegToRad(-90)) return BottomConnector;
        if (Mathf.Abs(rotation) == Mathf.DegToRad(180)) return LeftConnector;
        throw new ArgumentOutOfRangeException(nameof(rotation), "Rotation must be a multiple of 90. Got: " + Mathf.RadToDeg(rotation));
    }

    public Connector GetRotatedBottomConnector(float rotation)
    {
        if (rotation == 0) return BottomConnector;
        if (rotation == Mathf.DegToRad(90) || rotation == Mathf.DegToRad(-270)) return RightConnector;
        if (rotation == Mathf.DegToRad(270) || rotation == Mathf.DegToRad(-90)) return LeftConnector;
        if (Mathf.Abs(rotation) == Mathf.DegToRad(180)) return TopConnector;
        throw new ArgumentOutOfRangeException(nameof(rotation), "Rotation must be a multiple of 90. Got: " + Mathf.RadToDeg(rotation));
    }

    public Connector GetRotatedLeftConnector(float rotation)
    {
        if (rotation == 0) return LeftConnector;
        if (rotation == Mathf.DegToRad(90) || rotation == Mathf.DegToRad(-270)) return BottomConnector;
        if (rotation == Mathf.DegToRad(270) || rotation == Mathf.DegToRad(-90)) return TopConnector;
        if (Mathf.Abs(rotation) == Mathf.DegToRad(180)) return RightConnector;
        throw new ArgumentOutOfRangeException(nameof(rotation), "Rotation must be a multiple of 90. Got: " + Mathf.RadToDeg(rotation));
    }
}
