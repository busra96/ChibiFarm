using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header(" Elements ")] 
    [SerializeField] private GameObject treeCam;
    [SerializeField] private GameObject TreeMeshObj;

    [Header(" Settings ")]
    private bool isShaking;
    private Tween ShakingTween;

    [Button()]
    public void Shake()
    {
        if(isShaking)
            return;

        isShaking = true;
        ShakingTween = TreeMeshObj.transform.DOShakeRotation(duration: .5f, strength: 2.5f, vibrato: 2, randomness: 60, fadeOut: true).SetLoops(-1,LoopType.Restart);
    }

    public void StopShaking()
    {
        if(!isShaking)
            return;

        isShaking = false;
        if(ShakingTween != null)
            ShakingTween.Kill();

        TreeMeshObj.transform.DOLocalRotate(new Vector3(0,50,0), .5f).SetEase(Ease.Linear);
    }

    public void EnableCam()
    {
        treeCam.SetActive(true);
    }

    public void DisableCam()
    {
        treeCam.SetActive(false);
    }
}
