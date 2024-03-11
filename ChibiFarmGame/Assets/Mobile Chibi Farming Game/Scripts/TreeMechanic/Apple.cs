using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Apple : MonoBehaviour
{
    enum State { Ready, Growing }
    private State state;
    
    [Header(" Elements ")]
    private Rigidbody _rb;
    private Transform Player;
    private AppleTree AppleTree;

    [Header(" Settings ")]
    private bool isCollect;
    private Vector3 initialPos;
    private Quaternion initialRot;
    
    [Header(" Actions ")] 
    public static Action<CropType> onAppleHarvested;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        AppleTree = GetComponentInParent<AppleTree>();

        initialPos = transform.position;
        initialRot = transform.rotation;

        Player = FindObjectOfType<PlayerHarvestAbility>().transform;
    }

    private void Start()
    {
        state = State.Ready;
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
        
        state = State.Growing;

        LeanTween.delayedCall(1, () =>
        {
            Collect();
        });
    }

    public void Reset()
    {
        LeanTween.scale(gameObject, Vector3.zero, 1).setDelay(2).setOnComplete(ForceReset);
    }

    public bool IsReady()
    {
        return state == State.Ready;
    }

    private void ForceReset()
    {
        transform.SetParent(AppleTree.ApplesParent);
        transform.position = initialPos;
        transform.rotation = initialRot;

        _rb.isKinematic = true;

        
        //Scale up
        float randomScaleTime = Random.Range(5, 10);
        LeanTween.scale(gameObject, Vector3.one, randomScaleTime).setOnComplete(SetReady);

    }

    private void SetReady()
    {
        state = State.Ready;
    }
}
