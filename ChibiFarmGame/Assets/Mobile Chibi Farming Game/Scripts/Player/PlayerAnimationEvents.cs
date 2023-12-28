using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEvents : MonoBehaviour
{
    [Header(" Elements ")] 
    [SerializeField] private ParticleSystem seedParticles;
    [SerializeField] private ParticleSystem waterParticles;

    [Header(" Events ")] 
    [SerializeField] private UnityEvent startHarvestingEvents;
    [SerializeField] private UnityEvent stopHarvestingEvents;
    
    
    
    private void PlaySeedParticles()
    {
        seedParticles.Play();
    }
    
    private void PlayWaterParticles()
    {
        waterParticles.Play();
    }

    private void StartHarvestingCallback()
    {
        startHarvestingEvents?.Invoke();
    }
    
    private void StopHarvestingCallback()
    {
        stopHarvestingEvents?.Invoke();
    }
}
