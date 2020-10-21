using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IHasQuests
{
    Signal PlayerQuestAddedSignal { get; }
    Signal QuestRemovedSignal { get; }
    List<Quest> QuestsAvailable { get; }

    void TakeQuest(Quest quest);
}