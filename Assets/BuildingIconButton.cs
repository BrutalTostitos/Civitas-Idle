using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingIconButton : MonoBehaviour
{
    public BuildingObject buildingObject;
    public Image image;
    public Text Name;

    // Start is called before the first frame update
    void Start()
    {
        if (buildingObject == null)
        {
            return;
        }

        Name.text = buildingObject.name;
        image.sprite = buildingObject.Icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        BuildingController.GetInstance().SelectBuilding(buildingObject);
    }
}
