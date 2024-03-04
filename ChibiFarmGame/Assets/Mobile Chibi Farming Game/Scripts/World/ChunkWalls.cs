using UnityEngine;

public class ChunkWalls : MonoBehaviour
{
    [Header(" Elements ")] 
    [SerializeField] private GameObject frontWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject backWall;
    [SerializeField] private GameObject leftWall;

    public void Configure(int configuration)
    {
        
        if (IsKthBitSet(configuration, 0))
            frontWall.SetActive(true);
        
        if(IsKthBitSet(configuration, 1))
            rightWall.SetActive(true);
        
        if(IsKthBitSet(configuration, 2))
            backWall.SetActive(true);
        
        if(IsKthBitSet(configuration, 3))
            leftWall.SetActive(true);
    }

    public bool IsKthBitSet(int configuration, int k)
    {
        if ((configuration & (1 << k)) > 0)
            return false;
        else
            return true;

    }
}
