using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquipment : MonoBehaviour
{
    public bool charOpen = false;
    public Equipment[] equipment;
    private Equipment selectedEquipment = null;
    public GameObject characterUI;

    private PlayerMovement attributes { get { return GetComponent<PlayerMovement>(); } }

    private void Start()
    {
        equipment = new Equipment[6];
        ReloadCharacterEquUI();
    }

    private void Update()
    {
        if (Input.GetButtonDown("character"))
        {
            characterUI.SetActive(!charOpen);
            charOpen = !charOpen;
        }

        attributes.currentlyInInteractable = charOpen;
        attributes.currentState = (attributes.currentlyInInteractable) ? EntityState.INTERACTING : EntityState.IDLE;
    }

    public void SelectEquipment(Equipment e)
    {
        selectedEquipment = e;
        ReloadCharacterEquUI();
    }
    public void Equip(Equipment e)
    {
        //switch (e.type)
        //{
        //case EquipmentType.ARMOR:
        //var slot = characterUI.transform.GetChild(1).GetChild((int)e.slot);
        //slot.GetChild(0).GetComponent<Image>().sprite = e.GetComponent<SpriteRenderer>().sprite;
        equipment[(int)e.slot] = e;
        //break;
        //}
    }
    public void Unequip(Equipment e)
    {
        var slot = characterUI.transform.GetChild(1).GetChild((int)e.slot);
        slot.GetChild(0).GetComponent<Image>().sprite = null;
        equipment[(int)e.slot] = null;
    }
    public void ReloadCharacterEquUI()
    {
        // create and populate UI
        var grid = characterUI.transform.GetChild(1);

        //for (int i = grid.transform.childCount - 1; i >= inventory.Length; i--)
        //grid.transform.GetChild(i).gameObject.SetActive(false);

        characterUI.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
        characterUI.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => { UnequipItemButton(); });
        characterUI.transform.GetChild(3).gameObject.SetActive(selectedEquipment != null);

        for (int i = 0; i < equipment.Length; i++)
        {
            try
            {
                var cell = grid.transform.GetChild(i).gameObject;
                if (equipment[i] != null)
                {
                    //cell.GetComponent<Image>().color = (inventory[i] == selectedItem) ? SELECTED_CELL_COLOR : NORMAL_CELL_COLOR;
                    //Debug.Log(cell.GetComponent<Image>().color);

                    cell.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                    var e = equipment[i];
                    //Debug.Log(e);
                    cell.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { SelectEquipment(e); });
                    cell.transform.GetChild(0).GetComponent<Image>().sprite = e.GetComponent<SpriteRenderer>().sprite;
                    cell.transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    cell.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            catch (System.Exception)
            {

            }
        }

    }
    private void UnequipItemButton()
    {
        Unequip(selectedEquipment);
        GetComponent<PlayerInventory>().PickUp(new InventoryItem(selectedEquipment, 1));
        selectedEquipment = null;
        ReloadCharacterEquUI();
        GetComponent<PlayerInventory>().ReloadInventoryUI();
    }
}
