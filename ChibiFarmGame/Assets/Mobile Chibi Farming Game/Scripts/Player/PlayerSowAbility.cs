using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class PlayerSowAbility : MonoBehaviour
{
    [Header(" Elements ")]
    private PlayerAnimator playerAnimator;

    [Header(" Settings ")]
    private CropField currentCropField;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();

        SeedParticles.onSeedsCollided += SeedsCollidedCallback;
    }

    private void OnDestroy()
    {
        SeedParticles.onSeedsCollided -= SeedsCollidedCallback;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SeedsCollidedCallback(Vector3[] seedPositions)
    {
        if(currentCropField == null)
            return;

        
        currentCropField.SeedsCollidedCallback(seedPositions);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            playerAnimator.PlaySowAnimation();
            currentCropField = other.GetComponent<CropField>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            playerAnimator.StopSowAnimation();
            currentCropField = null;
        }
    }
}
