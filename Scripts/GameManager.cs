using Godot;
using System;

public partial class GameManager : Node
{
    public static GameManager Instance;

    [Export]
    public Score GameScore { get; set; }
    public override void _EnterTree()
    {
        base._EnterTree();
        // Singleton pattern
        if(Instance != null)
        {
            QueueFree();
        }
        else
        {
            Instance = this;
        }
    }
}
