using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICropContainer : MonoBehaviour
{
    [Header(" Elements ")] 
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI amountText;

    public void Configure(Sprite icon, int amount)
    {
        this.icon.sprite = icon;
        amountText.text = amount.ToString();
    }

    public void UpdateDisplay(int amount)
    {
        amountText.text = amount.ToString();
    }
}
