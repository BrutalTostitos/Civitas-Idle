using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInformationScript : MonoBehaviour
{
    //This script is used by the InfoPanel to display the building details
    

    public Text Name;
    public Text Description;
    public Image Icon;

    public Text GoldCost;
    public Text StoneCost;
    public Text BrickCost;
    public Text LumberCost;
    
    public BuildingObject building;

    public BuildingController buildingController;

    void Start()
    {
        buildingController.SetInfoPanel(this);
    }

    public void SetSelected(BuildingObject buildingObject)
    {
        building = buildingObject;
        if (buildingObject == null)
        {
            Name.text = "";
            Description.text = "";
            Icon.sprite = null;
            Icon.enabled = false;
            GoldCost.text = "0";
            StoneCost.text = "0";
            BrickCost.text = "0";
            LumberCost.text = "0";
            return;
        }
        Name.text = buildingObject.name;
        Description.text = buildingObject.Description;
        Icon.sprite = buildingObject.Icon;
        Icon.enabled = true;

        GoldCost.text = buildingObject.GoldCost.ToString();
        StoneCost.text = buildingObject.StoneCost.ToString();
        BrickCost.text = buildingObject.BrickCost.ToString();
        LumberCost.text = buildingObject.LumberCost.ToString();
    }
}
