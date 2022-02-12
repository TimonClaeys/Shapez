using Godot;
using System;

public class CoinSpawner : Node
{
    private int _level;
    private int _currentCoin;


    [Signal]
    public delegate void LevelComplete();

    private PackedScene _coin;

    public override void _Ready()
    {

        _coin = ResourceLoader.Load<PackedScene>("res://Scenes/Coin.tscn");
    }

    private void SpawnCoin(){
        GD.Print("Level " + LevelData.Level);
        if (_currentCoin < LevelData.Level)
        {
            Random random =  new Random();
            Node2D coin = (Node2D)_coin.Instance();
            coin.RotationDegrees = random.Next(360);
            GetParent().GetChild(0).AddChild(coin);
            _currentCoin++;
        }
        else
        {
            EmitSignal(nameof(LevelComplete));
            _currentCoin = 0;
        }
    }
}
