using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    [SerializeField] private List<InventoryItem> items = new List<InventoryItem>();

    public void CropHarvestedCallback(CropType cropType)
    {
        bool cropFound = false;

        for (int i = 0; i < items.Count; i++)
        {

            InventoryItem item = items[i];

            if (item.cropType == cropType)
            {
                item.amount++;
                cropFound = true;
                break;
            }
        }
        
        if(cropFound)
            return;
        
        //Create a new item in the list with that cropType
        items.Add(new InventoryItem(cropType,1));
    }

    public void Clear()
    {
        items.Clear();
    }

    public InventoryItem[] GetInventoryItems()
    {
      return items.ToArray();
    }
}
