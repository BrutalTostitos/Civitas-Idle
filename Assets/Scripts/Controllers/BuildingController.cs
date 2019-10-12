using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class BuildingController : MonoBehaviour
{
    //Singleton Instance for Buildings data.
    private static BuildingController mInstance;

    //What buildings you can build.
    public List<BuildingObject> mAvailableBuildings;
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
        mOwnedBuildings = new List<BuildingObject>();

        InfoPanel.building = null;
        if (buildingsContentPanel == null)
            buildingsContentPanel = GameObject.Find("BuildingAvailablePanel");
        
        mBuidlingIcons = new Dictionary<BuildingObject, GameObject>();

        int x = 0;
        int y = 0;
        foreach (BuildingObject building in mAvailableBuildings)
        {
            if (building.Prereq == null)
            {
            }
            else
            {
                bool test = false;
                foreach (BuildingObject bo in building.Prereq)
                {
                    if (!mOwnedBuildings.Contains(bo))
                    {
                        test = true;
                    }
                }
                if (test)
                {
                    continue;
                }
            }
            //the world building looks weird if you stare at it too much
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

        BuildingController.SetInstance(this);
        InfoPanel.SetSelected(null);
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
            if (building.Prereq == null)
            {

            }
            else
            {
                bool test = false;
                foreach (BuildingObject bo in building.Prereq)
                {
                    if (!mOwnedBuildings.Contains(bo))
                    {
                        test = true;
                    }
                }
                if (test)
                {
                    continue;
                }
            }
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

                InfoPanel.building.OnBuy();

                RebuildOptions();
                
                InfoPanel.building = null;
                InfoPanel.SetSelected(null);
            }
        }
    }

    //Used for loading the save.
    private void SimulateBuildingPurchases(List<string> name)
    {
        BuildingObject[] boArray = new BuildingObject[getBuildings().Count];
        getBuildings().CopyTo(boArray);
        foreach(BuildingObject bo in boArray)
        {
             if (name.Contains(bo.name))
             {
                 mAvailableBuildings.Remove(bo);
                 mOwnedBuildings.Add(bo);

                 bo.OnBuy();
             }
        }

        RebuildOptions();
    }

    private BuildingSave CreateSaveGameObject()
    {
        BuildingSave save = new BuildingSave();
        foreach (BuildingObject bo in mOwnedBuildings)
        {
            //uses name of the object, not the one assigned inside the scriptable object, just so that if we have any objects displaying the same name, it wont overlap.
            save.ownedBuildings.Add(bo.name);
        }

        return save;
    }

    public void SaveGame(string saveName)
    {
        BuildingSave save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + saveName + "/BuildingSave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Saved buildings...");
    }

    public void LoadGame(string loadName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + loadName + "/BuildingSave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + loadName + "/BuildingSave.save", FileMode.Open);
            BuildingSave save = (BuildingSave)bf.Deserialize(file);
            file.Close();

            SimulateBuildingPurchases(save.ownedBuildings);
        }
        else
        {
            Debug.Log("No Building Save Found");
        }
    }
}
