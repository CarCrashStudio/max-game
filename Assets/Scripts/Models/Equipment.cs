using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType { ARMOR, WEAPON }
public enum EquipmentSlot { HEAD, TORSO, LEGS, BOOT, ARMS, MAINHAND }
public class Equipment : Item
{
    public EquipmentType type;
    public EquipmentSlot slot;

    // Equips the item in the appropriate slot
    public override void Use(Entity target)
    {
        throw new System.NotImplementedException();
    }
}
