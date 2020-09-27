using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType { ARMOR, WEAPON }
public enum EquipmentSlot { HEAD, TORSO, LEGS, BOOT, ARMS, MAINHAND }

[Serializable]
public class Equipment : Item
{
    public EquipmentType type;
    public EquipmentSlot slot;
    public Modifier modifier;

    public AttackType attackType;
    private IAttack attack;

    public override void Start ()
    {
        Debug.Log("Start Hit");
        switch (attackType)
        {
            case AttackType.MELEE:
                attack = new MeleeAttack();
                break;
            case AttackType.RANGED:
                attack = new RangedAttack();
                break;
        }
    }
    public override void Update()
    {
    }
    public override void Use()
    {

    }

    public void Attack (Entity attacker, Entity target)
    {
        if (attack != null)
            attack.Attack(attacker, target);
    }

    
}
