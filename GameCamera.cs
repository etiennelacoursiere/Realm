using Godot;

public partial class GameCamera : Camera2D
{
    private readonly StringName ACTION_PAN_LEFT = "pan_left";
    private readonly StringName ACTION_PAN_RIGHT = "pan_right";
    private readonly StringName ACTION_PAN_UP = "pan_up";
    private readonly StringName ACTION_PAN_DOWN = "pan_down";
    private readonly StringName ACTION_ZOOM_IN = "zoom_in";
    private readonly StringName ACTION_ZOOM_OUT = "zoom_out";
    private readonly StringName ACTION_RESET_CAMERA = "reset_camera";

    [Export]
    public float ZoomFactor { get; private set; } = 1.0f;

    [Export]
    public float MinZoom { get; private set; } = 0.3f;

    [Export]
    public float MaxZoom { get; private set; } = 1.5f;

    [Export]
    public float ZoomSpeed { get; private set; } = 0.1f;

    [Export]
    public float PanSpeed { get; private set; } = 500f;

    public override void _Ready()
    {
        Zoom = new Vector2(ZoomFactor, ZoomFactor);
        Position = new Vector2(0, 0);
    }

    public override void _Process(double delta)
    {
        ProcessZoom();
        ProcessPan(delta);
        ResetCamera();
    }

    private void ProcessZoom()
    {
        if (Input.IsActionJustReleased(ACTION_ZOOM_IN))
        {
            ZoomFactor = Mathf.Clamp(ZoomFactor + ZoomSpeed, MinZoom, MaxZoom);
            Zoom = new Vector2(ZoomFactor, ZoomFactor);
        }
        if (Input.IsActionJustReleased(ACTION_ZOOM_OUT))
        {
            ZoomFactor = Mathf.Clamp(ZoomFactor - ZoomSpeed, MinZoom, MaxZoom);
            Zoom = new Vector2(ZoomFactor, ZoomFactor);
        }
    }

    private void ProcessPan(double delta)
    {
        var panVector = Input.GetVector(ACTION_PAN_LEFT, ACTION_PAN_RIGHT, ACTION_PAN_UP, ACTION_PAN_DOWN);
        GlobalPosition += panVector * PanSpeed * (float)delta;
    }

    private void ResetCamera()
    {
        if (Input.IsActionJustPressed(ACTION_RESET_CAMERA))
        {
            ZoomFactor = 1.0f;
            Zoom = new Vector2(ZoomFactor, ZoomFactor);
            Position = new Vector2(0, 0);
        }
    }
}
