using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInformationScript : MonoBehaviour
{
    public Text Name;
    public Text Description;
    public Image Icon;
    

    void Awake()
    {
        BuildingController.GetInstance().SetInfoPanel(this);
    }

    public void SetSelected(BuildingObject buildingObject)
    {
        if (buildingObject == null)
        {
            Name.text = "";
            Description.text = "";
            Icon.sprite = null;
            Icon.enabled = false;
            return;
        }
        Name.text = buildingObject.name;
        Description.text = buildingObject.Description;
        Icon.sprite = buildingObject.Icon;
        Icon.enabled = true;
    }
}
