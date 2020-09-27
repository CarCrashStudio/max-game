using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffTarget { STRENGTH, DEXTERITY, CONSTITUTION, INTELLIGENCE, WISDOM, CHARISMA, HEALTH, CURRENCY }
[Serializable]
public class Potion : Item
{
    [SerializeField] private bool isDrinkable;
    [SerializeField] private bool isThrowable;
    [SerializeField] private BuffTarget buffTarget;
    [SerializeField] private int buffAmount;
    [SerializeField] private float throwRadius;

    public override void Start()
    {

    }

    public override void Update()
    {

    }

    public override void Use()
    {
        // call the base Use to discover the item if it isn't already
        base.Use();

        if (isDrinkable) 
        { 
            ApplyBuff(
                FindObjectOfType<Player>()); 
        }
        
    }

    private void ApplyBuff (object target)
    {
        switch (buffTarget)
        {
            case BuffTarget.HEALTH:
                ((Entity)target).health.Heal(buffAmount);
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
