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
}
