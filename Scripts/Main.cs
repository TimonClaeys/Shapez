using Godot;
using System;

public class Main : Node2D
{
    Control _UI;

    Node _coinSpawner;

    private bool _firstTime = true;

    private int _score;

    private Control _scene;
    public override void _Ready()
    {
        _UI = GetNode<Control>("UI");
        _coinSpawner = GetNode<Node>("CoinSpawner");

        LevelData.Load();
        ShowLevelScreen();
    }

    public void OnCoinCollected()
    {
        _score++;
        UpdateScore();
        _coinSpawner.Call("SpawnCoin");

    }

    private void UpdateScore()
    {
        _UI.CallDeferred("UpdateScore", _score);
        if (_score > LevelData.BestScore)
        {
            LevelData.BestScore = _score;
        }
        LevelData.CurrentScore = _score;
    }

    private void OnGameLost()
    {
        GetTree().ChangeScene("res://Scenes/UI/LoseScreen.tscn");
        LevelData.Save();
        _score = 0 ;
    }

    private void OnLevelComplete()
    {


        Node2D lockNode = GetNode<Node2D>("Lock");

        GetTree().CallGroup("Pointer", "ToggleMovement");


        if (LevelData.Level > 0)
        {
            ShowLevelScreen();            
        }
        else
        {
            LevelData.Level++;
            _coinSpawner.Call("SpawnCoin");
            GetTree().CallGroup("Pointer", "ToggleMovement");
        }

        LevelData.Save();
    }

    private void ShowLevelScreen()
    {
        PackedScene levelScene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/LevelScreen.tscn");

        Control scene = levelScene.Instance<Control>();
        scene.RectGlobalPosition = new Vector2(0, 0);
        scene.RectSize = new Vector2(720, 180);

        _scene = scene;
        AddChild(scene);

        AddSignal();
    }

    private void AddSignal()
    {
        Button button = (Button)_scene.GetChild(0).GetChild(0);

        button.Connect("pressed", this, "OnNextLevelButtonPressed");
    }

    private void OnNextLevelButtonPressed()
    {
        GetTree().CallGroup("Pointer", "ToggleMovement");
        _scene.QueueFree();

        if (!_firstTime)
        {
            LevelData.Level++;
        }
        else
        {
            _firstTime = false;
        }
        
        _score = 0;
        UpdateScore();

        _coinSpawner.Call("SpawnCoin");
    }
}
