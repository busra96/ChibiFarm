using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTile : MonoBehaviour
{
    
    public enum State { Empty, Sown, Watered}

    private State state;
    
    // Start is called before the first frame update
    void Start()
    {
        state = State.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sow()
    {
        state = State.Sown;

        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.transform.position = transform.position;
        go.transform.localScale = Vector3.one * .5f;
    }

    public bool IsEmpty()
    {
        return state == State.Empty;
    }
}
