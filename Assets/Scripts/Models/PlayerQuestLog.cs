using UnityEngine;
using System.Collections;
using UnityEngine.PlayerLoop;
using System.Collections.Generic;
using UnityEngine.Timeline;
using System.Linq;
using TMPro;
using System;
using UnityEngine.UI;

public class PlayerQuestLog : QuestLog
{
    [SerializeField] GameObject QuestLogUI;
    [SerializeField] Transform QuestLogLayout;
    [SerializeField] GameObject QuestInfoUI;

    List<GameObject> questElements;

    private void Awake ()
    {
        questElements = new List<GameObject>();
        quests = new List<Quest>();
    }
    public void AddQuestElement ()
    {
        if (quests.Count == 0) { return; }

        var quest = quests.Last();

        Debug.Log(quest);
    }
    public void ToggleUI ()
    {
        QuestLogUI.SetActive(!QuestLogUI.activeSelf);

        if (QuestLogUI.activeSelf)
        {
            foreach (Quest quest in quests)
            {
                // create a new quest element in layout
                var element = Instantiate(Resources.Load<GameObject>("UI/QuestElement"), QuestLogLayout);

                TextMeshProUGUI text = element.GetComponentInChildren<TextMeshProUGUI>();
                Button button = element.GetComponentInChildren<Button>();

                GameObject completedImage = element.transform.GetChild(0).GetChild(0).gameObject;
                completedImage.SetActive(quest.Completed);

                text.text = $"<size=30><color=black>{quest.Name}</color></size>{Environment.NewLine}{quest.Description}";

                element.GetComponent<QuestElement>().QuestInfoUI = QuestInfoUI;
                element.GetComponent<QuestElement>().QuestLogUI = QuestLogUI;
                element.GetComponent<QuestElement>().quest = quest;

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(delegate { QuestLogUI.SetActive(false); element.GetComponent<QuestElement>().OpenQuestInfo(isQuestGiver: false); });

                questElements.Add(element);
            }
        }
        else
        {
            foreach (GameObject element in questElements)
                Destroy(element);
            questElements.Clear();
        }
    }
}
