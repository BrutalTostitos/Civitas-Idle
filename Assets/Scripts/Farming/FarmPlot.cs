using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmPlot : MonoBehaviour
{

    public Button tillButton;
    public ProgressBar tillProgressBar;     //horizontal bar
	public ProgressBar overgrowthProgressBar;
	public Button overGrowthButton;
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

	public bool isTilled;
	public float tillProgress;// = 0.0f;
	public float tillProgressCap;// = 5.0f;

	public float overGrownProgress;	//Monitors the current state of the field being over grown
	public float overGrownCap;		//The assigned value to signify the field is completely overgrown
	

    public int ID = -1;

    private void Awake()
    {
		#region Progress Bar init
		tillProgress = 0.0f;
		tillProgressCap = 5.0f;
		overGrownProgress = 0.0f;
		overGrownCap = 5.0f;

		tillProgressBar.current = tillProgress;
		tillProgressBar.maximum = tillProgressCap;
		overgrowthProgressBar.current = overGrownProgress;          
		overgrowthProgressBar.maximum = overGrownCap;
		#endregion
		isTilled = false;
		mButtonList.Add(cornButton);
        mButtonList.Add(wheatButton);
        mButtonList.Add(potatoButton);
        mButtonList.Add(hopsButton);
        cornButton.gameObject.SetActive(false);
        wheatButton.gameObject.SetActive(false);
        potatoButton.gameObject.SetActive(false);
        hopsButton.gameObject.SetActive(false);
        harvestProgressBar.gameObject.SetActive(false);
		overgrowthProgressBar.gameObject.SetActive(false);
		
		//adding click events
        tillButton.onClick.AddListener(() => TillField());			
		overGrowthButton.onClick.AddListener(() => WeedField());	

    }
    private void Update()
    {
		UpdateFieldStatus();
        
    }
	public void UpdateFieldStatus()
	{
		if (mSeed.mType != Seeds.SEED_TYPE.None)
		{
			//Checking to see if our field is overgrown. If not, the seeds will continue to grow
			if (overGrownProgress < overGrownCap)
			{
				//Plot specific
				overGrownProgress += Time.deltaTime;
				overgrowthProgressBar.current = overGrownProgress;
				//Seed specific
				mSeed.mHarvestTime += Time.deltaTime;
				harvestProgressBar.current = mSeed.mHarvestTime;
			}



			if (mSeed.mHarvestTime >= mSeed.mHarvestTimeCap)
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
			isTilled = true;
            ToggleButtonsTilled();
        }
        tillProgressBar.current = tillProgress;
    }
	public void WeedField(float amount = 1.0f)
	{
		overGrownProgress -= amount;
		if (overGrownProgress < 0)
		{
			overGrownProgress = 0.0f;
		}
	}
	public void HarvestCrops()
    {
        //Determining our harvest and updating farmingcontrollers seed count
        switch(mSeed.mType)
        {
            case Seeds.SEED_TYPE.Corn:
                FarmingController.GetInstance().mFarmingSeeds["Corn"].modifyCountCond(mSeed.mHarvestYield, 0);
                break;
            case Seeds.SEED_TYPE.Wheat:
                FarmingController.GetInstance().mFarmingSeeds["Wheat"].modifyCountCond(mSeed.mHarvestYield, 0);
                break;
            case Seeds.SEED_TYPE.Potato:
                FarmingController.GetInstance().mFarmingSeeds["Potato"].modifyCountCond(mSeed.mHarvestYield, 0);
                break;
            case Seeds.SEED_TYPE.Hops:
                FarmingController.GetInstance().mFarmingSeeds["Hops"].modifyCountCond(mSeed.mHarvestYield, 0);
                break;

        }


		//Resetting the plot
		mSeed.mType = Seeds.SEED_TYPE.None;
		overGrownProgress = 0.0f;
		gameObject.GetComponent<Image>().sprite = null;
		isTilled = false;
        ToggleButtonsHarvested();


		//Sending notification to farmcontroller that we need an update
		FarmingController.GetInstance().totalSeededPlots--;
        FarmingController.GetInstance().UpdateMeDaddy();
        FarmingController.GetInstance().farmingDetailScript.ClearPlot(ID);
    }
    

    public void PlantSeedPlot(Seeds seedType)
    {
        if (mSeed.mType != Seeds.SEED_TYPE.None)
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
        harvestProgressBar.maximum = mSeed.mHarvestTimeCap;
        ToggleButtonsPlanted();
		FarmingController.GetInstance().totalSeededPlots++;
    }
    //BUTTON TOGGLING STUFF. THERE IS A SMARTER WAY OF DOING THIS
    //TODO - cleanup and optimization

    //Toggles all buttons to their appropriate active state on tilled
    public void ToggleButtonsTilled()
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
		overgrowthProgressBar.gameObject.SetActive(!overgrowthProgressBar.gameObject.activeSelf);
	}
    private void ToggleButtonsHarvested()
    {

        tillButton.gameObject.SetActive(!tillButton.gameObject.activeSelf);
        tillProgressBar.gameObject.SetActive(!tillProgressBar.gameObject.activeSelf);
        harvestProgressBar.gameObject.SetActive(!harvestProgressBar.gameObject.activeSelf);
		overgrowthProgressBar.gameObject.SetActive(!overgrowthProgressBar.gameObject.activeSelf);
    }
    
}
