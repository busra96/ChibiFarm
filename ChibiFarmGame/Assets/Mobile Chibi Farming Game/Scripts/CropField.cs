using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropField : MonoBehaviour
{
    [Header(" Elements ")] 
    [SerializeField] private Transform tilesParent;
    private List<CropTile> cropTiles = new List<CropTile>();

    // Start is called before the first frame update
    void Start()
    {
        StoreTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StoreTiles()
    {
        for (int i = 0; i < tilesParent.childCount; i++)
            cropTiles.Add(tilesParent.GetChild(i).GetComponent<CropTile>());
    }

    public void SeedsCollidedCallback(Vector3[] seedPositions)
    {
        for (int i = 0; i < seedPositions.Length; i++)
        {
            CropTile closestCropTile = GetClosestCropTile(seedPositions[i]);
            
            
            if(closestCropTile == null)
                continue;
            
            if (!closestCropTile.IsEmpty())
                continue;

            Sow(closestCropTile);
        }
    }

    private void Sow(CropTile cropTile)
    {
        cropTile.Sow();
    }

    private CropTile GetClosestCropTile(Vector3 seedPosition)
    {
        float minDistance = 5000;
        int closestCropTileIndex = -1;
        
        for (int i = 0; i < cropTiles.Count; i++)
        {
            CropTile cropTile = cropTiles[i];
            float distanceTileSeed = Vector3.Distance(cropTile.transform.position, seedPosition);

            if (distanceTileSeed < minDistance)
            {
                minDistance = distanceTileSeed;
                closestCropTileIndex = i;
            }
        }
        
        if (closestCropTileIndex == -1)
            return null;

        return cropTiles[closestCropTileIndex];
    }
}
