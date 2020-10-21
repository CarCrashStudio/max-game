using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class QuestGiver : NPC, IHasQuests
{
    [SerializeField] private Signal playerQuestAddedSignal;
    [SerializeField] private Signal questRemovedSignal;
    [SerializeField] private List<Quest> questsAvailable;

    [SerializeField] private GameObject QuestInfoUI;
    [SerializeField] private GameObject QuestsAvailableUI;
    [SerializeField] private Transform QuestsAvailableLayout;

    private List<GameObject> elements;

    public Signal PlayerQuestAddedSignal => playerQuestAddedSignal;
    public List<Quest> QuestsAvailable => questsAvailable;

    public Signal QuestRemovedSignal => throw new System.NotImplementedException();

    private void Awake () { questsAvailable = new List<Quest>(); }

    public override void Interact(GameObject interacter)
    {
        if (Active) { return; }

        base.active = true;

        elements = new List<GameObject>();
        questsAvailable.Add(new Quest("Test Quest", "This is a Test", null, 15,
                                       new KillObjective ("log", Vector2.zero, 0f, "Kill 1 Log", false, 0, 1)));
        foreach(Quest quest in questsAvailable)
        {
            var element = Instantiate(Resources.Load<GameObject>("UI/QuestElement"), QuestsAvailableLayout);

            TextMeshProUGUI text = element.GetComponentInChildren<TextMeshProUGUI>();
            Button button = element.GetComponentInChildren<Button>();

            text.text = $"<size=30><color=black>{quest.Name}</color></size>{Environment.NewLine}{quest.Description}";

            element.GetComponent<QuestElement>().QuestInfoUI = QuestInfoUI;
            element.GetComponent<QuestElement>().QuestLogUI = QuestsAvailableUI;
            element.GetComponent<QuestElement>().quest = quest;

            Button declineButton = QuestInfoUI.transform.GetChild(5).GetComponent<Button>();
            Button acceptButton = QuestInfoUI.transform.GetChild(6).GetComponent<Button>();

            acceptButton.onClick.RemoveAllListeners();
            acceptButton.onClick.AddListener(delegate { 
                interacter.GetComponent<QuestLog>().AddQuest(quest);
                TakeQuest(quest);

                elements.Remove(element);
                Destroy(element);

                playerQuestAddedSignal.Raise();

                CloseQuestInfo();
            });
            declineButton.onClick.RemoveAllListeners();
            declineButton.onClick.AddListener(delegate {
                CloseQuestInfo();
            });

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(delegate { QuestsAvailableUI.SetActive(false); element.GetComponent<QuestElement>().OpenQuestInfo(isQuestGiver: true); });

            elements.Add(element);
        }

        QuestsAvailableUI.SetActive(true);
    }

    public void CloseUI ()
    {
        CloseQuestGiver();
    }

    private void CloseQuestGiver()
    {
        active = false;
        QuestsAvailableUI.SetActive(false);
        foreach (GameObject e in elements)
            Destroy(e);
    }
    private void CloseQuestInfo()
    {
        active = false;
        QuestInfoUI.SetActive(false);
        QuestsAvailableUI.SetActive(true);
    }

    public void TakeQuest(Quest quest)
    {
        questsAvailable.Remove(quest);

        //interacter.GetComponent<QuestLog>().AddQuest(quest);
        questRemovedSignal.Raise();
    }
}
