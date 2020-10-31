using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    [SerializeField] public Item item;
    [SerializeField] public int quantity;

    public int previousSlotIndex;

    [JsonConstructor]
    public InventoryItem(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
    public InventoryItem(Item item, int quantity, int previousSlotIndex)
    {
        this.item = item;
        this.quantity = quantity;
        this.previousSlotIndex = previousSlotIndex;
    }
    public InventoryItem(Item item, int quantity, int previousSlotIndex, Canvas canvas)
    {
        this.item = item;
        this.quantity = quantity;
        this.previousSlotIndex = previousSlotIndex;
    }

    public static InventoryItem Empty ()
    {
        return new InventoryItem(null, 0);
    }
}
