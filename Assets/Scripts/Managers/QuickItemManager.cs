using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickItemManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject quickItems;
    public void Update()
    {
        if (inventory.equipment[(int)EquipmentSlotType.MAINHAND] != null)
        {
            quickItems.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            quickItems.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            quickItems.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = inventory.equipment[(int)EquipmentSlotType.MAINHAND].sprite;
        }
        if (inventory.equipment[(int)EquipmentSlotType.OFFHAND] != null)
        {
            quickItems.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            quickItems.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
            quickItems.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = inventory.equipment[(int)EquipmentSlotType.OFFHAND].sprite;
        }
        if (inventory.equipment[(int)EquipmentSlotType.POTION] != null)
        {
            quickItems.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
            quickItems.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
            quickItems.transform.GetChild(2).GetChild(1).GetComponent<Image>().sprite = inventory.equipment[(int)EquipmentSlotType.POTION].sprite;
        }
    }
}
