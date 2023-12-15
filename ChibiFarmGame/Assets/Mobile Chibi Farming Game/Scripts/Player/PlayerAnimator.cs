using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header(" Elements ")] 
    [SerializeField] private Animator animator;

    [Header(" Settings ")] 
    [SerializeField] private float moveSpeedMultiplier;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ManageAnimations(Vector3 moveVector)
    {
        if (moveVector.magnitude > 0)
        {
            animator.SetFloat("moveSpeed", moveVector.magnitude * moveSpeedMultiplier);
            PlayRunAnimation();

            animator.transform.forward = moveVector.normalized;
        }
        else
        {
            PlayIdleAnimation();
        }
    }

    private void PlayIdleAnimation()
    {
        animator.Play("idle");
    }

    private void PlayRunAnimation()
    {
        animator.Play("run");
    }
    
    private void PlaySowAnimation()
    {
        animator.Play("sow");
    }
}
