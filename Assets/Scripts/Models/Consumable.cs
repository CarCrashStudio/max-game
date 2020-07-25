using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableType { BUFF, HEALTH, CURRENCY }
[Serializable]
public class Consumable : Item
{
    public ConsumableType type;
    public float amount;

    public override void Use(Entity target)
    {
        //switch (type)
        //{
        //    case ConsumableType.HEALTH:
        //        target.health.Heal(amount);
        //        //if (target.healthSignal != null)
        //            //target.healthSignal.Raise();
        //        break;
        //    case ConsumableType.CURRENCY:
        //        ((PlayerMovement)target).gold += amount;
        //        break;
        //}
    }
}
