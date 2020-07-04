using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public GameObject item;
    public float lootChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;

    public GameObject GetLoot ()
    {
        float currentChance = Random.Range(0, 1);
        for (int i = 0; i < loots.Length; i++)
            if (currentChance <= loots[i].lootChance)
                return loots[i].item;
        return null;
    }
}
