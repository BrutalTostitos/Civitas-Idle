using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Building", menuName = "Building", order = 51)]
public class BuildingObject : ScriptableObject
{
    [SerializeField]
    private string mName;

    [SerializeField]
    private string description;

    [SerializeField]
    private string OnBuyFunction;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int populationIncrease;

    [SerializeField]
    private int goldCost;

    [SerializeField]
    private int stoneCost;

    [SerializeField]
    private int brickCost;

    [SerializeField]
    private int lumberCost;

    [SerializeField]
    private BuildingObject[] prereq;

    [SerializeField]
    private bool isMultiSprite;

    [SerializeField]
    private Sprite buildingTileSprite;
    
    [SerializeField]
    private Sprite buildingTileSpriteLower;
    
    [SerializeField]
    private Sprite buildingTileSpriteUpper;

    public bool IsMultiSprite
    {
        get
        {
            return isMultiSprite;
        }
    }

    public Sprite BuildingTileSprite
    {
        get
        {
            return buildingTileSprite;
        }
    }

    public Sprite BuildingTileSpriteLower
    {
        get
        {
            return buildingTileSpriteLower;
        }
    }

    public Sprite BuildingTileSpriteUpper
    {
        get
        {
            return buildingTileSpriteUpper;
        }
    }

    public string Name
    {
        get
        {
            return mName;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }
    }

    public int PopulationIncrease
    {
        get 
        {
            return populationIncrease;
        }
    }

    public int GoldCost
    {
        get
        {
            return goldCost;
        }
    }

    public int StoneCost
    {
        get
        {
            return stoneCost;
        }
    }

    public int BrickCost
    {
        get
        {
            return brickCost;
        }
    }

    public int LumberCost
    {
        get
        {
            return lumberCost;
        }
    }

    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }

    public BuildingObject[] Prereq
    {
        get
        {
            return prereq;
        }
    }

    public void OnBuy()
    {
        if (OnBuyFunction != "")
            GameObject.Find("EventController").SendMessage(OnBuyFunction);
    }
}
