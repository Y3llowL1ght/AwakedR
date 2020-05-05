using Godot;
using System;
using System.Collections.Generic;

public class PlayerInventory : Node
{
    public int Size = 4;
    public List<InventorySlot> InventoryBackpack;
    public InventorySlot ArmorChestSlot;
    public InventorySlot ArmorHeadSlot;
    public InventorySlot WeaponSlot;
    public InventorySlot SuitUpgradeA;
    public InventorySlot SuitUpgradeB;
    public InventorySlot SuitUpgradeC;

    public override void _Ready()
    {
        //backpack nodes init
        InventoryBackpack = new List<InventorySlot>();
        for (int i = 0; i < Size; i++)
        {
            var slot = new InventorySlot();
            AddChild(slot);
            slot.Name = $"InventorySlot{i}";
            InventoryBackpack.Add(slot);
        }

        //Custom player nodes init
        ArmorChestSlot = new InventorySlot();
        ArmorHeadSlot = new InventorySlot();
        WeaponSlot = new InventorySlot();
        SuitUpgradeA = new InventorySlot();
        SuitUpgradeB = new InventorySlot();
        SuitUpgradeC = new InventorySlot();

        AddChild(ArmorChestSlot);
        AddChild(ArmorHeadSlot);
        AddChild(WeaponSlot);
        AddChild(SuitUpgradeA);
        AddChild(SuitUpgradeB);
        AddChild(SuitUpgradeC);

        ArmorChestSlot.Name = "ArmorChestSlot";
        ArmorHeadSlot.Name = "ArmorHeadSlot";
        WeaponSlot.Name = "WeaponSlot";
        SuitUpgradeA.Name = "SuitUpgradeA";
        SuitUpgradeB.Name = "SuitUpgradeB";
        SuitUpgradeC.Name = "SuitUpgradeC";

        ChangeItemSlot(GetNode<Item>("InventorySlotX/WeaponX"),WeaponSlot);
        GD.Print();

    }

    public bool ChangeItemSlot(Item item, InventorySlot slot){
        if (slot.Occupied != true)
        {
            var prevslot =  item.GetParent();
            if (typeof(InventorySlot) != prevslot.GetType()) return false;
            InventorySlot PreviousSlot = (InventorySlot)prevslot;
            PreviousSlot.RemoveChild(item);
            PreviousSlot.Occupied = false;
            PreviousSlot.Item = null;
            slot.AddChild(item);
            slot.Item = item;
            slot.Occupied = true;
            return true;
        }else return false;
    }


}
