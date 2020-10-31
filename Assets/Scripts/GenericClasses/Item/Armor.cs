using Newtonsoft.Json;
using UnityEngine;
[CreateAssetMenu(fileName = "New Armor", menuName = "Items/Equipment/Armor")]
public class Armor : Equipment
{
    [SerializeField] private ArmorProficiencies armorProficiencySatisfied;

    [JsonConstructor]
    public Armor(string name, string description, bool isDiscovered, Rarity rarity, EquipmentSlotType slot, Modifier modifier, EquipmentType type, ArmorProficiencies armorProficiencySatisfied) 
        : base(name, description, isDiscovered, rarity, slot, modifier, type)
    {
        this.armorProficiencySatisfied = armorProficiencySatisfied;
    }

    public ArmorProficiencies ArmorProficiencySatisfied => armorProficiencySatisfied;
}