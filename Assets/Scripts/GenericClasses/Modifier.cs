using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Modifier
{
    [UnityEngine.SerializeField] private int strength_modifier = 0;
    [UnityEngine.SerializeField] private int dexterity_modifier = 0;
    [UnityEngine.SerializeField] private int constitution_modifier = 0;
    [UnityEngine.SerializeField] private int intelligence_modifier = 0;
    [UnityEngine.SerializeField] private int wisdom_modifier = 0;
    [UnityEngine.SerializeField] private int charisma_modifier = 0;

    public int Strength { get { return strength_modifier; } }
    public int Dexterity { get { return dexterity_modifier; } }
    public int Constitution { get { return constitution_modifier; } }
    public int Intelligence { get { return intelligence_modifier; } }
    public int Wisdom { get { return wisdom_modifier; } }
    public int Charisma { get { return charisma_modifier; } }

    public Modifier (int strength = 0, int dexterity = 0, int constituiton = 0, int intelligence = 0, int wisdom = 0, int charisma = 0)
    {
        strength_modifier = strength;
        dexterity_modifier = dexterity;
        constitution_modifier = constituiton;
        intelligence_modifier = intelligence;
        wisdom_modifier = wisdom;
        charisma_modifier = charisma;
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
        return temp;
    }

    public override string ToString()
    {
        return $"Strength: {strength_modifier}\nDexterity: {dexterity_modifier}\nConstitution: {constitution_modifier}\nWisdom: {wisdom_modifier}\nIntelligence: {intelligence_modifier}\nCharisma: {charisma_modifier}\n";
    }
}
