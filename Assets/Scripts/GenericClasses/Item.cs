using System;
using UnityEngine;

[CreateAssetMenu]
public class Item  : ScriptableObject
{
    [Header("Details")]
    public string name;
    public string description;
    public bool isDiscovered;

    public Sprite sprite;

    public virtual void Use()
    {
        if (!isDiscovered) { Discover(); }
    }

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
