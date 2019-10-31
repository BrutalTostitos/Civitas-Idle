using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTileController : MonoBehaviour
{
    public SpriteRenderer BuildingSprite;
    public SpriteRenderer BuildingSpriteLower;
    public SpriteRenderer BuildingSpriteUpper;

    // Start is called before the first frame update
    void Start()
    {
        BuildingSpriteLower.sprite = null;
        BuildingSpriteUpper.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBuilding(BuildingObject buildingObject)
    {
        if (buildingObject.IsMultiSprite)
        {

            BuildingSpriteLower.sprite = buildingObject.BuildingTileSpriteLower;
            BuildingSpriteUpper.sprite = buildingObject.BuildingTileSpriteUpper;

        }
    }
}
