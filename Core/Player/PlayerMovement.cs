using Godot;
using System;

public class PlayerMovement : KinematicBody2D
{   
    [Export]
    private float MaxSpeed = 500;
    [Export]
    private float Acceleration = 2000;
    Vector2 Motion = Vector2.Zero;

    public override void _Process(float delta)
    {
        Vector2 axis = GetMovementInput();
        if(axis == Vector2.Zero){
            ApplyFriction(Acceleration * delta);
        }else{
            ApplyAcceleration(axis * Acceleration * delta);
        }
        Motion = MoveAndSlide(Motion);
    }

    Vector2 GetMovementInput(){
        Vector2 axis = Vector2.Zero;
        axis.x = Convert.ToInt32(Input.IsActionPressed("ui_right")) - Convert.ToInt32(Input.IsActionPressed("ui_left"));
        axis.y = Convert.ToInt32(Input.IsActionPressed("ui_down")) - Convert.ToInt32(Input.IsActionPressed("ui_up"));
        return axis.Normalized();
    }

    void ApplyFriction(float friction){
        Motion = Motion.Length() > friction ? Motion.Normalized()*friction : Vector2.Zero; 
    }

    void ApplyAcceleration(Vector2 acceleration){
        Motion += acceleration;
        Motion = Motion.Clamped(MaxSpeed);
    }
}
