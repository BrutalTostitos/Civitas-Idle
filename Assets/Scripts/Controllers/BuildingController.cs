using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingController : MonoBehaviour
{
    //Singleton Instance for Buildings data.
    private static BuildingController mInstance;

    //What buildings you can build.
    private List<BuildingObject> mAvailableBuildings;
    private Dictionary<BuildingObject, GameObject> mBuidlingIcons;

    //What buildings you own.
    private List<BuildingObject> mOwnedBuildings;

    //The building currently selected.
    private BuildingObject mTagSelected = null;

    public BuildingInformationScript InfoPanel;

    public GameObject buildingsContentPanel;
    public GameObject prefabGameIcon;
    

    void Awake()
    {
        if (buildingsContentPanel == null)
            buildingsContentPanel = GameObject.Find("BuildingAvailablePanel");
        
        mAvailableBuildings = new List<BuildingObject>();
        mBuidlingIcons = new Dictionary<BuildingObject, GameObject>();

        BuildingObject[] buildingsFromResources = Resources.FindObjectsOfTypeAll<BuildingObject>();
        mAvailableBuildings.AddRange(buildingsFromResources);

        int x = 0;
        int y = 0;
        foreach (BuildingObject building in mAvailableBuildings)
        {
            Debug.Log("this running yet");
            //the world building looks weird if you stare at it too much
            //For all the available buildings, we instantiate the icon on the left side
            Debug.Log(building.name);
            GameObject temp = Instantiate(prefabGameIcon, buildingsContentPanel.transform);
            temp.transform.Translate(x*150*0.016f, -y*150*0.016f, 0);

            //Attaching the corresponding building object with its icon
            temp.GetComponent<BuildingIconButton>().buildingObject = building;
            x++;
            //The smart way to lay out a grid
            if (x > 4)
            {
                x = 0;
                y++;
            }

            mBuidlingIcons[building] = temp;
        }

        BuildingController.SetInstance(this);
    }

    public void RebuildOptions()
    {
        foreach (KeyValuePair<BuildingObject,GameObject> go in mBuidlingIcons)
        {
            GameObject.Destroy(go.Value);
        }
        mBuidlingIcons = new Dictionary<BuildingObject, GameObject>();

        int x = 0;
        int y = 0;
        foreach (BuildingObject building in mAvailableBuildings)
        {
            //For all the available buildings, we instantiate the icon on the left side
            GameObject temp = Instantiate(prefabGameIcon, buildingsContentPanel.transform);
            temp.transform.Translate(x*150*0.016f, -y*150*0.016f, 0);

            //Attaching the corresponding building object with its icon
            temp.GetComponent<BuildingIconButton>().buildingObject = building;
            x++;
            //The smart way to lay out a grid
            if (x > 4)
            {
                x = 0;
                y++;
            }
            mBuidlingIcons[building] = temp;
        }
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

    //Function added by josh //Thanks dad
    public void PurchaseBuilding()
    {

        if (InfoPanel.building != null)
        {
            if (GameController.GetInstance().canBuy(InfoPanel.building))
            {
                GameController.GetInstance().useResourcesToBuyBuilding(InfoPanel.building);
                mAvailableBuildings.Remove(InfoPanel.building);
                

                mOwnedBuildings.Add(InfoPanel.building);

                RebuildOptions();
                
                InfoPanel.building = null;
            }
        }
    }
}
