using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    //Singleton Instance for Buildings data.
    private static BuildingController mInstance;

    //What buildings you can build.
    private List<Building> mAvailableBuildings;

    //What buildings you own.
    private List<Building> mOwnedBuildings;

    //The building currently selected.
    private Building mTagSelected = null;

    //Singleton Constructor.
    public static BuildingController GetInstance()
    {

        if (mInstance == null)
        {
            GameObject go = new GameObject();
            mInstance = go.AddComponent<BuildingController>();
        }
        return mInstance;

    }
    //TODO uncomment this line
    private BuildingController()
    {
        //mAvailableBuildings = BUILDING_REGISTRY.GET_BUILDINGS();

        mOwnedBuildings = new List<Building>();
    }
    
    //Gets the selected building (for populating the UI information panel)
    public Building GetSelectedBuilding()
    {
        return mTagSelected;
    }

    //Calculates the population cap increased from building bonuses.
    public int getPopulationCapIncrease()
    {
        int temp = 0;
        foreach (Building building in mOwnedBuildings)
        {
            if (building.mPopCapType == "people")
                temp += building.mPopulationCapIncrease;
        }
        return temp;
    }

    public int getWorkerPopCap(string type)
    {
        int temp = 0;
        foreach (Building building in mOwnedBuildings)
        {
            if (building.mPopCapType == type)
                temp += building.mPopulationCapIncrease;
        }
        return temp;
    }

    //Event from clicking the build button, in the building tab.
    
    //TODO Move to UIController
    /*
    public static void BuildButton_Click(object sender, EventArgs e)
    {
        Building building = GetInstance().mTagSelected;
        if (building == null)
        {
            return;
        }
        else
        {
            if (GetInstance().getBuildings().Contains(building))
            {
                bool canBuild = true;

                foreach (string key in building.mResourceCosts.Keys)
                {
                    if (GameController.GetInstance().mResources[key].getCount() < building.mResourceCosts[key])
                    {
                        canBuild = false;
                    }
                }

                if (canBuild)
                {
                    if (!GameController.GetInstance().changeGold(-building.mGoldCost, building.mGoldCost))
                    {
                        return;
                    }
                    foreach (string key in building.mResourceCosts.Keys)
                    {
                        GameController.GetInstance().mResources[key].modifyCountCond(-building.mResourceCosts[key], building.mResourceCosts[key]);
                    }
                    GetInstance().getBuildingsOwned().Add(building);
                    building.mEvent?.Invoke();
                    if (!building.mIsRepeatable)
                        GetInstance().getBuildings().Remove(building);
                    GetInstance().mTagSelected = null;
                    GameManager.RefreshBuildingList();
                    Program.MainForm.UpdateList();
                }
            }
        }
    }
    */
    
    //Gets the available buildings, for the building selector UI.
    public List<Building> getBuildings()
    {
        return mAvailableBuildings;
    }

    //Gets the owned buildings
    //TODO implement a way to see what buildings you have.
    public List<Building> getBuildingsOwned()
    {
        return mOwnedBuildings;
    }

    
}

//Building class, for holding what bonuses you get, costs, and other misc information.
public class Building
{
    //Building Name
    public string mName;

    //Building SubText
    public string mSubText;

    //Image that is used for the buildings.
    public int mImageID;

    //Gold needed for the construction of a building.
    public int mGoldCost;

    //What resources are needed to construct the building.
    public Dictionary<string, int> mResourceCosts;

    //The event that gets called once you construct a building.
    //Used for unlocking UI mostly.
    //public Func<int> mEvent = null;

    //If the building can be built more than once.
    public bool mIsRepeatable;

    //The description that shows in the UI.
    public string mDescription = "";

    //The type of population this building will increase the capacity for.
    public string mPopCapType = "";

    //Bonuses
    //How much the population capacity increases for the type of building.
    public int mPopulationCapIncrease = 0;

    //Building Constructor
    public Building(string name, string subText, int imageID, Dictionary<string, int> resourceCosts, bool isRepeatable = false, int goldCost = 0)
    {
        mName = name;
        mSubText = subText;
        mImageID = imageID;
        mGoldCost = goldCost;
        mResourceCosts = resourceCosts;
        mIsRepeatable = isRepeatable;
    }

