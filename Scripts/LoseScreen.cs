using Godot;
using System;

public class LoseScreen : Control
{
    
    public override void _Ready()
    {
        Label scoreLabel = GetNode<Label>("VBoxContainer/ScoreLabel");
        scoreLabel.Text = "Score: " + LevelData.CurrentScore;

        Label bestLabel = GetNode<Label>("VBoxContainer/BestLabel");
        bestLabel.Text =  "Best: " + LevelData.BestScore;

        Label levelLabel = GetNode<Label>("VBoxContainer/LevelLabel");
        levelLabel.Text = "Level " + LevelData.Level;
    }

    private void OnPlayAgainButtonPressed()
    {
        GetTree().ChangeScene("res://Scenes/Main.tscn");
    }
}
