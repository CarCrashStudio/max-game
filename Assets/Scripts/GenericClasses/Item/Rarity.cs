using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

[CreateAssetMenu()]
[JsonObject(MemberSerialization.OptIn)]
public class Rarity
{
    [SerializeField] [JsonProperty] private string name;
    [SerializeField] [JsonProperty] private Color textColour;

    public string Name { get { return name; } }
    public Color TextColour { get { return textColour; } }
}
