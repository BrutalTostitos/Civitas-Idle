﻿using System;
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

    
    public System.Random mRandom = new System.Random();

    int amount;

    #region connecting UI things
    public Button forageButton;
    public FarmPlot farmPlotPrefab;

    #endregion

    public FarmingController()
    {
        int amount = 1;
        mFarmPlots = new List<FarmPlot>();
        mFarmingSeeds = new Dictionary<string, Seeds>();

        //SEEDS
        mFarmingSeeds["Corn"] = new Seeds(5, SeedType.Corn);
        mFarmingSeeds["Potato"] = new Seeds(5, SeedType.Potato);
        mFarmingSeeds["Wheat"] = new Seeds(5, SeedType.Wheat);
        mFarmingSeeds["Hops"] = new Seeds(5, SeedType.Hops);

        #region Init farm plots

        for (int i = 0; i < 8; i++)
        {

            FarmPlot farmPlot = Instantiate(farmPlotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            //transform.SetParent(parent, false);
            //TODO set the parent as well
        }


        #endregion


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







