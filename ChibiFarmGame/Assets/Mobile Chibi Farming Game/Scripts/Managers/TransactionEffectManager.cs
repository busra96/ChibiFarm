using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransactionEffectManager : MonoBehaviour
{
    [Header(" Elements ")] 
    [SerializeField] private ParticleSystem coinPS;

    [Header(" Settings ")] 
    private int coinsAmount;

    [NaughtyAttributes.Button()]
    private void PlayCoinParticlesTest()
    {
        PlayCoinParticles(100);
    }
    

    public void PlayCoinParticles(int amount)
    {
        ParticleSystem.Burst burst = coinPS.emission.GetBurst(0);
        burst.count = amount;
        
        coinPS.emission.SetBurst(0, burst);
        
        coinPS.Play();

        coinsAmount = amount;

        StartCoroutine(PlayCoinParticlesCoroutine());
    }

    IEnumerator PlayCoinParticlesCoroutine()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[coinsAmount];

        while (coinPS.isPlaying)
        {
            coinPS.GetParticles(particles);
            
            for (int i = 0; i < particles.Length; i++)
                particles[i].position += Vector3.forward * Time.deltaTime * 5;
            
            coinPS.SetParticles(particles);
            yield return null;

        }
       
    }
}
