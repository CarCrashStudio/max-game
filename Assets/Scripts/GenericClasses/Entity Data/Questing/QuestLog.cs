using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    public int questCount => quests.Count;
    [SerializeField] protected List<Quest> quests;
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