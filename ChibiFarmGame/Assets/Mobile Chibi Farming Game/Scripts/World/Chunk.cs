using UnityEngine;
using TMPro;
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

    // Start is called before the first frame update
    void Start()
    {
        currentPrice = initialPrice;
        priceText.text = currentPrice.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryUnlock()
    {
        if(CashManager.instance.GetCoins() <= 0)
            return;
        
        currentPrice--;
        CashManager.instance.UseCoins(1);
        priceText.text = currentPrice.ToString();

        if (currentPrice <= 0)
            Unlock();
    }

    private void Unlock()
    {
        unlockedElements.SetActive(true);
        lockedElements.SetActive(false);

        unlocked = true;
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
