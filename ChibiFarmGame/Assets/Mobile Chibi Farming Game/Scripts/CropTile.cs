using UnityEngine;


public enum TieldFieldState { Empty, Sown, Watered}

public class CropTile : MonoBehaviour
{
    private TieldFieldState state;

    [Header(" Elements ")] 
    [SerializeField] private Transform cropParent;
    [SerializeField] private MeshRenderer tileRenderer;
    private Crop crop;
    
    
    // Start is called before the first frame update
    void Start()
    {
        state = TieldFieldState.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sow(CropData cropData)
    {
        state = TieldFieldState.Sown;

        crop = Instantiate(cropData.cropPrefab, transform.position, Quaternion.identity, cropParent);
        
    }

    public void Water()
    {
        state = TieldFieldState.Watered;
        tileRenderer.material.color = Color.white * .5f;

        crop.ScaleUp();
    }

    public bool IsEmpty()
    {
        return state == TieldFieldState.Empty;
    }

    public bool IsSown()
    {
        return state == TieldFieldState.Sown;
    }
}
