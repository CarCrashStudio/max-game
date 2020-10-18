using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum EquipmentType { ARMOR, WEAPON }
public enum EquipmentSlotType { HEAD, TORSO, LEGS, BOOT, MAINHAND, OFFHAND, POTION }

[Serializable]
[CreateAssetMenu(fileName = "New Equipment", menuName = "Items/Equipment")]
public class Equipment : Item, IHasCooldown
{
    public EquipmentType type;
    public EquipmentSlotType slot;
    public Modifier modifier;
    private CooldownManager cooldownManager => GameObject.FindObjectOfType<CooldownManager>();

    [SerializeField] private int id;
    [SerializeField] private float cooldownTime;

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

    public override string GetTooltipInfoText()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<size=35>").Append(ColouredName).Append("</size>").AppendLine();
        builder.Append(Rarity.Name).AppendLine();
        builder.Append(Description).AppendLine();

        return builder.ToString();
    }
}
