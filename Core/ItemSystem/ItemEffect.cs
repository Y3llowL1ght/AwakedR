using Godot;
using System;

public class ItemEffect : Node
{
    public string Title;
    public string Description;
    
}

public interface IActiveEffect
{
    double Cooldown{get;set;}
}

public interface ITriggerEffect
{
    
}

public interface IPassiveEffect
{
    
}

public interface IEquipmentEffect
{
    
}

