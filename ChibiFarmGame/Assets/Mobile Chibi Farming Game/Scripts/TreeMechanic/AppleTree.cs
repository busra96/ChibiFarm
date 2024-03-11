using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header(" Elements ")] 
    [SerializeField] private GameObject treeCam;
    [SerializeField] private GameObject TreeMeshObj;
    private AppleTreeManager treeManager;
    public List<Apple> Apples;

    [Header(" Settings ")] 
    [SerializeField] private float shakeIncrement;
    private float shakeSliderValue;
    private bool isShaking;
    private Tween ShakingTween;


    public void Initialize(AppleTreeManager appleTreeManager)
    {
        EnableCam();

        shakeSliderValue = 0;
        
        treeManager = appleTreeManager;
    }
    
    [Button()]
    public void Shake()
    {
        UpdateShakeSlider();
        
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
    
    private void UpdateShakeSlider()
    {
        shakeSliderValue += shakeIncrement;
        treeManager.UpdateShakeSlider(shakeSliderValue);

        for (int i = 0; i <  Apples.Count; i++)
        {
            float applePercent = (float)i / Apples.Count;

            Apple currentApple = Apples[i].GetComponent<Apple>();

            if (shakeSliderValue > applePercent && !currentApple.IsFree())
                ReleaseApple(currentApple);
        }
    }

    private void ReleaseApple(Apple apple)
    {
        apple.Release();
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
