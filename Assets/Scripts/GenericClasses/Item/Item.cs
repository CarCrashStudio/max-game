using Newtonsoft.Json;
using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class Item  : IHasTooltip
{
    [SerializeField] [JsonProperty] private string name;
    [SerializeField] [JsonProperty] private string description;
    [SerializeField] [JsonProperty] private bool isDiscovered;

    [SerializeField] [JsonProperty] private Rarity rarity;

    public Sprite sprite;

    [JsonConstructor]
    public Item (string name, string description, bool isDiscovered, Rarity rarity)
    {
        this.name = name;
        this.description = description;
        this.isDiscovered = isDiscovered;
        this.rarity = rarity;
    }

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
    public virtual void Awake()
    {

    }
    public virtual string GetTooltipInfoText()
    {
        return string.Empty;
    }
}
