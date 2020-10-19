using UnityEngine;
[CreateAssetMenu(fileName = "New Armor", menuName = "Items/Equipment/Armor")]
public class Armor : Equipment
{
    [SerializeField] private ArmorProficiencies[] armorProficienciesSatisfied;
    public ArmorProficiencies[] ArmorProficienciesSatisfied => armorProficienciesSatisfied;
}