using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{

    [Header(" Elements ")]
    [SerializeField] private Transform CropContainersParent;
    [SerializeField] private UICropContainer uiCropContainerPrefab;



    public void Configure(Inventory inventory)
    {
        InventoryItem[] items = inventory.GetInventoryItems();

        for(int i = 0; i < items.Length; i++)
        {
            UICropContainer cropContainerInstance = Instantiate(uiCropContainerPrefab, CropContainersParent);

            Sprite cropIcon = DataManager.instance.GetCropSpriteFromCropType(items[i].cropType);

            cropContainerInstance.Configure(cropIcon,items[i].amount);
        }
    }

    public void UpdateDisplay(Inventory inventory)
    {
        InventoryItem[] items = inventory.GetInventoryItems();
        
        for(int i = 0; i < items.Length; i++)
        {
            UICropContainer containerInstance;
            
            if (i < CropContainersParent.childCount)
            {
                containerInstance = CropContainersParent.GetChild(i).GetComponent<UICropContainer>();
                containerInstance.gameObject.SetActive(true);
            }
            else
                containerInstance = Instantiate(uiCropContainerPrefab, CropContainersParent);
           
            
            Sprite cropIcon = DataManager.instance.GetCropSpriteFromCropType(items[i].cropType);
            containerInstance.Configure(cropIcon, items[i].amount);
        }

        int remainingContainers = CropContainersParent.childCount - items.Length;
        
        if(remainingContainers <= 0)
            return;

        for (int i = 0; i < remainingContainers; i++)
                CropContainersParent.GetChild(items.Length + i).gameObject.SetActive(false);

    }

    /* public void UpdateDisplay(Inventory inventory)
     {
         InventoryItem[] items = inventory.GetInventoryItems();
 
         //Clear the crop containers parent if there are any ui crop container
         while (CropContainersParent.childCount > 0 )
         {
             Transform container = CropContainersParent.GetChild(0);
             container.SetParent(null);
             Destroy(container.gameObject);
         }
         
         // create the ui crop containers from scratch again
         Configure(inventory);
         
         // for(int i = 0; i < items.Length; i++)
         // {
         //     UICropContainer cropContainerInstance = Instantiate(uiCropContainerPrefab, CropContainersParent);
         //
         //     Sprite cropIcon = DataManager.instance.GetCropSpriteFromCropType(items[i].cropType);
         //
         //     cropContainerInstance.Configure(cropIcon,items[i].amount);
         // }
     }*/
}
