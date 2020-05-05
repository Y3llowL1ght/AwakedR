using Godot;
using System;

public class PlayerMovement : KinematicBody2D
{   

    private float MaxSpeed;
    private float Acceleration;
    Vector2 Motion = Vector2.Zero;

    public override void _PhysicsProcess(float delta)
    {
        Vector2 inputVector = GetMovementInput();
        if(inputVector != Vector2.Zero){
            Motion = Motion.MoveToward(inputVector * MaxSpeed, Acceleration * delta);
        }else{
           Motion = Motion.MoveToward(Vector2.Zero, Acceleration * delta);
        }
        Motion = MoveAndSlide(Motion);
        
        var direction = GetGlobalMousePosition() - GlobalPosition;
        // Don't move if too close to the mouse pointer
        if (direction.Length() > 10)
        {
            Rotation = direction.Angle();
            
        }
    }

    public override void _Ready(){
        
        //MaxSpeed = (float)PlayerStats;
        Acceleration = 10000;
    }

    Vector2 GetMovementInput(){
        Vector2 axis = Vector2.Zero;
        axis.x = Input.GetActionStrength("mov_right") - Input.GetActionStrength("mov_left");
        axis.y = Input.GetActionStrength("mov_down") - Input.GetActionStrength("mov_up");
        return axis.Normalized();
    }

    
}
