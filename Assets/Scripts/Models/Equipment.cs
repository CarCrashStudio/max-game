using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType { ARMOR, WEAPON }
public enum EquipmentSlotType { HEAD, TORSO, LEGS, BOOT, MAINHAND, OFFHAND, POTION }

[Serializable]
[CreateAssetMenu]
public class Equipment : Item, IHasCooldown
{
    public EquipmentType type;
    public EquipmentSlotType slot;
    public Modifier modifier;

    [SerializeField] private int id;
    [SerializeField] private float cooldownTime;

    private CooldownManager cooldownManager => GameObject.FindObjectOfType<CooldownManager>();

    public int ID => id;

    public float CooldownTime => cooldownTime;

    //public AttackType attackType;
    //private IAttack attack;

    public override void Use()
    {
        if (cooldownManager.IsOnCooldown(ID)) { return; }

        base.Use();

        cooldownManager.PutOnCooldown(this);
    }
}
