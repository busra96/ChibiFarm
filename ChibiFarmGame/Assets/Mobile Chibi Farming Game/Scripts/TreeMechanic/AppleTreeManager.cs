using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AppleTreeManager : MonoBehaviour
{
    [Header(" Settings ")] 
    private AppleTree LastTriggeredTree;

    [Header(" Actions ")] 
    public static Action onTreeModeStarted;

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
        LastTriggeredTree.EnableCam();
        
        onTreeModeStarted?.Invoke();
    }
}
