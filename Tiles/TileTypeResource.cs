using Godot;

[GlobalClass]
public partial class TileTypeResource : Resource
{
    [Export]
    public int Count { get; private set; } = 0;

    [Export]
    public PackedScene TileScene { get; private set; } = null!;
}
