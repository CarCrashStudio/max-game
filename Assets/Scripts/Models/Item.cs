using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Item : MonoBehaviour
{
    [Header("Details")]
    public string name;
    public string pluralName;
    public float cost;

    public abstract void Use(Entity target);
}
