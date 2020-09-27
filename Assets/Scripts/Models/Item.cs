using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Item : MonoBehaviour
{
    [Header("Details")]
    public string name;
    public string description;
    public bool isDiscovered; 

    public virtual void Use()
    {
        if (!isDiscovered) { Discover(); }
    }
    public abstract void Start();
    public abstract void Update();

    public string GetName ()
    {
        if (isDiscovered) { return name; }
        else { return "???"; }
    }
    public string GetDescription()
    {
        if (isDiscovered) { return description; }
        else { return "???"; }
    }

    protected void Discover ()
    {
        isDiscovered = true;
    }
}
