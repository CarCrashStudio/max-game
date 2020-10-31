using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : Inventory
{
    bool gameIsPaused = false;
    [SerializeField] private TooltipPopup tooltipPopup;

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
        GameEvents.onEnemyKilled += onEnemyKilled;
        GameEvents.onPause += onPause;
        GameEvents.onResume += onResume;

        inventoryObjects = new GameObject[inventorySize];
        equipmentObjects = new GameObject[equipmentSize];

        for (int i = 0; i < inventorySize; i++)
        {
            // instantiate a new inventory slot
            inventorySlots[i].GetComponent<InventorySlot>().slotIndex = i;
        }
    }

    private void onEnemyKilled(Enemy enemy)
    {
        // give a random loot item from the enemy's loot table
        AddInventoryItem(enemy.lootTable.GetLoot());
    }
    private void onPause ()
    {
        gameIsPaused = true;
        inventoryUI.SetActive(false);
    }
    private void onResume()
    {
        gameIsPaused = false;
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
                inventoryObjects[i].GetComponent<DraggableInventoryItem>().TooltipPopup = tooltipPopup;
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
                equipmentObjects[i].GetComponent<DraggableEquipment>().TooltipPopup = tooltipPopup;
            }
        }
    }
    public void ToggleInventory()
    {
        if (gameIsPaused) return;
        inventoryUI.SetActive(!inventoryUI.activeInHierarchy);
    }
}
