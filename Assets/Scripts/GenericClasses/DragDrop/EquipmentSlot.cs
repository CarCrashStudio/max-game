using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : DropArea
{
    public EquipmentSlotType slotIndex;
    public Inventory inventory;

    // enable raycast signal
    [SerializeField] private Signal enableRaycastSignal;

    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (!SlotIsFull() && SlotIsCorrect(eventData.pointerDrag.GetComponent<DraggableInventoryItem>().GetInventoryItem()))
            {
                base.OnDrop(eventData);
                AddEquipment(eventData.pointerDrag.GetComponent<DraggableInventoryItem>().GetInventoryItem());
                Destroy(eventData.pointerDrag);
            }

            enableRaycastSignal.Raise();
        }
    }

    // check if equipment is already in that slot
    private bool SlotIsFull ()
    {
        return inventory.equipment[(int)slotIndex] != null;
    }

    // check if equipment is going to appropriate slot
    private bool SlotIsCorrect (InventoryItem inventoryItem)
    {
        return ((Equipment)inventoryItem.item).Slot == slotIndex;
    }
    // Add Equipment

    private void AddEquipment(InventoryItem inventoryItem)
    {
        inventory.AddEquipment((Equipment)inventoryItem.item);
    }
}
