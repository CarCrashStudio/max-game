using UnityEngine;
using UnityEngine.EventSystems;
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
            Reload();
            active = true;
            chestUI.SetActive(true);
        }
        else
        {
            active = false;
            chestUI.SetActive(false);
        }
    }

    public void Reload ()
    {
        // create and populate UI
        var grid = chestUI.transform.GetChild(0);

        for (int i = grid.transform.childCount - 1; i >= inventory.Length; i--)
            grid.transform.GetChild(i).gameObject.SetActive(false);

        for (int i = 0; i < inventory.Length - 1; i++)
        {
            try
            {
                var cell = grid.transform.GetChild(i).gameObject;
                cell.transform.GetChild(1).GetComponent<Text>().text = (inventory[i] != null) ? inventory[i].quantity.ToString() : "";
                if (inventory[i] != null)
                {
                    cell.transform.GetChild(0).GetComponent<Image>().sprite = inventory[i].item.GetComponent<SpriteRenderer>().sprite;
                    cell.transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    cell.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }

    public void OnPointerClick (PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            Debug.Log(eventData.ToString());
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
