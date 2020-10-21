using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class QuestElement : MonoBehaviour
{
    // quest that it is for
    // reference to quest info ui

    public Quest quest;
    public GameObject QuestInfoUI;
    public GameObject QuestLogUI;

    public void OpenQuestInfo (bool isQuestGiver)
    {
        TextMeshProUGUI questNameText = QuestInfoUI.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI questDescriptionText = QuestInfoUI.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        Button returnButton = QuestInfoUI.transform.GetChild(4).GetComponent<Button>();

        TextMeshProUGUI expText = QuestInfoUI.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        Image lootImage = QuestInfoUI.transform.GetChild(3).GetComponentInChildren<Image>();

        Button declineButton = QuestInfoUI.transform.GetChild(5).GetComponent<Button>();
        Button acceptButton = QuestInfoUI.transform.GetChild(6).GetComponent<Button>();

        declineButton.gameObject.SetActive(isQuestGiver);
        acceptButton.gameObject.SetActive(isQuestGiver);

        questNameText.text = quest.Name;
        questDescriptionText.text = quest.Description;

        expText.text = $"{quest.RewardExp} XP";
        if (quest.RewardItem == null)
        {
            lootImage.gameObject.SetActive(false);
        }
        else
        {
            lootImage.sprite = quest.RewardItem.item.sprite;
        }

        returnButton.onClick.RemoveAllListeners();
        returnButton.onClick.AddListener(delegate { QuestInfoUI.SetActive(false);  QuestLogUI.SetActive(true); });

        ToggleQuestInfo(true);
    }
    public void ToggleQuestInfo (bool toggle)
    {
        QuestInfoUI.SetActive(toggle);
    }
}
