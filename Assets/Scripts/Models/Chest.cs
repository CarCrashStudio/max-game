using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ChestType { SMALL, LARGE }
public class Chest : Interactable
{
    public ChestType type;
    public InventoryItem[] inventory;
    public GameObject player;

    public GameObject chestUI;
    public override void Interact(GameObject interacter)
    {
        if (!active)
        {
            Reload();
            active = true;
            chestUI.SetActive(true);
            interacter.GetComponent<MaxInventory>().UI.SetActive(true);
        }
        else
        {
            active = false;
            chestUI.SetActive(false);
            interacter.GetComponent<MaxInventory>().UI.SetActive(true);
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
                if (inventory[i] != null)
                {

                    cell.transform.GetChild(1).GetComponent<Text>().text = inventory[i].quantity.ToString();
                    cell.transform.GetChild(0).GetComponent<Image>().sprite = inventory[i].item.GetComponent<SpriteRenderer>().sprite;
                    var ii = inventory[i];
                    cell.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                    cell.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { Loot(ii); });
                    cell.transform.GetChild(1).gameObject.SetActive(true);
                    cell.transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    cell.transform.GetChild(1).gameObject.SetActive(false);
                    cell.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            catch (System.Exception)
            {

            }
        }

    }

    public void Loot (InventoryItem ii)
    {
        if (player.GetComponent<MaxAttributes>().currentlyInInteractable)
        {
            player.GetComponent<MaxInventory>().PickUp(ii);
            for (int i = 0; i < inventory.Length -1; i++)
            {
                if (inventory[i] == ii)
                {
                    inventory[i] = null;
                    break;
                }
            }
            Reload();
            player.GetComponent<MaxInventory>().ReloadUI();
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
