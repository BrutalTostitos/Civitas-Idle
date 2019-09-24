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
            return Description;
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

    public void OnBuy()
    {
        GameObject.Find("EventController").SendMessage(OnBuyFunction);
    }
}
