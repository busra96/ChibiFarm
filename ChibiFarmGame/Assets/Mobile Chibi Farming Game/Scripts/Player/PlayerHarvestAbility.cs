using UnityEngine;


[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerToolSelector))]
public class PlayerHarvestAbility : MonoBehaviour
{
    [Header(" Elements ")]
    private PlayerAnimator playerAnimator;
    private PlayerToolSelector playerToolSelector;

    [Header(" Settings ")]
    private CropField currentCropField;
    
    
    void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        playerToolSelector = GetComponent<PlayerToolSelector>();

        //WaterParticles.onWatersCollided += WatersCollidedCallback;
       
        CropField.onFullyHarvested += CropFieldFullyHarvestedCallback;
        
        playerToolSelector.onToolSelected += ToolSelectedCallback;
    }

    private void OnDestroy()
    {
        //WaterParticles.onWatersCollided -= WatersCollidedCallback;
       
        CropField.onFullyHarvested -= CropFieldFullyHarvestedCallback;
        
        playerToolSelector.onToolSelected -= ToolSelectedCallback;
    }

    private void ToolSelectedCallback(PlayerToolSelector.Tool selectedTool)
    {
        if (!playerToolSelector.CanHarvest())
            playerAnimator.StopHarvestAnimation();
    }

    private void CropFieldFullyHarvestedCallback(CropField cropField)
    {
        if(currentCropField == cropField)
            playerAnimator.StopHarvestAnimation();
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropField") && other.GetComponent<CropField>().IsWatered())
        {
            currentCropField = other.GetComponent<CropField>();
            EnteredCropField(currentCropField);
        }
    }

    private void EnteredCropField(CropField cropField)
    {
        if (playerToolSelector.CanHarvest())
        {
            if(currentCropField == null)
                currentCropField = cropField;
            
            playerAnimator.PlayHarvestAnimation();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CropField")  && other.GetComponent<CropField>().IsWatered())
            EnteredCropField(other.GetComponent<CropField>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            playerAnimator.StopHarvestAnimation();
            currentCropField = null;
        }
    }
}
