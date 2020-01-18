using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTileController : MonoBehaviour
{
    public SpriteRenderer BuildingSprite;
    public SpriteRenderer BuildingSpriteLower;
    public SpriteRenderer BuildingSpriteUpper;

    public bool isDiscovered = false;
    public bool isResourceTile = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBuilding(BuildingObject buildingObject)
    {
        //Cannot build on a resource tile.
        if (isResourceTile)
        {
            return;
        }
        NullifySprites();
        if (buildingObject.IsMultiSprite)
        {
            

            BuildingSpriteLower.sprite = buildingObject.BuildingTileSpriteLower;
            BuildingSpriteUpper.sprite = buildingObject.BuildingTileSpriteUpper;

        }
        else
        {
            BuildingSprite.sprite = buildingObject.BuildingTileSprite;
        }
    }

    void NullifySprites()
    {
        BuildingSprite.sprite = null;
        BuildingSpriteLower.sprite = null;
        BuildingSpriteUpper.sprite = null;
    }

    //TODO : Create a cost to discovering more land.
    public void Discover()
    {
        isDiscovered = true;
        EventController.getInstance().OnDiscover();
    }
}
