using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffTarget { STRENGTH, DEXTERITY, CONSTITUTION, INTELLIGENCE, WISDOM, CHARISMA, HEALTH, CURRENCY }
[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class Potion : Item, IHasCooldown
{
    [JsonProperty] [SerializeField] private int id = 0;
    [JsonProperty] [SerializeField] private float cooldownTime = 30;
    [JsonProperty] [SerializeField] private bool isDrinkable;
    [JsonProperty] [SerializeField] private bool isThrowable;
    [JsonProperty] [SerializeField] private BuffTarget buffTarget;
    [JsonProperty] [SerializeField] private int buffAmount;
    [JsonProperty] [SerializeField] private float throwRadius;

    [JsonConstructor]
    public Potion(int id, string name, string description, bool isDiscovered, Rarity rarity, float cooldownTime, bool isDrinkable, bool isThrowable, BuffTarget buffTarget, int buffAmount, float throwRadius) : base(name, description, isDiscovered, rarity)
    {
        this.id = id;
        this.cooldownTime = cooldownTime;
        this.isDrinkable = isDrinkable;
        this.isThrowable = isThrowable;
        this.buffTarget = buffTarget;
        this.buffAmount = buffAmount;
        this.throwRadius = throwRadius;
    }

    private CooldownManager cooldownManager => GameObject.FindObjectOfType<CooldownManager>();

    public int ID => id;
    public float CooldownTime => cooldownTime;
    public override void Use()
    {
        if (cooldownManager.IsOnCooldown(ID)) { return; }
        // call the base Use to discover the item if it isn't already
        base.Use();

        if (isDrinkable) 
        { 
            ApplyBuff(
                GameObject.FindObjectOfType<Player>()); 
        }

        cooldownManager.PutOnCooldown(this);
    }

    private void ApplyBuff (object target)
    {
        switch (buffTarget)
        {
            case BuffTarget.HEALTH:
                if (target is Player) { }
                    //((Player)target).health.Heal(buffAmount);
                else { }
                    //((Enemy)target).health.Heal(buffAmount);
                break;
            case BuffTarget.STRENGTH:
                var str = ((Entity)target).attributes.buffModifiers.Strength;
                ((Entity)target).attributes.buffModifiers.SetStrength (buffAmount + str);
                break;
            case BuffTarget.DEXTERITY:
                var dex = ((Entity)target).attributes.buffModifiers.Dexterity;
                ((Entity)target).attributes.buffModifiers.SetDexterity(buffAmount + dex);
                break;
            case BuffTarget.CONSTITUTION:
                var con = ((Entity)target).attributes.buffModifiers.Constitution;
                ((Entity)target).attributes.buffModifiers.SetConstitution(buffAmount + con);
                break;
            case BuffTarget.WISDOM:
                var wis = ((Entity)target).attributes.buffModifiers.Wisdom;
                ((Entity)target).attributes.buffModifiers.SetWisdom(buffAmount + wis);
                break;
            case BuffTarget.INTELLIGENCE:
                var inte = ((Entity)target).attributes.buffModifiers.Intelligence;
                ((Entity)target).attributes.buffModifiers.SetIntelligence(buffAmount + inte);
                break;
            case BuffTarget.CHARISMA:
                var cha = ((Entity)target).attributes.buffModifiers.Charisma;
                ((Entity)target).attributes.buffModifiers.SetCharisma(buffAmount + cha);
                break;
        }
    }
}
