using Godot;
using System;

public class Pointer : Node2D
{
    private int _direction = 1;
    [Export]    
    private int _rotationSpeed;
    [Signal]
    public delegate void CoinCollected();
    [Signal]
    public delegate void LoseGame();

    Area2D _pointerArea;

    private int _currentSpeed;

    public override void _Ready()
    {
        _pointerArea = GetNode<Area2D>("Key/Area2D");

        Random random =  new Random();



        int x = random.Next();

        if (x % 2 == 0)
        {
            _direction = 1;
        }
        else
        {
            _direction = -1;
        }
    }

    public override void _Process(float delta)
    {
        Rotate(_currentSpeed * _direction * delta);
    }

    private void ChangeDirection()
    {
        if (_direction == 1)
        {
            _direction = -1;
        }
        else
        {
            _direction = 1;
        }
    }

    private void CheckForScore(){
        GD.Print(_pointerArea.GetOverlappingAreas().Count);
        if (_pointerArea.GetOverlappingAreas().Count != 0)
        {
            Area2D coin = (Area2D)_pointerArea.GetOverlappingAreas()[0];

            coin.Owner.Call("PlayAnimation1");
            
            EmitSignal(nameof(CoinCollected));
        }
        else
        {
            EmitSignal(nameof(LoseGame));
        }
    }

    private void ToggleMovement()
    {

        if (_currentSpeed == 0)
        {
            _currentSpeed = _rotationSpeed;            
        }
        else
        {
            _currentSpeed = 0;
        }
    }

    public override void _Input(InputEvent @event)
    {
        
       if (@event is InputEventScreenTouch && _currentSpeed != 0)
        {
            if (@event.IsPressed())
            {
                GD.Print("Tapped");
                ChangeDirection();
                CheckForScore();
            }




        }
    }
}
