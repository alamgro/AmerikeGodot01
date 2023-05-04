using Godot;
using System;

public partial class Score : Label, IScore
{
    public int CurrentScore { get; set; } = 0;

    public void AddPoints(int points)
    {
        CurrentScore += points;
        Text = $"Score: {CurrentScore}";
    }
}
