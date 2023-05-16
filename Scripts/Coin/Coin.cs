using Godot;
using System;

public partial class Coin : Area2D, ICoin
{
    [Export]
    public int Value { get; set; }
    [Export]
    private AudioStreamPlayer2D _audioStreamPlayer;

    public override void _Ready()
    {
        base._Ready();
        BodyEntered += Collected;
    }

    public void Collected(Node2D node)
    {
        if (!node.Name.Equals("Player")) return;

        _audioStreamPlayer.Play();
        GameManager.Instance.GameScore.AddPoints(Value);
        QueueFree();
    }

}
