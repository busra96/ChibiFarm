using UnityEngine;

public class CropTile : MonoBehaviour
{
    
    public enum State { Empty, Sown, Watered}
    private State state;

    [Header(" Elements ")] 
    [SerializeField] private Transform cropParent;
    
    // Start is called before the first frame update
    void Start()
    {
        state = State.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sow(CropData cropData)
    {
        state = State.Sown;

        Crop crop = Instantiate(cropData.cropPrefab, transform.position, Quaternion.identity, cropParent);
        
    }

    public bool IsEmpty()
    {
        return state == State.Empty;
    }
}
