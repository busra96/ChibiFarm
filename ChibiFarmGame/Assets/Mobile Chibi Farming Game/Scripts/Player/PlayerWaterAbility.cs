using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerToolSelector))]
public class PlayerWaterAbility : MonoBehaviour
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

        WaterParticles.onWatersCollided += WatersCollidedCallback;
       
        CropField.onFullyWatered += CropFieldFullyWaterCallback;
        
        playerToolSelector.onToolSelected += ToolSelectedCallback;
    }

    private void OnDestroy()
    {
        WaterParticles.onWatersCollided -= WatersCollidedCallback;
       
        CropField.onFullyWatered -= CropFieldFullyWaterCallback;
        
        playerToolSelector.onToolSelected -= ToolSelectedCallback;
    }

    private void ToolSelectedCallback(PlayerToolSelector.Tool selectedTool)
    {
        if (!playerToolSelector.CanWater())
            playerAnimator.StopWaterAnimation();
    }

    private void WatersCollidedCallback(Vector3[] waterPositions)
    {
        if(currentCropField == null)
            return;

        currentCropField.WaterColliderCallback(waterPositions);
    }

    private void CropFieldFullyWaterCallback(CropField cropField)
    {
        if(currentCropField == cropField)
            playerAnimator.StopWaterAnimation();
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropField") && other.GetComponent<CropField>().IsSown())
        {
            currentCropField = other.GetComponent<CropField>();
            EnteredCropField(currentCropField);
        }
    }

    private void EnteredCropField(CropField cropField)
    {
        if (playerToolSelector.CanWater())
        {
            if(currentCropField == null)
                currentCropField = cropField;
            
            playerAnimator.PlayWaterAnimation();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CropField")  && other.GetComponent<CropField>().IsSown())
            EnteredCropField(other.GetComponent<CropField>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            playerAnimator.StopWaterAnimation();
            currentCropField = null;
        }
    }
}
