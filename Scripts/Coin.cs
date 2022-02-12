using Godot;
using System;

public class Coin : Node2D
{
    AnimationPlayer _animationPlayer;
    Area2D _area2D; 

    public override void _Ready()
    {
        _animationPlayer =  GetNode<AnimationPlayer>("Sprite/AnimationPlayer");
        _area2D = GetNode<Area2D>("Sprite/Area2D");
    }

    private void PlayAnimation1()
    {
        _animationPlayer.Play("CoinPickUp");
        _area2D.Monitorable = false;
        
    }
}
