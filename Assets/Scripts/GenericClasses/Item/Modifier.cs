using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class Modifier
{
    [UnityEngine.SerializeField] [JsonProperty] private int strength_modifier = 0;
    [UnityEngine.SerializeField] [JsonProperty] private int dexterity_modifier = 0;
    [UnityEngine.SerializeField] [JsonProperty] private int constitution_modifier = 0;
    [UnityEngine.SerializeField] [JsonProperty] private int intelligence_modifier = 0;
    [UnityEngine.SerializeField] [JsonProperty] private int wisdom_modifier = 0;
    [UnityEngine.SerializeField] [JsonProperty] private int charisma_modifier = 0;

    public int Strength { get { return strength_modifier; } }
    public int Dexterity { get { return dexterity_modifier; } }
    public int Constitution { get { return constitution_modifier; } }
    public int Intelligence { get { return intelligence_modifier; } }
    public int Wisdom { get { return wisdom_modifier; } }
    public int Charisma { get { return charisma_modifier; } }

    [JsonConstructor]
    public Modifier (int strength_modifier = 0, int dexterity_modifier = 0, int constituiton_modifier = 0, int intelligence_modifier = 0, int wisdom_modifier = 0, int charisma_modifier = 0)
    {
        this.strength_modifier = strength_modifier;
        this.dexterity_modifier = dexterity_modifier;
        this.constitution_modifier = constituiton_modifier;
        this.intelligence_modifier = intelligence_modifier;
        this.wisdom_modifier = wisdom_modifier;
        this.charisma_modifier = charisma_modifier;
    }

    public void SetStrength (int strength)
    {
        strength_modifier = strength;
    }
    public void SetDexterity(int dexterity)
    {
        dexterity_modifier = dexterity;
    }
    public void SetConstitution(int constitution)
    {
        constitution_modifier = constitution;
    }
    public void SetIntelligence(int intelligence)
    {
        intelligence_modifier = intelligence;
    }
    public void SetWisdom(int wisdom)
    {
        wisdom_modifier = wisdom;
    }
    public void SetCharisma(int charisma)
    {
        charisma_modifier = charisma;
    }

    public static Modifier operator +(Modifier left, Modifier right) {
        Modifier temp = new Modifier();
        temp.SetStrength(left.Strength + right.Strength);
        temp.SetDexterity(left.Dexterity + right.Dexterity);
        temp.SetConstitution(left.Constitution + right.Constitution);
        temp.SetWisdom(left.Wisdom + right.Wisdom);
        temp.SetIntelligence(left.Intelligence + right.Intelligence);
        temp.SetCharisma(left.Charisma + right.Charisma);

        //GameEvents.ChangesMade();

        return temp;
    }
    public static Modifier operator -(Modifier left, Modifier right)
    {
        Modifier temp = new Modifier();
        temp.SetStrength(left.Strength - right.Strength);
        temp.SetDexterity(left.Dexterity - right.Dexterity);
        temp.SetConstitution(left.Constitution - right.Constitution);
        temp.SetWisdom(left.Wisdom - right.Wisdom);
        temp.SetIntelligence(left.Intelligence - right.Intelligence);
        temp.SetCharisma(left.Charisma - right.Charisma);

        //GameEvents.ChangesMade();

        return temp;
    }

    public override string ToString()
    {
        return base.ToString();
        //return $"Strength: {strength_modifier}\nDexterity: {dexterity_modifier}\nConstitution: {constitution_modifier}\nWisdom: {wisdom_modifier}\nIntelligence: {intelligence_modifier}\nCharisma: {charisma_modifier}\n";
    }
}
