using System;
using UnityEngine;

public class Item  : ScriptableObject, IHasTooltip
{
    [SerializeField] private new string name;
    [SerializeField] private string description;
    [SerializeField] private bool isDiscovered;

    [SerializeField] private Rarity rarity;

    public Sprite sprite;

    public virtual void Use()
    {
        if (!isDiscovered) { Discover(); }
    }

    public string Name
    {
        get
        {
            if (isDiscovered) { return name; }
            else { return "???"; }
        }
    }
    public string ColouredName 
    { 
        get 
        {
            if (isDiscovered)
            {
                return $"<color=#{ColorUtility.ToHtmlStringRGB(rarity.TextColour)}>{name}</color>";
            }
            else
            {
                return "???";
            }
        } 
    }
    public string Description
    {
        get
        {
            if (isDiscovered) { return description; }
            else { return "???"; }
        }
    }
    public bool IsDiscovered { get { return isDiscovered; } }
    public Rarity Rarity { get { return rarity; } }

    protected void Discover ()
    {
        isDiscovered = true;
    }

    public virtual string GetTooltipInfoText()
    {
        return string.Empty;
    }
}
