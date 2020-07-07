using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MaxInventory : MonoBehaviour
{
    //C58A6D
    //C0C56D
    Color NORMAL_CELL_COLOR = new Color(197, 138, 109, 255);
    Color SELECTED_CELL_COLOR = new Color(192, 197, 109, 255);

    private MaxAttributes attributes { get { return GetComponent<MaxAttributes>(); } }

    public int inventorySize = 12;
    public GameObject UI;

    public InventoryItem[] inventory;
    private Equipment[] equipment;

    private InventoryItem selectedItem = null;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new InventoryItem[inventorySize];
        ReloadUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select (InventoryItem ii)
    {
        selectedItem = ii;
        ReloadUI();
    }

    public void PickUp (InventoryItem ii)
    {
        if (ii.item == null)
            throw new System.Exception("PickUp cannot use a null Item");

        bool found = false;

        // loop through the inventory array
        for (int i = 0; i < inventorySize && !found; i++)
        {
            // attempt to find similar item, and increase qty by ii qty
            if (inventory[i] != null && inventory[i].item == ii.item)
            {
                found = true;
                inventory[i].quantity += ii.quantity;
            }
        }
        // otherwise,
        // add new item at end of array
        if (!found)
        {
            var newI = inventory.Length - 1;
            for (int i = inventory.Length - 1; i >= 0; i--)
            {
                if (inventory[i] != null && inventory[i].item != null)
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
        if (ii.item == null)
            throw new System.Exception("Drop cannot use a null Item");

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

    public void ReloadUI()
    {
        // create and populate UI
        var grid = UI.transform.GetChild(0);

        for (int i = grid.transform.childCount - 1; i >= inventory.Length; i--)
            grid.transform.GetChild(i).gameObject.SetActive(false);

        UI.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        UI.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => { Drop(selectedItem); selectedItem = null; ReloadUI();  });
        UI.transform.GetChild(2).gameObject.SetActive(selectedItem != null);

        for (int i = 0; i < inventory.Length - 1; i++)
        {
            try
            {
                var cell = grid.transform.GetChild(i).gameObject;
                if (inventory[i] != null)
                {
                    //cell.GetComponent<Image>().color = (inventory[i] == selectedItem) ? SELECTED_CELL_COLOR : NORMAL_CELL_COLOR;
                    //Debug.Log(cell.GetComponent<Image>().color);

                    cell.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                    var ii = inventory[i];
                    cell.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { Select(ii); });

                    cell.transform.GetChild(0).GetComponent<Image>().sprite = inventory[i].item.GetComponent<SpriteRenderer>().sprite;
                    cell.transform.GetChild(0).gameObject.SetActive(true);
                    cell.transform.GetChild(1).GetComponent<Text>().text = inventory[i].quantity.ToString();
                    cell.transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    cell.transform.GetChild(0).gameObject.SetActive(false);
                    cell.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
            catch (System.Exception)
            {

            }
        }

    }
}
