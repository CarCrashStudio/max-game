﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MaxInventory : MonoBehaviour
{

    //C58A6D
    //C0C56D
    Color NORMAL_CELL_COLOR = new Color(197, 138, 109, 255);
    Color SELECTED_CELL_COLOR = new Color(192, 197, 109, 255);
    public bool invOpen = false;
    public bool charOpen = false;
    private MaxAttributes attributes { get { return GetComponent<MaxAttributes>(); } }

    public int inventorySize = 12;
    public GameObject inventoryUI;
    public GameObject characterUI;

    private InventoryItem[] inventory;
    public Equipment[] equipment;

    private InventoryItem selectedItem = null;
    private Equipment selectedEquipment = null;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new InventoryItem[inventorySize];
        equipment = new Equipment[6];
        ReloadInventoryUI();
        ReloadCharacterEquUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("inventory"))
        {
            inventoryUI.SetActive(!invOpen);
            invOpen = !invOpen;
        }
        if (Input.GetButtonDown("character"))
        {
            characterUI.SetActive(!charOpen);
            charOpen = !charOpen;
        }

        attributes.currentlyInInteractable = (charOpen || invOpen);
        attributes.currentState = (attributes.currentlyInInteractable) ? EntityState.INTERACTING : EntityState.IDLE;
    }

    public void SelectItem(InventoryItem ii)
    {
        selectedItem = ii;
        ReloadInventoryUI();
    }
    public void PickUp (InventoryItem ii)
    {
        if (ii.item == null)
            throw new System.Exception("PickUp cannot use a null Item");

        bool found = false;

        // loop through the inventory array
        for (int i = 0; i < inventorySize && !found; i++)
        {
            // attempt to find similar item, and increase qty by ii qty
            if (inventory[i] != null && inventory[i].item == ii.item)
            {
                found = true;
                inventory[i].quantity += ii.quantity;
            }
        }
        // otherwise,
        // add new item at end of array
        if (!found)
        {
            var newI = inventory.Length - 1;
            for (int i = inventory.Length - 1; i >= 0; i--)
            {
                if (inventory[i] != null && inventory[i].item != null)
                {
                    newI = i + 1;
                    break;
                }
            }

            if (newI == inventory.Length - 1)
                newI = 0;

            inventory[newI] = ii;
        }
    }
    public void Drop (InventoryItem ii)
    {
        if (ii.item == null)
            throw new System.Exception("Drop cannot use a null Item");

        // loop through the inventory array
        // attempt to find similar item, and decrease qty by ii qty
        for (int i = 0; i < inventorySize; i++)
            if (inventory[i].item != null && inventory[i].item == ii.item)
            {
                inventory[i].quantity -= ii.quantity;

                if (inventory[i].quantity <= 0)
                    inventory[i] = null;

                break;
            }


    }
    public void ReloadInventoryUI()
    {
        // create and populate UI
        var grid = inventoryUI.transform.GetChild(0);

        for (int i = grid.transform.childCount - 1; i >= inventory.Length; i--)
            grid.transform.GetChild(i).gameObject.SetActive(false);

        inventoryUI.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        inventoryUI.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => { DropItemButton(); });
        inventoryUI.transform.GetChild(2).gameObject.SetActive(selectedItem != null);

        inventoryUI.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
        inventoryUI.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => { EquipItemButton(); });
        inventoryUI.transform.GetChild(3).gameObject.SetActive(selectedItem != null && selectedItem.item is Equipment);

        for (int i = 0; i < inventory.Length - 1; i++)
        {
            try
            {
                var cell = grid.transform.GetChild(i).gameObject;
                if (inventory[i] != null)
                {
                    //cell.GetComponent<Image>().color = (inventory[i] == selectedItem) ? SELECTED_CELL_COLOR : NORMAL_CELL_COLOR;
                    //Debug.Log(cell.GetComponent<Image>().color);

                    cell.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                    var ii = inventory[i];
                    cell.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { SelectItem(ii); });

                    cell.transform.GetChild(0).GetComponent<Image>().sprite = inventory[i].item.GetComponent<SpriteRenderer>().sprite;
                    cell.transform.GetChild(0).gameObject.SetActive(true);
                    cell.transform.GetChild(1).GetComponent<Text>().text = inventory[i].quantity.ToString();
                    cell.transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    cell.transform.GetChild(0).gameObject.SetActive(false);
                    cell.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
            catch (System.Exception)
            {

            }
        }

    }
    private void EquipItemButton()
    {
        Equip((Equipment)selectedItem.item);
        Drop(new InventoryItem(selectedItem.item, 1));
        selectedItem = null;
        ReloadInventoryUI();
        ReloadCharacterEquUI();
    }
    private void DropItemButton()
    {
        Drop(selectedItem);
        ReloadInventoryUI();

        // drop gameobject of item in world at players coordinates
        for (int i = 0; i < selectedItem.quantity; i++)
            Instantiate(selectedItem.item, gameObject.transform.position, Quaternion.identity, attributes.manager.roomManager.currentRoom.transform);

        selectedItem = null;

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
        PickUp(new InventoryItem(selectedEquipment, 1));
        selectedEquipment = null;
        ReloadCharacterEquUI();
        ReloadInventoryUI();
    }
}
