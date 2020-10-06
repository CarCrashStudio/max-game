using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableEquipment : Draggable
{
    [SerializeField] Equipment equipment;
    [SerializeField] Inventory inventory;

    public Signal disableRaycastSignal;
    public Signal enableRaycastSignal;

    public Equipment GetEquipment() { return equipment; }
    public void SetEquipment(Equipment equipment) { this.equipment = equipment; }
    public Inventory GetInventory() { return inventory; }
    public void SetInventory(Inventory inventory) { this.inventory = inventory; }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        // TODO: figure out this dumb shit and it no work right
        disableRaycastSignal.Raise();

        inventory.RemoveEquipment(equipment);

        base.OnBeginDrag(eventData);
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        enableRaycastSignal.Raise();
        base.OnEndDrag(eventData);
    }

    public void ToggleRaycast(bool toggle)
    {
        //Debug.Log($"ToggleRaycast: {toggle}");
        canvasGroup.blocksRaycasts = toggle;
    }
}