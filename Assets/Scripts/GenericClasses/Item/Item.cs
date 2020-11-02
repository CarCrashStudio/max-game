using JsonKnownTypes;
using Newtonsoft.Json;
using System;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
[JsonConverter(typeof(JsonKnownTypesConverter<Item>))]
[JsonKnownType(typeof(Item), "item")]
//[JsonKnownType(typeof(Equipment), "equipment")]
[JsonKnownType(typeof(Weapon), "weapon")]
[JsonKnownType(typeof(Armor), "armor")]
[JsonKnownType(typeof(Potion), "potion")]
public class Item  : IHasTooltip
{
    [JsonProperty] [SerializeField] protected int id = 0;
    [SerializeField] [JsonProperty] private string name;
    [SerializeField] [JsonProperty] private string description;
    [SerializeField] [JsonProperty] private bool isDiscovered;

    [JsonProperty] private string rarityName;
    [SerializeField] private Rarity rarity;

    public Sprite sprite;

    public Item (int id, string name, string description, bool isDiscovered, Rarity rarity)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.isDiscovered = isDiscovered;
        this.rarity = rarity;
    }
    [JsonConstructor]
    public Item(int id, string name, string description, bool isDiscovered, string rarityName)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.isDiscovered = isDiscovered;
        this.rarity = GameManager.Rarities.Db.Where(r => r.Name == rarityName).FirstOrDefault();
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
