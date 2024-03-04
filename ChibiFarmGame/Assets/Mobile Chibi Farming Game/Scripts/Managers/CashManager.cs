using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static CashManager instance;
    
    [Header(" Settings ")] 
    private int coins;

    [Header(" Elements ")] 
    [SerializeField] private TextMeshProUGUI CoinText;
    
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        LoadData();
        UpdateCoinContainers();
    }

    [Button()]
    private void Add500Coins()
    {
        AddCoins(500);
    }

    public void UseCoins(int amount)
    {
        AddCoins(-amount);
    }
    
    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinContainers();

        SaveData();
        
    }

    private void UpdateCoinContainers()
    {
        CoinText.text = coins.ToString();
    }

    public int GetCoins()
    {
        return coins;
    }
    private void LoadData()
    {
        coins = PlayerPrefs.GetInt("Coins");
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("Coins",coins);
    }

    public void ResetData()
    {
        PlayerPrefs.SetInt("Coins",0);
    }
}
