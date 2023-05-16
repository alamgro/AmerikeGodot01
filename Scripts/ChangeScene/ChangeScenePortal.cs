using Godot;
using System;

public partial class ChangeScenePortal : Area2D
{
    [Export]
    private string _scenePath;

    public override void _Ready()
    {
        base._Ready();
        BodyEntered += ChangeScene;
    }
    public void ChangeScene(Node2D node)
    {
        if (!node.Name.Equals("Player")) return;

        PackedScene newScene = (PackedScene)GD.Load(_scenePath);
        Node newSceneInstance = newScene.Instantiate();
        Node currentScene = GetTree().CurrentScene;

        currentScene.GetParent().AddChild(newSceneInstance);
        currentScene.QueueFree();
    }
}