    //Only used for special buildings that have an update action.
    public void OnUpdate()
    {

    }
}
//TODO
//Uncomment this section
/*
//Building Database
public static class BUILDING_REGISTRY
{
    private static List<Building> BUILDINGS = new List<Building>();

    public static Building BlackSmith;
    public static Building Market;
    public static Building SawMill;
    public static Building StoneCutter;
    public static Building Hut;
    public static Building TinMine;
    public static Building CoalMine;
    public static Building IronMine;
    public static Building ExtraCopperMine;
    public static Building ExtraTinMine;
    public static Building ExtraCoalMine;
    public static Building ExtraIronMine;

    public static Building CopperForge;
    public static Building TinForge;
    public static Building IronForge;
    public static Building SteelForge;

    public static List<Building> GET_BUILDINGS()
    {
        return BUILDINGS;
    }

    public static void REGISTER(Building building)
    {
        BUILDINGS.Add(building);
    }

    public static void REGISTER_ALL()
    {
        //Creates the Black Smith Building Instance;
        Dictionary<string, int> bsResourceCosts = new Dictionary<string, int>();
        bsResourceCosts["Stone Slab"] = 30;

        BlackSmith = new Building("Black Smith", "Ingots -> Products", 0, bsResourceCosts);
        BlackSmith.mDescription = "Unlocks the Black Smith tab. This is used to create products using the metal that you acquire.";
        BlackSmith.mEvent = Events.UnlockSmithing;
        REGISTER(BlackSmith);

        //Creates the Market Building Instance;
        Dictionary<string, int> mResourceCosts = new Dictionary<string, int>();
        mResourceCosts["Stone Slab"] = 100;

        Market = new Building("Market", "Adds Merchants", 9, mResourceCosts, false, 100);
        Market.mDescription = "Unlocks the Market tab. This is where you can buy merchants and choose what is auto-sold.";
        Market.mEvent = Events.UnlockMarket;
        REGISTER(Market);

        //Creates the Saw Mill Building Instance;
        Dictionary<string, int> swResourceCosts = new Dictionary<string, int>();
        swResourceCosts["Stone Slab"] = 300;
        swResourceCosts["Iron Ingot"] = 50;

        SawMill = new Building("Saw Mill", "Wood -> Planks", 1, swResourceCosts);
        SawMill.mDescription = "Unlocks a new section under the Forestry tab, allows you to turn wood into planks.";
        //SawMill.mEvent = Events.UnlockTinMine;
        REGISTER(SawMill);

        //Creates the Stone Cutter Building Instance;
        Dictionary<string, int> scResourceCosts = new Dictionary<string, int>();
        scResourceCosts["Stone Slab"] = 30;
        scResourceCosts["Iron Ingot"] = 5;

        StoneCutter = new Building("Stone Cutter", "Stone -> Bricks", 2, scResourceCosts);
        StoneCutter.mDescription = "Does nothing, because heck off.";

        REGISTER(StoneCutter);

        //Creates the Hut Building Instance;
        Dictionary<string, int> hResourceCosts = new Dictionary<string, int>();
        hResourceCosts["Stone Slab"] = 30;

        Hut = new Building("Hut", "Houses People", 3, hResourceCosts, true);
        Hut.mDescription = "Adds 10 to your population capacity.";
        Hut.mPopCapType = "people";
        Hut.mPopulationCapIncrease = 10;
        REGISTER(Hut);

        //Creates the Tin Mine Building Instance;
        Dictionary<string, int> tinMineResourceCosts = new Dictionary<string, int>();
        hResourceCosts["Stone Slab"] = 100;

        TinMine = new Building("Tin Mine", "Unlocks Tin", 17, tinMineResourceCosts);
        TinMine.mDescription = "Gives you the ability to have miners work in the tin mine, and for you to manualy mine tin.";
        TinMine.mEvent = Events.UnlockTinMine;
        REGISTER(TinMine);

        //Creates the Coal Mine Building Instance;
        Dictionary<string, int> coalMineResourceCosts = new Dictionary<string, int>();
        hResourceCosts["Stone Slab"] = 100;

        CoalMine = new Building("Coal Mine", "Unlocks Coal", 14, coalMineResourceCosts);
        CoalMine.mDescription = "Gives you the ability to have miners work in the coal mine, and for you to manualy mine tin.";
        CoalMine.mEvent = Events.UnlockCoalMine;
        REGISTER(CoalMine);

        //Creates the Iron Mine Building Instance;
        Dictionary<string, int> ironMineResourceCosts = new Dictionary<string, int>();
        hResourceCosts["Stone Slab"] = 100;

        IronMine = new Building("Iron Mine", "Unlocks Iron", 16, ironMineResourceCosts);
        IronMine.mDescription = "Gives you the ability to have miners work in the iron mine, and for you to manualy mine iron.";
        IronMine.mEvent = Events.UnlockIronMine;
        REGISTER(IronMine);

        //Creates the Copper Mine Increase Building Instance;
        Dictionary<string, int> extraCopperMineResourceCosts = new Dictionary<string, int>();
        extraCopperMineResourceCosts["Stone Slab"] = 100;

        ExtraCopperMine = new Building("Extra Copper Mine", "+10 Capacity", 18, extraCopperMineResourceCosts, true);
        ExtraCopperMine.mDescription = "Adds 10 to the capacity to Copper Mines.";
        ExtraCopperMine.mPopCapType = "Copper Mine";
        ExtraCopperMine.mPopulationCapIncrease = 10;
        ExtraCopperMine.mEvent = Events.RecalculateWorkerCaps;
        REGISTER(ExtraCopperMine);

        //Creates the Tin Mine Increase Building Instance;
        Dictionary<string, int> extraTinMineResourceCosts = new Dictionary<string, int>();
        extraTinMineResourceCosts["Stone Slab"] = 100;

        ExtraTinMine = new Building("Extra Tin Mine", "+10 Capacity", 19, extraTinMineResourceCosts, true);
        ExtraTinMine.mDescription = "Adds 10 to the capacity to Tin Mines.";
        ExtraTinMine.mPopCapType = "Tin Mine";
        ExtraTinMine.mPopulationCapIncrease = 10;
        ExtraTinMine.mEvent = Events.RecalculateWorkerCaps;

        //Creates the Coal Mine Increase Building Instance;
        Dictionary<string, int> extraCoalMineResourceCosts = new Dictionary<string, int>();
        extraCoalMineResourceCosts["Stone Slab"] = 100;

        ExtraCoalMine = new Building("Extra Coal Mine", "+10 Capacity", 20, extraCoalMineResourceCosts, true);
        ExtraCoalMine.mDescription = "Adds 10 to the capacity to Coal Mines.";
        ExtraCoalMine.mPopCapType = "Coal Mine";
        ExtraCoalMine.mPopulationCapIncrease = 10;
        ExtraCoalMine.mEvent = Events.RecalculateWorkerCaps;

        //Creates the Iron Mine Increase Building Instance;
        Dictionary<string, int> extraIronMineResourceCosts = new Dictionary<string, int>();
        extraIronMineResourceCosts["Stone Slab"] = 100;

        ExtraIronMine = new Building("Extra Iron Mine", "+10 Capacity", 21, extraIronMineResourceCosts, true);
        ExtraIronMine.mDescription = "Adds 10 to the capacity to Iron Mines.";
        ExtraIronMine.mPopCapType = "Iron Mine";
        ExtraIronMine.mPopulationCapIncrease = 10;
        ExtraIronMine.mEvent = Events.RecalculateWorkerCaps;

        //Creates the Copper Forge Building Instance;
        Dictionary<string, int> copperForgeResourceCosts = new Dictionary<string, int>();
        copperForgeResourceCosts["Stone Slab"] = 100;

        CopperForge = new Building("Copper Forge", "+Copper Ingots", 21, copperForgeResourceCosts);
        CopperForge.mDescription = "Unlocks the ability to smelt Copper Ingots.";
        CopperForge.mEvent = Events.UnlockCopperForge;
        REGISTER(CopperForge);

        //Creates the Tin Forge Building Instance;
        Dictionary<string, int> tinForgeResourceCosts = new Dictionary<string, int>();
        tinForgeResourceCosts["Stone Slab"] = 100;

        TinForge = new Building("Tin Forge", "+Tin Ingots", 21, tinForgeResourceCosts);
        TinForge.mDescription = "Unlocks the ability to smelt Tin Ingots.";
        TinForge.mEvent = Events.UnlockTinForge;

        //Creates the Iron Forge Building Instance;
        Dictionary<string, int> ironForgeResourceCosts = new Dictionary<string, int>();
        ironForgeResourceCosts["Stone Slab"] = 100;

        IronForge = new Building("Iron Forge", "+Iron Ingots", 21, ironForgeResourceCosts);
        IronForge.mDescription = "Unlocks the ability to smelt Iron Ingots.";
        IronForge.mEvent = Events.UnlockIronForge;
    }
}


*/