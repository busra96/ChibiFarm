using System;
using UnityEngine;
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerShakeTreeAbility : MonoBehaviour
{
    [Header(" Components ")] 
    private PlayerAnimator playerAnimator;
    
    [Header(" Settings ")] 
    [SerializeField] private float distanceToTree;
    [Range(0,1)]
    [SerializeField ]private float shakeThreshold;
    private bool isActive;
    private Vector2 previousMousePosition;
    
    [Header(" Elements ")] 
    private AppleTree currentTree;
    
    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        AppleTreeManager.onTreeModeStarted += TreeModeStartedCallback;
    }

    private void OnDestroy()
    {
        AppleTreeManager.onTreeModeStarted -= TreeModeStartedCallback;
    }

    private void Update()
    {
        if(isActive)
           ManageTreeShaking();
    }

    private void TreeModeStartedCallback(AppleTree tree)
    {
        currentTree = tree;

        isActive = true;
        
        MoveTowardsTree();
    }

    private void MoveTowardsTree()
    {
        Vector3 treePos = currentTree.transform.position;
        Vector3 dir = transform.position - treePos;

        Vector3 flatDir = dir;
        flatDir.y = 0;

        Vector3 targetPos = treePos + flatDir.normalized * distanceToTree;

        playerAnimator.ManageAnimations(-flatDir);
        
        LeanTween.move(gameObject, targetPos, .5f);

    }

    private void ManageTreeShaking()
    {
        if(!Input.GetMouseButton(0))
            return;

        float shakeMagnitude = Vector2.Distance(Input.mousePosition, previousMousePosition);

        if (ShouldShake(shakeMagnitude))
            Shake();
        
        previousMousePosition = Input.mousePosition;
    }

    private void Shake()
    {
        playerAnimator.PlayShakeTreeAnimation();
    }
    
    private bool ShouldShake(float shakeMagnitude)
    {
        float screenPercent = shakeMagnitude / Screen.width;

        return screenPercent >= shakeThreshold;
    }
}
