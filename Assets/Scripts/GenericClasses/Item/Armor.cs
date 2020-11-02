using Newtonsoft.Json;
using UnityEngine;
public class Armor : Equipment
{
    [SerializeField] private ArmorProficiencies armorProficiencySatisfied;

    public Armor(int id, string name, string description, bool isDiscovered, Rarity rarity, EquipmentSlotType slot, Modifier modifier, EquipmentType type, ArmorProficiencies armorProficiencySatisfied) 
        : base(id, name, description, isDiscovered, rarity, slot, modifier, type)
    {
        this.armorProficiencySatisfied = armorProficiencySatisfied;
    }
    [JsonConstructor]
    public Armor(int id, string name, string description, bool isDiscovered, string rarityName, EquipmentSlotType slot, Modifier modifier, EquipmentType type, ArmorProficiencies armorProficiencySatisfied)
        : base(id, name, description, isDiscovered, rarityName, slot, modifier, type)
    {
        this.armorProficiencySatisfied = armorProficiencySatisfied;
    }

    public ArmorProficiencies ArmorProficiencySatisfied => armorProficiencySatisfied;
}