using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Inventory inventory;
    private string dataPath;
    
    void Start()
    {
        //inventory = new Inventory();
        dataPath = Application.dataPath + "/inventoryData.txt";
        LoadInventory();

        CropTile.onCropHarvested += CropHarvestedCallback;
    }

    private void OnDestroy()
    {
        CropTile.onCropHarvested -= CropHarvestedCallback;
    }

    private void CropHarvestedCallback(CropType cropType)
    {
        inventory.CropHarvestedCallback(cropType);
        SaveInventory();
    }

    private void LoadInventory()
    {
        string data = "";

        if (File.Exists(dataPath) == false)
            File.WriteAllText(dataPath, "{}");  // if  the file does not exist, create it and write an empty JSON in it
        
        
        data = File.ReadAllText(dataPath);
        inventory = JsonUtility.FromJson<Inventory>(data);

        if (inventory == null)
            inventory = new Inventory();
    }


    private void SaveInventory()
    {
        string data = JsonUtility.ToJson(inventory, true);
        File.WriteAllText(dataPath, data);
    }

}
