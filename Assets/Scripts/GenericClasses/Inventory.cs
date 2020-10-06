using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] protected Signal equipmentSignal;

    [SerializeField] protected int inventorySize = 6;
    [SerializeField] protected int equipmentSize = 7;
    [SerializeField] protected int maxItemStack = 16;

    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject inventoryUIGrid;
    [SerializeField] private Canvas canvas;

    [SerializeField] public InventoryItem[] inventory;
    [SerializeField] protected GameObject[] inventoryObjects;
    [SerializeField] protected GameObject[] inventorySlots;

    public Equipment[] equipment;
    [SerializeField] protected GameObject[] equipmentObjects;
    [SerializeField] protected GameObject[] equipmentSlots;
    private void Awake()
    {
        inventory = new InventoryItem[inventorySize];
        equipment = new Equipment[equipmentSize];

        inventoryObjects = new GameObject[inventorySize];
        equipmentObjects = new GameObject[equipmentSize];

        for (int i = 0; i < inventorySize; i++)
        {
            // instantiate a new inventory slot
            inventorySlots[i].GetComponent<InventorySlot>().slotIndex = i;
        }

        AddInventoryItem(Resources.Load<Equipment>("Items/Sword"), 1, 3);
        AddInventoryItem(Resources.Load<Equipment>("Items/Sword"), 1, 4);
    }
    private void Start()
    {
        
    }

    public void Update()
    {
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
            if(equipmentObjects[i] == null && equipment[i] != null)
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

    public void ToggleInventory ()
    {
        inventoryUI.SetActive(!inventoryUI.activeInHierarchy);
    }

    public void AddEquipment (Equipment equipment)
    {
        this.equipment[(int)equipment.slot] = equipment;

        if (equipmentSignal != null) { equipmentSignal.Raise(); }
    }
    public void RemoveEquipment (Equipment equipment)
    {
        this.equipment[(int)equipment.slot] = null;
        if (equipmentSignal != null) { equipmentSignal.Raise(); }
    }

    public void AddInventoryItem(Item item, int quantity = 1, int slotIndex = 0)
    {
        if (inventory[slotIndex] != null)
        {
            Debug.Log($"II: {inventory[slotIndex]}, II.I: {inventory[slotIndex].item}");
            if (inventory[slotIndex].item != null)
            {
                inventory[slotIndex].quantity += quantity;
            }
        }
        else if (inventory[slotIndex] == null || inventory[slotIndex].item == null)
        {
            inventory[slotIndex] = new InventoryItem(item, quantity, slotIndex, canvas);
        }
        
    }
    public void RemoveInventoryItem (Item item, int quantity, int slotIndex)
    {
        if (inventory[slotIndex] != null)
        {
            inventory[slotIndex] = null;
        }
    }
}
