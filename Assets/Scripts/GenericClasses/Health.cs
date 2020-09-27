using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu]
public class Health : ScriptableObject
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;

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
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
    public virtual void FullHeal ()
    {
        currentHealth = maxHealth;
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
    }
    public virtual void InstantKill()
    {
        currentHealth = 0;
    }
}
