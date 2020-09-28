﻿// Attribute Script
// Author: Trey Hall
// Description:
// Handles getting and modifying attributes related to the player such as health, mana, and other rpg attributes.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public DungeonManager manager;
    public bool currentlyInInteractable = false;
    public float gold = 5f;
    public int Level => level;

    #region INVENTORY
    public bool invOpen = false;
    public int inventorySize = 12;
    public GameObject inventoryUI;

    [SerializeField] private InventoryItem[] inventory;
    private InventoryItem selectedItem = null;

    public void SelectItem(InventoryItem ii)
    {
        selectedItem = ii;
        ReloadInventoryUI();
    }
    public void PickUp(InventoryItem ii)
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
    public void Drop(InventoryItem ii)
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

        inventoryUI.transform.GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
        inventoryUI.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(() => { UseItemButton(); });
        inventoryUI.transform.GetChild(4).gameObject.SetActive(selectedItem != null && selectedItem.item is Potion);

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
    private void UseItemButton()
    {
        ((Potion)selectedItem.item).Use();
        Drop(new InventoryItem(selectedItem.item, 1));
        selectedItem = null;
        ReloadInventoryUI();
        ReloadCharacterEquUI();
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
            Instantiate(selectedItem.item, gameObject.transform.position, Quaternion.identity, manager.roomManager.currentRoom.transform);

        selectedItem = null;

    }
    #endregion

    private Interactable nearestInteractable
    {
        get
        {
            List<Interactable> temp = new List<Interactable>();
            temp.AddRange(FindObjectsOfType<Interactable>());
            temp = temp.Where(t => t.entered).ToList();

            if (temp.Count > 0)
                return temp[0];
            else return null;
        }
    }

    public override void Start ()
    {
        base.Start();
    }
    public override void Update()
    {
        #region MOVEMENT
        Vector2 velocity = Vector2.zero;
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");

        moveEntity(velocity);
        #endregion

        #region INTERACTION
        if (Input.GetButtonDown("interact") && (currentState == EntityState.IDLE || currentState == EntityState.WALK || currentState == EntityState.INTERACTING))
        {
            var n = nearestInteractable;
            if (n != null)
            {
                currentlyInInteractable = (currentState != EntityState.INTERACTING);
                n.Interact(gameObject);
                currentState = (!currentlyInInteractable) ? EntityState.IDLE : EntityState.INTERACTING;
            }
        }
        #endregion
        base.Update();
    }

    public void LevelUp ()
    {
        level++;
    }
    public void ToggleCharacterSheet()
    {
        characterUI.SetActive(!charOpen);
        charOpen = !charOpen;

        currentlyInInteractable = charOpen;
        currentState = (currentlyInInteractable) ? EntityState.INTERACTING : EntityState.IDLE;
    }
    public void ToggleInventory()
    {
        if (!invOpen)
            ReloadInventoryUI();

        inventoryUI.SetActive(!invOpen);
        invOpen = !invOpen;

        currentlyInInteractable = invOpen;
        currentState = (currentlyInInteractable) ? EntityState.INTERACTING : EntityState.IDLE;
    }

    public override string ToString()
    {
        return base.ToString();
    }

    private Equipment selectedEquipment = null;
    public void SelectEquipment(Equipment e)
    {
        selectedEquipment = e;
        ReloadCharacterEquUI();
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