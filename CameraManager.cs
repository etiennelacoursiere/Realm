using Godot;
using System;

public partial class CameraManager : Camera2D
{
    [Export]
    public float ZoomFactor { get; private set; } = 1.0f;

    [Export]
    public float MinZoom { get; private set; } = 0.3f;

    [Export]
    public float MaxZoom { get; private set; } = 1.5f;

    [Export]
    public float ZoomSpeed { get; private set; } = 0.01f;

    [Export]
    public float PanSpeed { get; private set; } = 5.0f;

    public override void _Ready()
    {
        Zoom = new Vector2(ZoomFactor, ZoomFactor);
        Position = new Vector2(0, 0);
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionPressed("zoom_out"))
        {
            ZoomFactor = Mathf.Clamp(ZoomFactor - ZoomSpeed, MinZoom, MaxZoom);
            Zoom = new Vector2(ZoomFactor, ZoomFactor);
        }

        if (Input.IsActionPressed("zoom_in"))
        {
            ZoomFactor = Mathf.Clamp(ZoomFactor + ZoomSpeed, MinZoom, MaxZoom);
            Zoom = new Vector2(ZoomFactor, ZoomFactor);
        }

        if (Input.IsActionPressed("pan_up"))
        {
            Position += new Vector2(0, -PanSpeed);
        }

        if (Input.IsActionPressed("pan_down"))
        {
            Position += new Vector2(0, PanSpeed);
        }

        if (Input.IsActionPressed("pan_left"))
        {
            Position += new Vector2(-PanSpeed, 0);
        }

        if (Input.IsActionPressed("pan_right"))
        {
            Position += new Vector2(PanSpeed, 0);
        }
    }

    public void ResetCamera()
    {
        Position = new Vector2(0, 0);
        ZoomFactor = 1.0f;
        Zoom = new Vector2(ZoomFactor, ZoomFactor);
    }


}
