using UnityEngine;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Converters;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class Weapon : Equipment, IHasCooldown
{
    [SerializeField] private IAttack attack;

    [JsonConverter(typeof(StringEnumConverter))]
    [SerializeField] private Die rolledDie;
    [JsonConverter(typeof(StringEnumConverter))]
    [SerializeField] private WeaponProficiencies weaponProficiencySatisfied;
    [JsonProperty] [SerializeField] private float cooldownTime;

    public Weapon(int id, string name, string description, bool isDiscovered, Rarity rarity, EquipmentSlotType slot, Modifier modifier, EquipmentType type) 
        : base(id, name, description, isDiscovered, rarity, slot, modifier, type)
    {

    }
    [JsonConstructor]
    public Weapon(int id, string name, string description, bool isDiscovered, string rarityName, EquipmentSlotType slot, Modifier modifier, EquipmentType type)
        : base(id, name, description, isDiscovered, rarityName, slot, modifier, type)
    {

    }

    private CooldownManager cooldownManager => GameObject.FindObjectOfType<CooldownManager>();

    public int ID => id;
    public float CooldownTime => cooldownTime;
    public Die RolledDie => rolledDie;
    public WeaponProficiencies WeaponProficiencySatisfied => weaponProficiencySatisfied;
    public override EquipmentType Type => EquipmentType.WEAPON;

    public void Attack (Entity attacker, Animator animator, GameObject meleePoint, float meleeRadius, LayerMask enemyLayers)
    {
        if (cooldownManager.IsOnCooldown(ID)) { return; }
        if (attack == null)
        {
            if (WeaponProficiencySatisfied == WeaponProficiencies.SIMPLE_MELEE)
            {
                attack = new MeleeAttack();
            }
        }
        attack.Attack(attacker, this, animator, meleePoint, meleeRadius, enemyLayers);
        cooldownManager.PutOnCooldown(this);
    }
}