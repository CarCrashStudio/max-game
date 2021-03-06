﻿using UnityEngine;
using UnityEngine.UI;

public class QuickItemManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject quickItems;

    void Awake ()
    {
        GameEvents.onPause += GameEvents_onPause;
        GameEvents.onResume += GameEvents_onResume;
    }

    private void GameEvents_onResume()
    {
        quickItems.SetActive(true);
    }

    private void GameEvents_onPause()
    {
        quickItems.SetActive(false);
    }

    public void Update()
    {
        if (inventory.equipment[(int)EquipmentSlotType.MAINHAND] != null)
        {
            quickItems.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            quickItems.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            quickItems.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = inventory.equipment[(int)EquipmentSlotType.MAINHAND].sprite;
        }
        else
        {
            quickItems.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            quickItems.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            quickItems.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = null;
        }

        if (inventory.equipment[(int)EquipmentSlotType.OFFHAND] != null)
        {
            quickItems.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            quickItems.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
            quickItems.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = inventory.equipment[(int)EquipmentSlotType.OFFHAND].sprite;
        }
        else
        {
            quickItems.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            quickItems.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
            quickItems.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = null;
        }

        if (inventory.equipment[(int)EquipmentSlotType.POTION] != null)
        {
            quickItems.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
            quickItems.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
            quickItems.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = inventory.equipment[(int)EquipmentSlotType.POTION].sprite;
        }
        else
        {
            quickItems.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            quickItems.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
            quickItems.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = null;
        }
    }
}
