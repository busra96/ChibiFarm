using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Apple : MonoBehaviour
{
    [Header(" Elements ")]
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public bool IsFree()
    {
        return !_rb.isKinematic;
    }

    public void Release()
    {
        _rb.isKinematic = false;
        transform.SetParent(null);
    }
}
