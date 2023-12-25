using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [Header(" Elements ")] 
    [SerializeField] private Transform cropRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScaleUp()
    {
        cropRenderer.localScale = Vector3.one;
    }
}
