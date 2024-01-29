using System.Collections;
using System.Collections.Generic;
using System.IO;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(InventoryDisplay))]
public class InventoryManager : MonoBehaviour
{
    private Inventory inventory;
    private InventoryDisplay inventoryDisplay;
    private string dataPath;
    
    void Start()
    {
        //inventory = new Inventory();
        dataPath = Application.dataPath + "/inventoryData.txt";
        LoadInventory();
        
        ConfigureInventoryDisplay();
       
        CropTile.onCropHarvested += CropHarvestedCallback;
    }

    private void OnDestroy()
    {
        CropTile.onCropHarvested -= CropHarvestedCallback;
    }

    private void ConfigureInventoryDisplay()
    {
         inventoryDisplay = GetComponent<InventoryDisplay>();
        inventoryDisplay.Configure(inventory);
    }

    private void CropHarvestedCallback(CropType cropType)
    {

        //update our inventory
        inventory.CropHarvestedCallback(cropType);

        inventoryDisplay.UpdateDisplay(inventory);

        SaveInventory();
    }

    [Button()]
    public void ClearInventory()
    {
        inventory.Clear();
        inventoryDisplay.UpdateDisplay(inventory);
        
        SaveInventory();
    }

    
    public Inventory GetInventory()
    {
        return inventory;
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
