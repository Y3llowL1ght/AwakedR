using Godot;
using System;

public class PassiveEffect : ItemEffect
{
    
    public override void _Ready()
    {
        
    }

    public void ApplyEffect(){
        var stats = GetNode<PlayerStats>("../../../../PlayerStats");
        GD.Print(stats.Health);
        stats.TakeHealthDamage(50);
        GD.Print(stats.Health);
    }

    public void RemoveEffect(){
        GD.Print("No longer applied");
    }
}
