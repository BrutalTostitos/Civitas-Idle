using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static Seeds;

class FarmingController : MonoBehaviour
{
    private static FarmingController mInstance;
    public List<FarmPlot> mFarmPlots;           //Holds a reference to all farm plots
    public Dictionary<string, Seeds> mFarmingSeeds; //why is this a thing
    //public List<Seeds> mSeeds;                  //May be depricated

    //Holds the current coords to spawn a new farm plot
    private float plotSpawnX = 220;   //probably be 220
    private float plotSpawnY = 500;   //500
    private int plotWidth = 355;
    private int plotHeight = 185;
    Transform FarmPlotParent; 


    public System.Random mRandom = new System.Random();

    int amount;

    #region connecting UI things
    public Button forageButton;
    public FarmPlot farmPlotPrefab;

    #endregion

    //Cant use constructor to instantiate objects. Using awake() instead
    public void Awake()
    {
        int amount = 1;
        mFarmPlots = new List<FarmPlot>();
        mFarmingSeeds = new Dictionary<string, Seeds>();

        //Assigning reference for the parent obejct for all the farm plots
        FarmPlotParent = transform.Find("/MainCanvas/FarmingCanvas/FarmingBackgroundPanel");

        #region Init farm plots

        for (int i = 0; i < 8; i++)
        {
            FarmPlot farmPlot = Instantiate(farmPlotPrefab, FarmPlotParent);
            farmPlot.transform.position = new Vector3(plotSpawnX * i, plotSpawnY, 0);


        //    private float plotSpawnX = 220;   //probably be 220
        //private float plotSpawnY = 500;   //500
        //private int plotWidth = 355;
        //private int plotHeight = 185;
        }
        #endregion
        //SEEDS 
        mFarmingSeeds["Corn"] = new Seeds(5, SeedType.Corn);
        mFarmingSeeds["Potato"] = new Seeds(5, SeedType.Potato);
        mFarmingSeeds["Wheat"] = new Seeds(5, SeedType.Wheat);
        mFarmingSeeds["Hops"] = new Seeds(5, SeedType.Hops);
    }

    //TODO update for unity
    public static FarmingController GetInstance()
    {
        if (mInstance == null)
        {
            mInstance = new FarmingController();
        }
        return mInstance;
    }
    public void Forage()
    {
        switch (mRandom.Next(0, 4))
        {

            case 0:
                GetInstance().mFarmingSeeds["Corn"].modifyCountCond(amount, 0);
                break;
            case 1:
                GetInstance().mFarmingSeeds["Potato"].modifyCountCond(amount, 0);
                break;
            case 2:
                GetInstance().mFarmingSeeds["Wheat"].modifyCountCond(amount, 0);
                break;
            case 3:
                GetInstance().mFarmingSeeds["Hops"].modifyCountCond(amount, 0);
                break;

            default:
                break;
        }
    }

    //Button crap
    private void WheatButtonClick(object sender, EventArgs e)
    {
        int count = 1; //hard coded for now

        if (mFarmingSeeds["Wheat"].modifyCountCond(-count, count)) //test if we have the resources
        {

            //TODO fix PlantSeeds(SeedType.Wheat);
        }
        //TODO send event to eventcontroller to update uicontroller
    }
    private void PotatoButtonClick(object sender, EventArgs e)
    {
        int count = 1; //hard coded for now

        if (mFarmingSeeds["Potato"].modifyCountCond(-count, count)) //test if we have the resources
        {
            //TODO fix PlantSeeds(SeedType.Potato);
        }
        //TODO send event to eventcontroller to update uicontroller
    }

    private void CornButtonClick(object sender, EventArgs e)
    {
        int count = 1; //hard coded for now

        if (mFarmingSeeds["Corn"].modifyCountCond(-count, count)) //test if we have the resources
        {
            //TODO fix PlantSeeds(SeedType.Corn);
        }
        //TODO send event to eventcontroller to update uicontroller
    }
    private void HopsButtonClick(object sender, EventArgs e)
    {
        int count = 1; //hard coded for now

        if (mFarmingSeeds["Hops"].modifyCountCond(-count, count)) //test if we have the resources
        {
            //TODO fix PlantSeeds(SeedType.Hops);
        }
        //TODO send event to eventcontroller to update uicontroller
    }



}







