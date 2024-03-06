using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [Header(" Elements ")] 
    [SerializeField] private GameObject treeButton;
    [SerializeField] private GameObject toolButtonsContainer;

    private void Awake()
    {
        PlayerDetection.onEnteredTreeZone += EnteredTreeZoneCallback;
        PlayerDetection.onExitedTreeZone += ExitedTreeZoneCallback;
    }

    private void OnDestroy()
    {
        PlayerDetection.onEnteredTreeZone -= EnteredTreeZoneCallback;
        PlayerDetection.onExitedTreeZone -= ExitedTreeZoneCallback;
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
        treeButton.SetActive(true);
        toolButtonsContainer.SetActive(false);
    }
    
    private void ExitedTreeZoneCallback(AppleTree tree)
    {
        toolButtonsContainer.SetActive(true);
        treeButton.SetActive(false);
    }
}
