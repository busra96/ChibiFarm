using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        priceText.text = initialPrice.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryUnlock()
    {
        Debug.Log(" Trying to unlock the chunk " + name);
    }
}
