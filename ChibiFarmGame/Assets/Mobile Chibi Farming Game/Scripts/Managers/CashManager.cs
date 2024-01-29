using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static CashManager instance;
    
    [Header(" Settings ")] 
    private int coins;
    
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        LoadData();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log(" We now have " + coins  + " coins ");

        SaveData();
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
