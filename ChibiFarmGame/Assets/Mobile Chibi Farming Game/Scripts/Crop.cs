using UnityEngine;

public class Crop : MonoBehaviour
{
    [Header(" Elements ")] 
    [SerializeField] private Transform cropRenderer;
    [SerializeField] private ParticleSystem harvestedParticle;
    
    

    public void ScaleUp()
    {
        //cropRenderer.localScale = Vector3.one;
        
        cropRenderer.gameObject.LeanScale(Vector3.one, 1).setEase(LeanTweenType.easeOutBack);
    }
    
    public void ScaleDown()
    {
        //cropRenderer.localScale = Vector3.one;
        
        cropRenderer.gameObject.LeanScale(Vector3.zero, 1).setEase(LeanTweenType.easeOutBack)
            .setOnComplete(()=> Destroy(gameObject));

        harvestedParticle.transform.parent = null;
        harvestedParticle.gameObject.SetActive(true);
        harvestedParticle.Play();
    }

}
