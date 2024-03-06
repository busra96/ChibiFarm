using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTreeManager : MonoBehaviour
{
    [Header(" Settings ")] 
    private AppleTree LastTriggeredTree;
    
    private void Awake()
    {
        PlayerDetection.onEnteredTreeZone += EnteredTreeZoneCallback;
    }

    private void OnDestroy()
    {
        PlayerDetection.onEnteredTreeZone -= EnteredTreeZoneCallback;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void EnteredTreeZoneCallback(AppleTree tree)
    {
        LastTriggeredTree = tree;
    }

    public void TreeButtonCallback()
    {
        Debug.Log(" Tree button click ");
        LastTriggeredTree.EnableCam();
    }
}
