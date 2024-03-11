using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Apple : MonoBehaviour
{
    [Header(" Elements ")]
    private Rigidbody _rb;
    private Transform Player;
    private AppleTree AppleTree;

    [Header(" Settings ")]
    private bool isCollect;
    
    [Header(" Actions ")] 
    public static Action<CropType> onAppleHarvested;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        AppleTree = GetComponentInParent<AppleTree>();

        Player = FindObjectOfType<PlayerHarvestAbility>().transform;
    }



    public bool IsFree()
    {
        return !_rb.isKinematic;
    }

    private void Collect()
    {
        transform.DOScale(Vector3.zero, .2f).SetEase(Ease.Linear);
        transform.DOJump(Player.transform.position, 1, 1, .2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            onAppleHarvested?.Invoke(CropType.Apple);
            transform.SetParent(AppleTree.ApplesParent);
        });
    }

    public void Release()
    {
        _rb.isKinematic = false;
        transform.SetParent(null);

        LeanTween.delayedCall(1, () =>
        {
            Collect();
        });
    }
}
