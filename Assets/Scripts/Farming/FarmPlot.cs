using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmPlot : MonoBehaviour
{

    public Button tillButton;
    public ProgressBar tillProgressBar;     //horizontal bar
    public ProgressBar harvestProgressBar;  //Radial bar
    public Button cornButton;
    public Button wheatButton;
    public Button potatoButton;
    public Button hopsButton;
    public List<Button> mButtonList;
    //public GameObject go;   //maybe not needed
    public Seeds mSeed = null;
    public Sprite cornSprite;
    public Sprite wheatSprite;
    public Sprite potatoSprite;
    public Sprite hopsSprite;

    
    public float tillProgress = 0.0f;
    public float tillProgressCap = 10.0f;

    public int ID = -1;

    private void Awake()
    {
        mButtonList.Add(cornButton);
        mButtonList.Add(wheatButton);
        mButtonList.Add(potatoButton);
        mButtonList.Add(hopsButton);
        cornButton.gameObject.SetActive(false);
        wheatButton.gameObject.SetActive(false);
        potatoButton.gameObject.SetActive(false);
        hopsButton.gameObject.SetActive(false);
        harvestProgressBar.gameObject.SetActive(false);
        tillButton.onClick.AddListener(() => TillField());  //adding the click event
        

    }
    private void Update()
    {
        //mSeed member variables can be accessed once planted & assigned, why is it still null?
        if (mSeed != null)
        {
            //Debug.Log(mSeed.harvestTime);
            mSeed.harvestTime += Time.deltaTime;
            harvestProgressBar.current = (int)mSeed.harvestTime;
            if(mSeed.harvestTime >= mSeed.harvestTimeCap)
            {
                HarvestCrops();
            }
        }
    }
    public void TillField(int amount = 1)
    {
        tillProgress += amount;
        
        if (tillProgress >= tillProgressCap)
        {
            tillProgress = 0.0f;
            ToggleButtonsTilled();
        }
        tillProgressBar.current = (int)tillProgress;
    }
    public void HarvestCrops()
    {
        //Determining our harvest and updating farmingcontrollers seed count
        switch(mSeed.mType)
        {
            case Seeds.SEED_TYPE.Corn:
                FarmingController.GetInstance().mFarmingSeeds["Corn"].modifyCountCond(mSeed.harvestYield, 0);
                break;
            case Seeds.SEED_TYPE.Wheat:
                FarmingController.GetInstance().mFarmingSeeds["Wheat"].modifyCountCond(mSeed.harvestYield, 0);
                break;
            case Seeds.SEED_TYPE.Potato:
                FarmingController.GetInstance().mFarmingSeeds["Potato"].modifyCountCond(mSeed.harvestYield, 0);
                break;
            case Seeds.SEED_TYPE.Hops:
                FarmingController.GetInstance().mFarmingSeeds["Hops"].modifyCountCond(mSeed.harvestYield, 0);
                break;

        }
        
        
        //Resetting the plot
        mSeed = null;
        gameObject.GetComponent<Image>().sprite = null;
        ToggleButtonsHarvested();


        //Sending notification to farmcontroller that we need an update
        FarmingController.GetInstance().UpdateMeDaddy();
        FarmingController.GetInstance().farmingDetailScript.ClearPlot(ID);
    }
    

    public void PlantSeedPlot(Seeds seedType)
    {
        if (mSeed != null)
        {
            return;
        }
        mSeed = seedType.ShallowCopy();
        switch (seedType.mType)
        {
            //Updating UI
            case Seeds.SEED_TYPE.Corn:
                //gameObject.sprite = cornSprite;
                gameObject.GetComponent<Image>().sprite = cornSprite;
                //gameObject.GetComponent<Image>().transform.localScale = new Vector3(.5f, .5f);
                break;
            case Seeds.SEED_TYPE.Wheat:
                gameObject.GetComponent<Image>().sprite = wheatSprite;
                break;
            case Seeds.SEED_TYPE.Potato:
                gameObject.GetComponent<Image>().sprite = potatoSprite;
                break;
            case Seeds.SEED_TYPE.Hops:
                gameObject.GetComponent<Image>().sprite = hopsSprite;
                break;
        }
        harvestProgressBar.maximum = (int)mSeed.harvestTimeCap;
        ToggleButtonsPlanted();
        Debug.Log("Planted seed of type" + mSeed.mType);
    }
    //BUTTON TOGGLING STUFF. THERE IS A SMARTER WAY OF DOING THIS
    //TODO - cleanup and optimization

    //Toggles all buttons to their appropriate active state on tilled
    private void ToggleButtonsTilled()
    {
        foreach (Button tmp in mButtonList)
        {
            tmp.gameObject.SetActive(!tmp.gameObject.activeSelf);                           //Toggles plant
        }
        tillButton.gameObject.SetActive(!tillButton.gameObject.activeSelf);                 //Toggles till button
        tillProgressBar.gameObject.SetActive(!tillProgressBar.gameObject.activeSelf);       //toggles till progress bar
    }

    private void ToggleButtonsPlanted()
    {
        foreach (Button tmp in mButtonList)
        {
            tmp.gameObject.SetActive(!tmp.gameObject.activeSelf);
        }

        harvestProgressBar.gameObject.SetActive(!harvestProgressBar.gameObject.activeSelf);
    }
    private void ToggleButtonsHarvested()
    {

        tillButton.gameObject.SetActive(!tillButton.gameObject.activeSelf);
        tillProgressBar.gameObject.SetActive(!tillProgressBar.gameObject.activeSelf);
        harvestProgressBar.gameObject.SetActive(!harvestProgressBar.gameObject.activeSelf);
    }
    
}
