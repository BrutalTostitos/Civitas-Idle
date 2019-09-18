using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Building", menuName = "Building", order = 51)]
public class BuildingObject : ScriptableObject
{
    [SerializeField]
    private string Name;

    [SerializeField]
    private string Description;

    [SerializeField]
    private string OnBuyFunction;

    [SerializeField]
    private Sprite Icon;

    [SerializeField]
    private int PopulationIncrease;

    [SerializeField]
    private int GoldCost;

    public void OnBuy()
    {
        GameObject.Find("EventController").SendMessage(OnBuyFunction);
    }
}
