using System.IO;
using System.Text;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Header(" Elements ")] 
    [SerializeField] private Transform world;
    private Chunk[,] grid;

    [Header(" Settings ")] 
    [SerializeField] private int gridSize;
    [SerializeField] private int gridScale;
    
    [Header(" Data ")]
    private WorldData worldData;
    private string dataPath;
    private bool shouldSave;
    
    private void Awake()
    {
        Chunk.onUnlocked += ChunkUnlockedCallback;
        Chunk.onPriceChanged += ChunkPriceChangedCallback;
    }
    
    void Start()
    {
        dataPath = Application.dataPath + "/WorldData.txt";
        LoadWorld();
        Initialize();
        
        InvokeRepeating("TrySaveGame", 1, 1);
    }

    private void OnDestroy()
    {
        Chunk.onUnlocked -= ChunkUnlockedCallback;
        Chunk.onPriceChanged -= ChunkPriceChangedCallback;
    }

    private void Initialize()
    {
        for (int i = 0; i < world.childCount; i++)
            world.GetChild(i).GetComponent<Chunk>().Initialize(worldData.chunkPrices[i]);

        InitializeGrid();
        
        UpdateChunkWalls();
    }

    private void InitializeGrid()
    {
        grid = new Chunk[gridSize, gridSize];

        for (int i = 0; i < world.childCount; i++)
        {
            Chunk chunk = world.GetChild(i).GetComponent<Chunk>();

            Vector2Int chunkGridPosition = new Vector2Int((int)chunk.transform.position.x/ gridScale, (int)chunk.transform.position.z/ gridScale);
           // Debug.Log(" Vector2Int " + chunkGridPosition.x + " // " + chunkGridPosition.y);

            chunkGridPosition += new Vector2Int(gridSize / 2, gridSize / 2);
            
            //Debug.Log(" Vector2Int Last " + chunkGridPosition.x + " // " + chunkGridPosition.y);

            grid[chunkGridPosition.x, chunkGridPosition.y] = chunk;
        }
        
    }

    private void UpdateChunkWalls()
    {
        //loop along the x axis
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            //loop along the z axis
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                Chunk chunk = grid[x, y];
                
                if(chunk == null)
                    continue;

                chunk.frontChunk = GetChunk(x, y + 1);
                chunk.rightChunk = GetChunk(x + 1, y);
                chunk.backChunk  = GetChunk(x, y - 1);
                chunk.leftChunk  = GetChunk(x - 1, y);

                int configuration = 0;

                if (chunk.frontChunk != null && chunk.frontChunk.IsUnlocked())
                    configuration = configuration + 1;
                
                if (chunk.rightChunk != null && chunk.rightChunk.IsUnlocked())
                    configuration = configuration + 2;
                
                if (chunk.backChunk != null && chunk.backChunk.IsUnlocked())
                    configuration = configuration + 4;
                
                if (chunk.leftChunk != null && chunk.leftChunk.IsUnlocked())
                    configuration = configuration + 8;
                
                // We know the configuration of the chunk
                chunk.UpdateWalls(configuration);
            }
        }
    }

    public Chunk GetChunk(int x, int y)
    {
        Chunk chunk = null;

        if (IsValidGridPosition(x, y))
            chunk = grid[x, y];
        
        return chunk;
    }

    private bool IsValidGridPosition(int x, int y)
    {
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize)
            return false;

        return true;
    }

    private void TrySaveGame()
    {
       // Debug.Log(" Try save game ");

        if (shouldSave)
        {
            SaveWorld();
            shouldSave = false;
        }
           
    }
    
    private void ChunkUnlockedCallback()
    {
        Debug.Log(" Chunk unlocked ");

        SaveWorld();
    }

    private void ChunkPriceChangedCallback()
    {
        shouldSave = true;
    }

    private void LoadWorld()
    {
        string data = "";

        if (!File.Exists(dataPath))
        {
            FileStream fs = new FileStream(dataPath, FileMode.Create);

            worldData = new WorldData();

            for (int i = 0; i < world.childCount; i++)
            {
                int chunkInitialPrice = world.GetChild(i).GetComponent<Chunk>().GetInitialPrice();
                worldData.chunkPrices.Add(chunkInitialPrice); 
            }

            string worldDataString = JsonUtility.ToJson(worldData, true);

            byte[] worldDataBytes = Encoding.UTF8.GetBytes(worldDataString);
            
            fs.Write(worldDataBytes);
            
            fs.Close();
        }
        else
        {
            data = File.ReadAllText(dataPath);
            worldData = JsonUtility.FromJson<WorldData>(data);

            if (worldData.chunkPrices.Count < world.childCount)
                UpdateData();
        }
    }

    private void UpdateData()
    {
        //how many chunks are missing in our data
        int missingData = world.childCount - worldData.chunkPrices.Count;

        for (int i = 0; i < missingData; i++)
        {
            int chunkIndex = world.childCount - missingData + i;
            int chunkPrice = world.GetChild(chunkIndex).GetComponent<Chunk>().GetInitialPrice();
            worldData.chunkPrices.Add(chunkPrice);
        }

    }

    private void SaveWorld()
    {
        if (worldData.chunkPrices.Count != world.childCount)
            worldData = new WorldData();

        for (int i = 0; i < world.childCount; i++)
        {
            int chunkCurrentPrice = world.GetChild(i).GetComponent<Chunk>().GetCurrentPrice();

            if (worldData.chunkPrices.Count > i)
                worldData.chunkPrices[i] = chunkCurrentPrice;
            else
                worldData.chunkPrices.Add(chunkCurrentPrice); 
        }

        string data = JsonUtility.ToJson(worldData, true);
        
        File.WriteAllText(dataPath, data);
        
        Debug.LogWarning(" Data Saved !");
    }
}
