using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{

    [Header(" Elements ")]
    [SerializeField] private Transform CropContainersParent;
    [SerializeField] private UICropContainer uiCropContainerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Configure(Inventory inventory)
    {
        InventoryItem[] items = inventory.GetInventoryItems();

        for(int i = 0; i < items.Length; i++)
        {
            UICropContainer cropContainerInstance = Instantiate(uiCropContainerPrefab, CropContainersParent);
            cropContainerInstance.UpdateDisplay(items[i].amount);
        }
    }
}
