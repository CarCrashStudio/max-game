using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class InventorySlot : DropArea
{
    public int slotIndex;
    public Inventory inventory;

    [SerializeField] private Signal enableRaycastSignal;
    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if ((ItemAlreadyExists() && ItemIsSimilar(eventData.pointerDrag.GetComponent<DraggableInventoryItem>().GetInventoryItem())) || !ItemAlreadyExists())
            {
                base.OnDrop(eventData);

                var draggable = eventData.pointerDrag.GetComponent<Draggable>();
                if (draggable is DraggableInventoryItem)
                {
                    AddInventory(eventData.pointerDrag.GetComponent<DraggableInventoryItem>().GetInventoryItem());
                }
                else
                {
                    AddInventory(new InventoryItem(eventData.pointerDrag.GetComponent<DraggableEquipment>().GetEquipment(), 1));
                }

                Destroy(eventData.pointerDrag);
            }
        }

        enableRaycastSignal.Raise();
    }

    // check that item doesnt already exist in this slot
    private bool ItemAlreadyExists ()
    {
        return inventory.inventory[slotIndex] != null && inventory.inventory[slotIndex].item != null;
    }
    private bool ItemIsSimilar(InventoryItem inventoryItem)
    {
        return inventory.inventory[slotIndex] != null && inventory.inventory[slotIndex].item == inventoryItem.item;
    }

    // increase the quantity of the item in the slot
    private void AddInventory (InventoryItem inventoryItem)
    {
        inventory.AddInventoryItem(inventoryItem.item, inventoryItem.quantity, slotIndex);
    }
}