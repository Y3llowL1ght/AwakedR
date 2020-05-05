using Godot;
using System;
using System.Collections.Generic;

public class Item : Node
{
    [Export]public string Title;
    [Export]public string Description;
    [Export]public Texture Icon;

    [Export]public Rarity Rarity;
    [Export]public ItemType Type;

    [Export]public List<ItemEffect> Effects;

}





public enum Rarity
{
    T1, T2, T3, T4, T5, T6
}

public enum ItemType{
    Weapon, ArmorChest, ArmorHead, SuitUpgrade, Consumable
}

public enum EffectType{
    Active, Passive
}