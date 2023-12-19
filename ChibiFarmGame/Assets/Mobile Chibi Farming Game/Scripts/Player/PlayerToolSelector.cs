using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerToolSelector : MonoBehaviour
{
    
    public enum  Tool {  None, Sow, Water, Harvest }
    private Tool activeTool;

    [Header(" Elements ")] 
    [SerializeField] private Image[] toolImages;
     

    [Header(" Settings ")] 
    [SerializeField] private Color SelectToolColor;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SelectTool(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectTool(int toolIndex)
    {
        activeTool = (Tool)toolIndex;

        for (int i = 0; i < toolImages.Length; i++)
        {
            toolImages[i].color = i == toolIndex ? SelectToolColor : Color.white;
        }
    }
}
