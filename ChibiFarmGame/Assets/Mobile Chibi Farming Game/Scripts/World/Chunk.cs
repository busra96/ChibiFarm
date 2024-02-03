using UnityEngine;
using TMPro;
using System;
public class Chunk : MonoBehaviour
{
    [Header(" Elements ")] 
    [SerializeField] private GameObject unlockedElements;
    [SerializeField] private GameObject lockedElements;
    [SerializeField] private TextMeshPro priceText;


    [Header(" Settings ")] 
    [SerializeField] private int initialPrice;
    private int currentPrice;
    private bool unlocked;

    [Header(" Action ")] 
    public static Action onUnlocked;
    public static Action onPriceChanged;

    
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

    
}
