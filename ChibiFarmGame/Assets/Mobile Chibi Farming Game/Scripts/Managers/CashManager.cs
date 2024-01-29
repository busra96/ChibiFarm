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

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinContainers();
        Debug.Log(" We now have " + coins  + " coins ");

        SaveData();
        
    }

    private void UpdateCoinContainers()
    {
        CoinText.text = coins.ToString();
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
