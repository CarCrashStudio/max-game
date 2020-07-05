using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableType { BUFF, HEALTH, CURRENCY }
[Serializable]
public class Consumable : Item
{
    public Signal healthSignal;
    public ConsumableType type;
    public float amount;

    public override void Use(Entity target)
    {
        switch (type)
        {
            case ConsumableType.HEALTH:
                target.currentHealth.initialValue += amount;
                if (healthSignal != null)
                    healthSignal.Raise();
                break;
            case ConsumableType.CURRENCY:
                ((MaxAttributes)target).gold += amount;
                if (healthSignal != null)
                    healthSignal.Raise();
                break;
        }
    }
}
