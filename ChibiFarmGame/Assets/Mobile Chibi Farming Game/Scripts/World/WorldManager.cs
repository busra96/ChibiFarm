using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Header(" Elements ")] 
    [SerializeField] private Transform world;
    
    [Header(" Data ")]
    private WorldData worldData;
    
    void Start()
    {
        LoadWorld();
    }

    private void LoadWorld()
    {
        string dataPath = Application.dataPath + "/WorldData.txt";
        string data = "";

        if (!File.Exists(dataPath))
        {
            FileStream fs = new FileStream(dataPath, FileMode.Create);

            worldData = new WorldData(world.childCount);

            for (int i = 0; i < world.childCount; i++)
                worldData.chunkPrices[i] = world.GetChild(i).GetComponent<Chunk>().GetInitialPrice();

            string worldDataString = JsonUtility.ToJson(worldData, true);

            byte[] worldDataBytes = Encoding.UTF8.GetBytes(worldDataString);
            
            fs.Write(worldDataBytes);
            
            fs.Close();
        }
        else
        {
            data = File.ReadAllText(dataPath);
            worldData = JsonUtility.FromJson<WorldData>(data);

        }
    }

    private void SaveWorld()
    {
        
    }
}
