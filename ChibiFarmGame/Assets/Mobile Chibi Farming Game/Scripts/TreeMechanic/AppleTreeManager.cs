using System;
using UnityEngine;
using UnityEngine.UI;


public class AppleTreeManager : MonoBehaviour
{
    [Header(" Elements ")] 
    [SerializeField] private Slider ShakeSlider;
    
    [Header(" Settings ")] 
    private AppleTree LastTriggeredTree;

    [Header(" Actions ")] 
    public static Action<AppleTree> onTreeModeStarted;
    public static Action  onTreeModeSEnded;

    private void Awake()
    {
        PlayerDetection.onEnteredTreeZone += EnteredTreeZoneCallback;
    }

    private void OnDestroy()
    {
        PlayerDetection.onEnteredTreeZone -= EnteredTreeZoneCallback;
    }
    
    private void EnteredTreeZoneCallback(AppleTree tree)
    {
        LastTriggeredTree = tree;
    }

    public void TreeButtonCallback()
    {
        Debug.Log(" Tree button click ");
       

        StartTreeMode();
    }

    private void StartTreeMode()
    {
        LastTriggeredTree.Initialize(this);
        LastTriggeredTree.EnableCam();
        
        onTreeModeStarted?.Invoke(LastTriggeredTree);
        
        //Initialize the slider
        UpdateShakeSlider(0);
    }

    public void UpdateShakeSlider(float value)
    {
        ShakeSlider.value = value;
    }

    public void EndTreeMode()
    {
     onTreeModeSEnded?.Invoke();   
    }
}
