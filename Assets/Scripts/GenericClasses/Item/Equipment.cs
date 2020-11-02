using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum WeaponProficiencies { SIMPLE_MELEE, SIMPLE_RANGED }
public enum ArmorProficiencies { LIGHT, MEDIUM, HEAVY }
public enum EquipmentType { ARMOR, WEAPON }
public enum EquipmentSlotType { HEAD, TORSO, LEGS, BOOT, MAINHAND, OFFHAND, POTION }

[Serializable]
public class Equipment : Item
{
    [JsonConverter(typeof(StringEnumConverter))]
    [SerializeField] private EquipmentSlotType slot;
    [SerializeField] private Modifier modifier;

    public Equipment(int id, string name, string description, bool isDiscovered, Rarity rarity, EquipmentSlotType slot, Modifier modifier, EquipmentType type)
        : base(id, name, description, isDiscovered, rarity)
    {
        this.slot = slot;
        this.modifier = modifier;
        Type = type;
    }
    [JsonConstructor]
    public Equipment(int id, string name, string description, bool isDiscovered, string rarityName, EquipmentSlotType slot, Modifier modifier, EquipmentType type)
        : base(id, name, description, isDiscovered, rarityName)
    {
        this.slot = slot;
        this.modifier = modifier;
        Type = type;
    }

    public EquipmentSlotType Slot => slot;
    public virtual EquipmentType Type { get; }
    public Modifier Modifier => modifier;
    
    public override void Use()
    {
        base.Use();
    }
    public override string GetTooltipInfoText()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<size=35>").Append(ColouredName).Append("</size>").AppendLine();
        builder.Append(Rarity.Name).AppendLine();
        builder.Append(Description).AppendLine();

        return builder.ToString();
    }
}
