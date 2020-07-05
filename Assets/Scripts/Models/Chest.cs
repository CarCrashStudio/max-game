using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ChestType { SMALL, LARGE }
public class Chest : Interactable
{
    public ChestType type;
    public InventoryItem[] inventory;

    public GameObject chestUI;

    public override void Interact()
    {
        if (!active)
        {
            var grid = chestUI.transform.GetChild(0);

            for (int i = grid.transform.childCount - 1; i >= inventory.Length; i--)
                grid.transform.GetChild(i).gameObject.SetActive(false);

            for (int i = 0; i < inventory.Length; i++)
            {
                try
                {
                    var cell = grid.transform.GetChild(i).gameObject;
                    cell.transform.GetChild(1).GetComponent<Text>().text = (inventory[i] != null) ? inventory[i].quantity.ToString() : "";
                    cell.transform.GetChild(0).GetComponent<Image>().sprite = inventory[i].item.GetComponent<SpriteRenderer>().sprite;

                    cell.transform.GetChild(0).gameObject.SetActive(inventory[i] != null);
                }
                catch { }
            }

            active = true;
            chestUI.SetActive(true);
        }
        else
        {
            active = false;
            chestUI.SetActive(false);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
