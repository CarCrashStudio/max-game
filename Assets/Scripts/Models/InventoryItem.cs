using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public Item item;
    public int quantity;

    public InventoryItem ()
    {

    }

    public InventoryItem (Item i, int q)
    {
        item = i;
        quantity = q;
    }
}
