using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaxInventory : MonoBehaviour
{
    private MaxAttributes attributes { get { return GetComponent<MaxAttributes>(); } }

    public int inventorySize = 12;
    public InventoryItem[] inventory;
    private Equipment[] equipment;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new InventoryItem[inventorySize];
    }

    // Update is called once per frame
    void Update()
    {
        if (attributes.currentlyInInteractable)
        {
            var n = attributes.nearestInteractable;
            if (n is Chest)
            {
                var chest = (Chest)n;
                if (Input.GetMouseButtonDown(0))
                {
                    PickUp(chest.inventory[0]);
                    chest.inventory[0] = null;
                    chest.Reload();
                }
            }
        }
    }

    public void PickUp (InventoryItem ii)
    {
        if (ii.item == null)
            throw new System.Exception("PickUp cannot use a null Item");

        bool found = false;

        // loop through the inventory array
        for (int i = 0; i < inventorySize; i++)
        {
            // attempt to find similar item, and increase qty by ii qty
            if (inventory[i] != null && inventory[i].item != null && inventory[i].item == ii.item)
            {
                inventory[i].quantity += ii.quantity;
                found = true;
            }

            if (found)
                break;
        }
        // otherwise,
        // add new item at end of array
        if (!found)
        {
            var newI = inventory.Length - 1;
            for (int i = inventory.Length - 1; i >= 0; i--)
            {
                if (inventory[i].item != null)
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
}
