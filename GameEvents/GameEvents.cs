using Godot;

namespace Game;

public partial class GameEvents : Node
{
    public static GameEvents Instance { get; private set; }

    [Signal] public delegate void TilePlacedEventHandler(Tile tile);

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            Instance = this;
        }
    }

    public static void EmitTilePlaced(Tile tile)
    {
        Instance.EmitSignal(SignalName.TilePlaced, tile);
    }
}
