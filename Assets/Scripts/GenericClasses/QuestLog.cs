using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    public int questCount => quests.Count;
    [SerializeField] private List<Quest> quests;
    private void Awake()
    {
        quests = new List<Quest>();
    }
    public void AddQuest (Quest quest)
    {
        quests.Add(quest);
    }
    public void RemoveQuest(Quest quest)
    {
        quests.Remove(quest);
    }
}
public class Quest
{
    bool completed = false;

    private string name;
    private string description;

    private LootTable rewardTable;
    private float rewardExp;

    public Quest(string name, string description, LootTable rewardTable = null, float rewardExp = 0f)
    {
        this.name = name;
        this.description = description;
        this.rewardTable = rewardTable;
        this.rewardExp = rewardExp;
    }

    public bool Completed => completed;

    public string Name => name; 
    public string Description => description; 
    public LootTable RewardTable => rewardTable; 
    public float RewardExp => rewardExp; 
}
