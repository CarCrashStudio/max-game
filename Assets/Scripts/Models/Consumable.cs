using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableType { BUFF, HEALTH }
public class Consumable : Item
{
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
        }
    }
}
