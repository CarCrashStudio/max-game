using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableInventoryItem : Draggable
{
    [SerializeField] private TooltipPopup tooltipPopup;
    [SerializeField] InventoryItem inventoryItem;
    [SerializeField] Inventory inventory;

    public Signal disableRaycastSignal;
    public Signal enableRaycastSignal;

    public TooltipPopup TooltipPopup
    {
        get { return tooltipPopup; }
        set { tooltipPopup = value; }
    }

    public InventoryItem GetInventoryItem () { return inventoryItem; }
    public void SetInventoryItem (InventoryItem inventoryItem) { this.inventoryItem = inventoryItem; }
    public Inventory GetInventory() { return inventory; }
    public void SetInventory(Inventory inventory) { this.inventory = inventory; }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        // TODO: figure out this dumb shit and it no work right
        disableRaycastSignal.Raise();

        inventory.RemoveInventoryItem(inventoryItem.item, inventoryItem.quantity, inventoryItem.previousSlotIndex);

        base.OnBeginDrag(eventData);
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        enableRaycastSignal.Raise();
        base.OnEndDrag(eventData);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        tooltipPopup.DisplayInfo(inventoryItem.item);

        base.OnPointerEnter(eventData);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        tooltipPopup.HideInfo();

        base.OnPointerExit(eventData);
    }

    public void ToggleRaycast (bool toggle)
    {
        //Debug.Log($"ToggleRaycast: {toggle}");
        canvasGroup.blocksRaycasts = toggle;
    }
}