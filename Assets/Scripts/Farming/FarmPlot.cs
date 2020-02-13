using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmPlot : MonoBehaviour
{

    public Button tillButton;
    public ProgressBar tillProgressBar;     //radial bar
	public ProgressBar harvestProgressBar;  //horizontal bar/mask
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

	

    public int ID = -1;

    private void Awake()
    {
		#region Progress Bar init
		tillProgress = 0.0f;
		tillProgressCap = 5.0f;

		tillProgressBar.current = tillProgress;
		tillProgressBar.maximum = tillProgressCap;
		#endregion
		isTilled = false;
        harvestProgressBar.gameObject.SetActive(false);
		
		//adding click events
        tillButton.onClick.AddListener(() => TillField());			

    }
    //I dont think this actually matters at all anymore
	//private void Update()
	//{
	//	UpdateFieldStatus();

	//}
	public void UpdateFieldStatus()
	{
		if (mSeed.mType != SEED_TYPE.None)
		{
			//Checking to see if our field is overgrown. If not, the seeds will continue to grow
			
			
			//Seed specific
			mSeed.mHarvestTime += Time.deltaTime * TalentBuffs.GetInstance().GetGrowthModSpeed(mSeed.mType);
			harvestProgressBar.current = mSeed.mHarvestTime;
			



			if (mSeed.mHarvestTime >= mSeed.mHarvestTimeCap)
			{
				HarvestCrops();
			}
		}
	}
    public void TillField(int amount = 1)
    {
        tillProgress += (int)(amount * TalentBuffs.GetInstance().TillModPower);
        
        if (tillProgress >= tillProgressCap)
        {
            tillProgress = 0.0f;
			isTilled = true;
            ToggleButtonsTilled();
        }
        tillProgressBar.current = tillProgress;
    }
	
	public void HarvestCrops()
    {
        //Determining our harvest and updating farmingcontrollers seed count
        int tempYield = (int)(mSeed.mHarvestYield * TalentBuffs.GetInstance().GetSeedModOutput(mSeed.mType));
        switch(mSeed.mType)
        {
            case SEED_TYPE.Corn:
                FarmingController.GetInstance().mFarmingSeeds["Corn"].modifyCountCond(tempYield, 0);
                break;
            case SEED_TYPE.Wheat:
                FarmingController.GetInstance().mFarmingSeeds["Wheat"].modifyCountCond(tempYield, 0);
                break;
            case SEED_TYPE.Potato:
                FarmingController.GetInstance().mFarmingSeeds["Potato"].modifyCountCond(tempYield, 0);
                break;
            case SEED_TYPE.Hops:
                FarmingController.GetInstance().mFarmingSeeds["Hops"].modifyCountCond(tempYield, 0);
                break;

        }


		//Resetting the plot
		mSeed.mType = SEED_TYPE.None;
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
        if (mSeed.mType != SEED_TYPE.None)
        {
            return;
        }
        mSeed = seedType.ShallowCopy();
        switch (seedType.mType)
        {
            //Updating UI
            case SEED_TYPE.Corn:
                //gameObject.sprite = cornSprite;
                gameObject.GetComponent<Image>().sprite = cornSprite;
                //gameObject.GetComponent<Image>().transform.localScale = new Vector3(.5f, .5f);
                break;
            case SEED_TYPE.Wheat:
                gameObject.GetComponent<Image>().sprite = wheatSprite;
                break;
            case SEED_TYPE.Potato:
                gameObject.GetComponent<Image>().sprite = potatoSprite;
                break;
            case SEED_TYPE.Hops:
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
	}
    private void ToggleButtonsHarvested()
    {

        tillButton.gameObject.SetActive(!tillButton.gameObject.activeSelf);
        tillProgressBar.gameObject.SetActive(!tillProgressBar.gameObject.activeSelf);
        harvestProgressBar.gameObject.SetActive(!harvestProgressBar.gameObject.activeSelf);
    }
    
}
