using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System;

[CreateAssetMenu()]
[JsonObject(MemberSerialization.OptIn)]
[Serializable]
public class Rarity
{
    public struct Vector3
    {
        public int x,
                   y,
                   z;
    }

    [SerializeField] [JsonProperty] private string name;
    [JsonProperty] private Vector3 textColour;

    public string Name { get { return name; } }
    public Color TextColour { get { return new Color(textColour.x, textColour.y, textColour.z); } }

    [JsonConstructor]
    public Rarity (string name, Vector3 textColour)
    {
        this.name = name;
        this.textColour = textColour;
    }
}
