using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableEquipment : Draggable
{
    [SerializeField] private TooltipPopup tooltipPopup;

    [SerializeField] Equipment equipment;
    [SerializeField] Inventory inventory;

    public Signal disableRaycastSignal;
    public Signal enableRaycastSignal;

    public TooltipPopup TooltipPopup
    {
        get { return tooltipPopup; }
        set { tooltipPopup = value; }
    }

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

    public override void OnPointerEnter(PointerEventData eventData)
    {
        tooltipPopup.DisplayInfo(equipment);

        base.OnPointerEnter(eventData);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        tooltipPopup.HideInfo();

        base.OnPointerExit(eventData);
    }

    public void ToggleRaycast(bool toggle)
    {
        //Debug.Log($"ToggleRaycast: {toggle}");
        canvasGroup.blocksRaycasts = toggle;
    }
}