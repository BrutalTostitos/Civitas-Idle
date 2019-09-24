using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    //Singleton Instance for Buildings data.
    private static BuildingController mInstance;

    //What buildings you can build.
    private List<BuildingObject> mAvailableBuildings;

    //What buildings you own.
    private List<BuildingObject> mOwnedBuildings;

    //The building currently selected.
    private BuildingObject mTagSelected = null;

    private BuildingInformationScript InfoPanel;

    private GameObject buildingsContentPanel;
    public GameObject prefabGameIcon;

    void Awake()
    {
        buildingsContentPanel = GameObject.Find("BuildingAvailablePanel");
        
        mAvailableBuildings = new List<BuildingObject>();

        BuildingObject[] buildingsFromResources = Resources.FindObjectsOfTypeAll<BuildingObject>();
        mAvailableBuildings.AddRange(buildingsFromResources);

        int x = 0;
        int y = 0;
        foreach (BuildingObject building in mAvailableBuildings)
        {
            Debug.Log(building.name);
            GameObject temp = Instantiate(prefabGameIcon, buildingsContentPanel.transform);
            temp.transform.Translate(x*150, -y*150, 0);
            temp.GetComponent<BuildingIconButton>().buildingObject = building;
            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }
        }

        BuildingController.SetInstance(this);
    }

    public static void SetInstance(BuildingController buildingController)
    {
        mInstance = buildingController;
    }

    //Singleton Constructor.
    public static BuildingController GetInstance()
    {
        if (mInstance == null)
        {
            GameObject tmp;
            tmp = GameObject.Find("BuildingController");
            if (tmp == null)
            {
                GameObject go = new GameObject();
                mInstance = go.AddComponent<BuildingController>();
            }
            else
            {
                BuildingController tmp2;
                tmp2 = tmp.GetComponent<BuildingController>();
                if (tmp2 = null)
                {
                    tmp.AddComponent<BuildingController>();
                }
                else
                    mInstance = tmp2;
            }
        }
        return mInstance;

    }
    //TODO uncomment this line
    private BuildingController()
    {
        mOwnedBuildings = new List<BuildingObject>();
    }
    
    //Gets the selected building (for populating the UI information panel)
    public BuildingObject GetSelectedBuilding()
    {
        return mTagSelected;
    }

    //Calculates the population cap increased from building bonuses.
    public int getPopulationCapIncrease()
    {
        int temp = 0;
        foreach (BuildingObject building in mOwnedBuildings)
        {
            temp += building.PopulationIncrease;
        }
        return temp;
    }

    //Gets the available buildings, for the building selector UI.
    public List<BuildingObject> getBuildings()
    {
        return mAvailableBuildings;
    }

    //Gets the owned buildings
    //TODO implement a way to see what buildings you have.
    public List<BuildingObject> getBuildingsOwned()
    {
        return mOwnedBuildings;
    }

    public void SelectBuilding(BuildingObject building)
    {
        InfoPanel.SetSelected(building);
    }

    public void SetInfoPanel(BuildingInformationScript buildingInformationScript)
    {
        InfoPanel = buildingInformationScript;
    }
}
