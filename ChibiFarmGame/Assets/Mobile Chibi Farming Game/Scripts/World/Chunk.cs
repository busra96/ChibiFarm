using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(ChunkWalls))]
public class Chunk : MonoBehaviour
{
    [Header(" Elements ")] 
    [SerializeField] private GameObject unlockedElements;
    [SerializeField] private GameObject lockedElements;
    [SerializeField] private TextMeshPro priceText;
    private ChunkWalls chunkWalls;

    public Chunk frontChunk;
    public Chunk rightChunk;
    public Chunk backChunk;
    public Chunk leftChunk;

    [Header(" Settings ")] 
    [SerializeField] private int initialPrice;
    private int currentPrice;
    private bool unlocked;

    [Header(" Action ")] 
    public static Action onUnlocked;
    public static Action onPriceChanged;

    private void Awake()
    {
        chunkWalls = GetComponent<ChunkWalls>();
    }

    public void Initialize(int loadedPrice)
    {
        currentPrice = loadedPrice;
        priceText.text = currentPrice.ToString();
        
        if(currentPrice <= 0)
            Unlock(false);
    }

    public void TryUnlock()
    {
        if(CashManager.instance.GetCoins() <= 0)
            return;
        
        currentPrice--;
        CashManager.instance.UseCoins(1);
        
        onPriceChanged?.Invoke();
        
        priceText.text = currentPrice.ToString();

        if (currentPrice <= 0)
            Unlock();
    }
    
    private void Unlock(bool triggerAction = true)
    {
        unlockedElements.SetActive(true);
        lockedElements.SetActive(false);

        unlocked = true;
        
        if(triggerAction)
           onUnlocked?.Invoke();
    }

    public void UpdateWalls(int configuration)
    {
        chunkWalls.Configure(configuration);
    }

    public void DisplayLockedElements()
    {
        lockedElements.SetActive(true);
    }
    
    public bool IsUnlocked()
    {
        return unlocked;
    }

    public int GetInitialPrice()
    {
        return initialPrice;
    }

    public int GetCurrentPrice()
    {
        return currentPrice;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 6);

        Gizmos.color = new Color(0, 0, 0, 0);
        Gizmos.DrawCube(transform.position, Vector3.one * 6);
    }
}
