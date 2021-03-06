﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] protected int inventorySize = 6;
    [SerializeField] protected int equipmentSize = 7;
    [SerializeField] protected int maxItemStack = 16;

    [SerializeField] protected Canvas canvas;
    public InventoryItem[] inventory;
    public Equipment[] equipment;

    protected Entity entity;

    public virtual void Awake()
    {
        inventory = new InventoryItem[inventorySize];
        equipment = new Equipment[equipmentSize];


        AddInventoryItem(GameManager.Items.Db[0], 1, 0);
        AddInventoryItem(GameManager.Items.Db[1], 1, 1);
        AddInventoryItem(GameManager.Items.Db[2], 1, 2);
    }
    private void Start()
    {
        
    }

    public virtual void Update()
    {
        UpdateModifiers();
    }

    private void UpdateModifiers ()
    {
        // apply any modifier buffs from equipment
        var totalAdditionalModifiers = new Modifier();
        for (int i = 0; i < equipmentSize; i++)
        {
            //if (i == (int)EquipmentSlotType.MAINHAND || i == (int)EquipmentSlotType.OFFHAND || i == (int)EquipmentSlotType.POTION) { continue; }

            // do not include weapons in the buffs. Weapon modifiers will get added on during attack
            if (equipment[i] != null && equipment[i].Type != EquipmentType.WEAPON)
            {
                // get the modifier of the equipment
                totalAdditionalModifiers += equipment[i].Modifier;
            }
        }

        //Debug.Log(totalAdditionalModifiers.ToString());
        entity.attributes.equipmentModifiers = totalAdditionalModifiers;

        // Update the attributes of this entity
        entity.attributes.Update();
    }

    public void AddEquipment (Equipment equipment)
    {
        this.equipment[(int)equipment.Slot] = equipment;
        GameEvents.ChangesMade();
    }
    public void RemoveEquipment (Equipment equipment)
    {
        this.equipment[(int)equipment.Slot] = null;
        GameEvents.ChangesMade();
    }

    public void AddInventoryItem(Item item, int quantity = 1, int slotIndex = 0)
    {
        if (inventory[slotIndex] != null)
        {
            if (inventory[slotIndex].item != null && inventory[slotIndex].item == item)
            {
                inventory[slotIndex].quantity += quantity;
            }
        }
        else if (inventory[slotIndex] == null || inventory[slotIndex].item == null)
        {
            inventory[slotIndex] = new InventoryItem(item, quantity, slotIndex, canvas);
        }
        GameEvents.ChangesMade();
    }
    public void AddInventoryItem(InventoryItem inventoryItem, int slotIndex = 0)
    {
        if (inventory[slotIndex] != null)
        {
            if (inventory[slotIndex].item != null && inventory[slotIndex].item == inventoryItem.item)
            {
                inventory[slotIndex].quantity += inventoryItem.quantity;
            }
        }
        else if (inventory[slotIndex] == null || inventory[slotIndex].item == null)
        {
            inventory[slotIndex] = inventoryItem;
        }
        GameEvents.ChangesMade();
    }
    public void AddInventoryItem(InventoryItem inventoryItem)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventory[i] != null)
            {
                if (inventory[i].item != null && inventory[i].item == inventoryItem.item)
                {
                    inventory[i].quantity += inventoryItem.quantity;
                    break;
                }
            }
            else if (inventory[i] == null || inventory[i].item == null)
            {
                inventory[i] = inventoryItem;
                break;
            } 
        }
        GameEvents.ChangesMade();
    }

    public void RemoveInventoryItem (Item item, int quantity, int slotIndex)
    {
        if (inventory[slotIndex] != null)
        {
            inventory[slotIndex] = null;
        }
        GameEvents.ChangesMade();
    }
}
