using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    public int questCount => quests.Count;
    [SerializeField] protected List<Quest> quests;

    public List<Quest> Quests
    {
        get
        {
            if (quests == null) { quests = new List<Quest>(); }
            return quests;
        }
    }
    private void Awake()
    {
        quests = new List<Quest>();
    }
    public void AddQuest (Quest quest)
    {
        quests.Add(quest);
        GameEvents.ChangesMade();
    }
    public void RemoveQuest(Quest quest)
    {
        quests.Remove(quest);
        GameEvents.ChangesMade();
    }
}