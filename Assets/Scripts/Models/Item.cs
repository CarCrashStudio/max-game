using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public Signal healthSignal;

    [Header("Details")]
    public string itemName;
    public string pluralName;
    public float cost;

    public abstract void Use(Entity target);
}
