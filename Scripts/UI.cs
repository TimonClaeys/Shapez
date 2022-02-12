using Godot;
using System;

public class UI : Control
{

    public override void _Ready()
    {
        UpdateScore(0);
    }
    public void UpdateScore(int score)
    {
        Label scoreLabel = GetNode<Label>("CenterContainer/Label");
        scoreLabel.Text = Convert.ToString(score);

        Label levelLabel = GetNode<Label>("VBoxContainer/Label");
        levelLabel.Text =  "Level " + LevelData.Level;
    }
}
