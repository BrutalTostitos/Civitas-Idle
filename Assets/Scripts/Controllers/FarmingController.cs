using EventCallBacks;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static Seeds;

class FarmingController : MonoBehaviour
{
    //PRIVATE
    private static FarmingController mInstance;
    FarmingUpdateEventInfo fuei = new FarmingUpdateEventInfo();
    






    //Holds the current coords to spawn a new farm plot
    Transform FarmPlotParent;
    private float plotSpawnX = -605;   //probably be 220
    private float plotSpawnY = 84;   //500




    //PUBLIC
    public List<FarmPlot> mFarmPlots;           //Holds a reference to all farm plots
    public Dictionary<string, Seeds> mFarmingSeeds; //why is this a thing



    public System.Random mRandom = new System.Random();

    int amount;

    #region connecting UI things
    public Button forageButton;
    public FarmPlot farmPlotPrefab;

    #endregion

    //Cant use constructor to instantiate objects. Using awake() instead
    public void Awake()
    {
        mInstance = this;
        //Event system setup
        fuei.eventGo = gameObject;
        int amount = 1;
        mFarmPlots = new List<FarmPlot>();
        mFarmingSeeds = new Dictionary<string, Seeds>();

        //Assigning reference for the parent obejct for all the farm plots
        FarmPlotParent = transform.Find("/MainCanvas/FarmingCanvas/FarmingBackgroundPanel");







        #region Init farm plots

        for (int i = 0; i < 10; i++)
        {
            FarmPlot tmp = Instantiate(farmPlotPrefab);
            tmp.transform.SetParent(FarmPlotParent, true);      //gets rescaled when we set the parent?
            tmp.transform.localScale = new Vector3(1, 1, 1);    //resetting scale
            float width = tmp.GetComponent<RectTransform>().sizeDelta.x;    //getting our width
            float height = tmp.GetComponent<RectTransform>().sizeDelta.y;
            tmp.transform.localPosition = new Vector3(plotSpawnX + ((width + 20) * (i%5)), //20 is our buffer
                plotSpawnY + -(((height + 20) * (int)(i / 5))));                           //20 is our buffer
            
        }
        #endregion
        //SEEDS 
        mFarmingSeeds["Corn"] = new Seeds(5, SeedType.Corn);
        mFarmingSeeds["Potato"] = new Seeds(5, SeedType.Potato);
        mFarmingSeeds["Wheat"] = new Seeds(5, SeedType.Wheat);
        mFarmingSeeds["Hops"] = new Seeds(5, SeedType.Hops);
    }

    private void Update()
    {
        Debug.Log(mFarmingSeeds["Corn"].getCount());
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
        int test = UnityEngine.Random.Range(0, 4);
        Debug.Log(test);
        switch (test)
        {
            case 0:
                mFarmingSeeds["Corn"].mCount++;// modifyCountCond(amount, 0);
                break;
            case 1:
                mFarmingSeeds["Wheat"].mCount++; //mFarmingSeeds["Potato"].modifyCountCond(amount, 0);
                break;
            case 2:
                mFarmingSeeds["Potato"].mCount++; //mFarmingSeeds["Wheat"].modifyCountCond(amount, 0);
                break;
            case 3:
                mFarmingSeeds["Hops"].mCount++; //mFarmingSeeds["Hops"].modifyCountCond(amount, 0);
                break;

            default:
                break;
        }
        EventController.getInstance().FireEvent(fuei);
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







