using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Attributes
{
    [SerializeField] protected int strength;
    [SerializeField] protected int dexterity;
    [SerializeField] protected int constitution;
    [SerializeField] protected int intelligence;
    [SerializeField] protected int wisdom;
    [SerializeField] protected int charisma;

    [SerializeField] private int strengthModifier = 0;
    [SerializeField] private int dexterityModifier = 0;
    [SerializeField] private int constitutionModifier = 0;
    [SerializeField] private int intelligenceModifier = 0;
    [SerializeField] private int wisdomModifier = 0;
    [SerializeField] private int charismaModifier = 0;

    public int StrengthModifier { get { return strengthModifier; } }
    public int DexterityModifier { get { return dexterityModifier; } }
    public int ConstitutionModifier { get { return constitutionModifier; } }
    public int IntelligenceModifier { get { return intelligenceModifier; } }
    public int WisdomModifier { get { return wisdomModifier; } }
    public int CharismaModifier { get { return charismaModifier; } }

    public void Start()
    {
        strengthModifier = (strength - 10) / 2;
        dexterityModifier = (dexterity - 10) / 2;
        constitutionModifier = (constitution - 10) / 2;
        intelligenceModifier = (intelligence - 10) / 2;
        wisdomModifier = (wisdom - 10) / 2;
        charismaModifier = (charisma - 10) / 2;
    }
    public void Update()
    {
        strengthModifier = (strength - 10) / 2;
        dexterityModifier = (dexterity - 10) / 2;
        constitutionModifier = (constitution - 10) / 2;
        intelligenceModifier = (intelligence - 10) / 2;
        wisdomModifier = (wisdom - 10) / 2;
        charismaModifier = (charisma - 10) / 2;
    }

    public void IncreaseStrength (int amt)
    {
        strength += amt;
    }
    public void IncreaseDexterity(int amt)
    {
        dexterity += amt;
    }
    public void IncreaseConstitution(int amt)
    {
        constitution += amt;
    }
    public void IncreaseIntelligence(int amt)
    {
        intelligence += amt;
    }
    public void IncreaseWisdom  (int amt)
    {
        wisdom += amt;
    }
    public void IncreaseCharisma(int amt)
    {
        charisma += amt;
    }

    public void DecreaseStrength(int amt)
    {
        strength -= amt;
        if (strength < 1)
            strength = 1;
    }
    public void DecreaseDexterity(int amt)
    {
        dexterity -= amt;
        if (dexterity < 1)
            dexterity = 1;
    }
    public void DecreaseConstitution(int amt)
    {
        constitution -= amt;
        if (constitution < 1)
            constitution = 1;
    }
    public void DecreaseIntelligence(int amt)
    {
        intelligence -= amt;
        if (intelligence < 1)
            intelligence = 1;
    }
    public void DecreaseWisdom(int amt)
    {
        wisdom -= amt;
        if (wisdom < 1)
            wisdom = 1;
    }
    public void DecreaseCharisma(int amt)
    {
        charisma -= amt;
        if (charisma < 1)
            charisma = 1;
    }
}

//[CustomEditor(typeof(Attributes))]
//public class AttributesEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        Attributes attributes = (Attributes)target;
//        DrawDefaultInspector();

//        if (GUILayout.Button("Roll Stats"))
//        {
//            attributes.IncreaseStrength(Random.Range(0, 20));
//            attributes.IncreaseDexterity(Random.Range(0, 20));
//            attributes.IncreaseConstitution(Random.Range(0, 20));
//            attributes.IncreaseIntelligence(Random.Range(0, 20));
//            attributes.IncreaseWisdom(Random.Range(0, 20));
//            attributes.IncreaseCharisma(Random.Range(0, 20));
//        }
//    }
//}
