using UnityEngine;

public class UIManager : MonoBehaviour
{

    [Header(" Elements ")] 
    [SerializeField]private GameObject gamePanel;
    [SerializeField]private GameObject treeModelPanel;
    
    [SerializeField] private GameObject treeButton;
    [SerializeField] private GameObject toolButtonsContainer;

    private void Awake()
    {
        PlayerDetection.onEnteredTreeZone += EnteredTreeZoneCallback;
        PlayerDetection.onExitedTreeZone += ExitedTreeZoneCallback;

        AppleTreeManager.onTreeModeStarted += SetTreeMode;
    }

    private void OnDestroy()
    {
        PlayerDetection.onEnteredTreeZone -= EnteredTreeZoneCallback;
        PlayerDetection.onExitedTreeZone -= ExitedTreeZoneCallback;
        
        AppleTreeManager.onTreeModeStarted -= SetTreeMode;
    }

    // Start is called before the first frame update
    void Start()
    {
       SetGameMode();   
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

    private void SetGameMode()
    {
        gamePanel.SetActive(true);
        treeModelPanel.SetActive(false);
    }

    private void SetTreeMode(AppleTree tree)
    {
        treeModelPanel.SetActive(true);
        gamePanel.SetActive(false);
    }
}
