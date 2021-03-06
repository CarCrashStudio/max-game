﻿using UnityEngine;
public class Health : MonoBehaviour
{
    [SerializeField] protected Signal healthSignal;
    protected void RaiseSignal()
    {
        if (healthSignal != null)
        {
            healthSignal.Raise();
        }
    }

    [SerializeField] protected FloatValue maxHealth;
    [SerializeField] protected float currentHealth;

    public FloatValue MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;

    public void Start()
    {
        currentHealth = maxHealth.initialValue;
    }

    public string GetHealth ()
    {
        return string.Concat(currentHealth.ToString(), "/", maxHealth.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amt"></param>
    public virtual void Heal (float amt)
    {
        currentHealth += amt;
        if (currentHealth > maxHealth.initialValue)
            currentHealth = maxHealth.initialValue;
        maxHealth.runtimeValue = currentHealth;
    }
    public virtual void FullHeal ()
    {
        currentHealth = maxHealth.initialValue;
        maxHealth.runtimeValue = currentHealth;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amt"></param>
    public virtual void Damage(float amt)
    {
        currentHealth -= amt;
        if (currentHealth < 0)
            currentHealth = 0;
        maxHealth.runtimeValue = currentHealth;
    }
    public virtual void InstantKill()
    {
        currentHealth = 0;
        maxHealth.runtimeValue = currentHealth;
    }

    public void Load (float currentHealth, float maxHealth)
    {
        this.currentHealth = currentHealth;
        this.maxHealth.runtimeValue = maxHealth;

        RaiseSignal();
    }
}
