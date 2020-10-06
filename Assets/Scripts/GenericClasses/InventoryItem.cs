using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//[RequireComponent(typeof(Image), typeof(RectTransform))]
[Serializable]
public class InventoryItem //: Draggable
{
    [SerializeField] public Item item;
    [SerializeField] public int quantity;

    public int previousSlotIndex;
    //public Inventory inventory;

    //protected Image image;

    public InventoryItem(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
    public InventoryItem(Item item, int quantity, int previousSlotIndex)
    {
        this.item = item;
        this.quantity = quantity;
        this.previousSlotIndex = previousSlotIndex;
    }
    public InventoryItem(Item item, int quantity, int previousSlotIndex, Canvas canvas)
    {
        this.item = item;
        this.quantity = quantity;
        this.previousSlotIndex = previousSlotIndex;
        //this.canvas = canvas;
    }

    public static InventoryItem Empty ()
    {
        return new InventoryItem(null, 0);
    }

    public void SetCanvas (Canvas canvas)
    {
        //this.canvas = canvas;
    }

    private void Awake()
    {
        //image = GetComponent<Image>();

        //if (TryGetComponent<RectTransform>(out var rt)) { rectTransform = rt; }
        //if (TryGetComponent<CanvasGroup>(out var cg)) { canvasGroup = cg; }
    }
    private void Update()
    {
        if (item != null)
        {
            //image.sprite = item.sprite;
            //GetComponentInChildren<Text>().text = quantity.ToString();
        }
    }
    
}
