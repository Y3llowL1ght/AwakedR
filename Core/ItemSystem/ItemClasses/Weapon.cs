using Godot;
using System;

public class Weapon : Item, IEquipmentItem
{
    public override void _Ready()
    {
        GD.Print("Im a Weapon");
    }
}
