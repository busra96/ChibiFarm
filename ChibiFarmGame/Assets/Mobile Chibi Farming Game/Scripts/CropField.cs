using System.Collections.Generic;
using UnityEngine;
using System;

public class CropField : MonoBehaviour
{
    [Header(" Elements ")] 
    [SerializeField] private Transform tilesParent;
    private List<CropTile> cropTiles = new List<CropTile>();

    [Header(" Settings ")] 
    [SerializeField] private CropData cropData;
    private TieldFieldState state;
    
    private int tileSown;

    [Header(" Actions ")]
    public static Action<CropField> onFullSown;

    // Start is called before the first frame update
    void Start()
    {
        state = TieldFieldState.Empty;
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
        cropTile.Sow(cropData);
        tileSown++;

        if (tileSown == cropTiles.Count)
            FieldFullySown();
    }

    public void WaterColliderCallback(Vector3[] waterPositions)
    {
        
    }

    private void FieldFullySown()
    {
        Debug.Log(" Field fully sown ");
        
        state = TieldFieldState.Sown;
        onFullSown?.Invoke(this);
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
    
    public bool IsEmpty()
    {
        return state == TieldFieldState.Empty;
    }

    public bool IsSown()
    {
        return state == TieldFieldState.Sown;
    }
    
    public bool IsWatered()
    {
        return state == TieldFieldState.Watered;
    }
}
