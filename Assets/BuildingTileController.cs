using EventCallBacks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTileController : MonoBehaviour
{
    public SpriteRenderer BuildingSprite;
    public SpriteRenderer BuildingSpriteLower;
    public SpriteRenderer BuildingSpriteUpper;

    public GameObject DiscoverButtonObject;
    public TextMeshPro ButtonText;

    public bool isDiscovered = false;
    public bool isResourceTile = false;
    public int resourceType = 0;

    MapUpdateEventInfo mapUpdateEventInfo = new MapUpdateEventInfo();

    // Start is called before the first frame update
    void Start()
    {
        EventController.getInstance().RegisterListener<MapUpdateEventInfo>(UpdatePrice);
        ButtonText.text = GameController.GetInstance().DiscoverGoldCost.ToString() + "G";

        if (isDiscovered)
        {
            DisableButton();
        }
    }

    public void UpdatePrice(MapUpdateEventInfo eventInfo)
    {
        ButtonText.text = GameController.GetInstance().DiscoverGoldCost.ToString() + "G";
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
        if (GameController.GetInstance().changeGold(-GameController.GetInstance().GetDiscoverCost(), GameController.GetInstance().GetDiscoverCost()))
        {
            GameController.GetInstance().DiscoverGoldCost = (int) (GameController.GetInstance().DiscoverGoldCost * 1.5f);
            isDiscovered = true;
            EventController.getInstance().OnDiscover();
            DisableButton();
        }
        
    }

    public void DisableButton()
    {
        DiscoverButtonObject.SetActive(false);
    }
}
