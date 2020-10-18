using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : Inventory
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject inventoryUIGrid;

    [SerializeField] private GameObject[] inventoryObjects;
    [SerializeField] private GameObject[] inventorySlots;

    [SerializeField] private GameObject[] equipmentObjects;
    [SerializeField] private GameObject[] equipmentSlots;

    public override void Awake()
    {
        base.Awake();
        entity = FindObjectOfType<Player>();

        inventoryObjects = new GameObject[inventorySize];
        equipmentObjects = new GameObject[equipmentSize];

        for (int i = 0; i < inventorySize; i++)
        {
            // instantiate a new inventory slot
            inventorySlots[i].GetComponent<InventorySlot>().slotIndex = i;
        }
    }
    public override void Update()
    {
        base.Update();

        if (!inventoryUI.activeInHierarchy) { return; }

        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryObjects[i] == null && (inventory[i] != null && inventory[i].item != null))
            {
                inventoryObjects[i] = Instantiate(Resources.Load<GameObject>("UI/Inventory Item"), inventoryUIGrid.transform.parent);
                inventoryObjects[i].GetComponent<RectTransform>().anchoredPosition = inventorySlots[i].GetComponent<RectTransform>().anchoredPosition;
            }
            if (inventory[i] != null && inventory[i].item != null)
            {
                inventoryObjects[i].GetComponent<RectTransform>().anchoredPosition = inventorySlots[i].GetComponent<RectTransform>().anchoredPosition;
                inventoryObjects[i].GetComponent<Image>().sprite = inventory[i].item.sprite;
                inventoryObjects[i].transform.GetChild(0).GetComponent<Text>().text = inventory[i].quantity.ToString();
                inventoryObjects[i].GetComponent<DraggableInventoryItem>().canvas = canvas;
                inventoryObjects[i].GetComponent<DraggableInventoryItem>().SetInventoryItem(inventory[i]);
                inventoryObjects[i].GetComponent<DraggableInventoryItem>().SetInventory(this);
            }
        }
        for (int i = 0; i < equipmentSize; i++)
        {
            if (equipmentObjects[i] == null && equipment[i] != null)
            {
                equipmentObjects[i] = Instantiate(Resources.Load<GameObject>("UI/Equipment Item"), inventoryUIGrid.transform.parent);
                equipmentObjects[i].GetComponent<RectTransform>().anchoredPosition = equipmentSlots[i].GetComponent<RectTransform>().anchoredPosition;
            }
            if (equipment[i] != null)
            {
                equipmentObjects[i].GetComponent<RectTransform>().anchoredPosition = equipmentSlots[i].GetComponent<RectTransform>().anchoredPosition;
                equipmentObjects[i].GetComponent<Image>().sprite = equipment[i].sprite;
                equipmentObjects[i].transform.GetChild(0).GetComponent<Text>().enabled = false;
                equipmentObjects[i].GetComponent<DraggableEquipment>().canvas = canvas;
                equipmentObjects[i].GetComponent<DraggableEquipment>().SetEquipment(equipment[i]);
                equipmentObjects[i].GetComponent<DraggableEquipment>().SetInventory(this);
            }
        }
    }

    public void ToggleInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeInHierarchy);
    }
}
