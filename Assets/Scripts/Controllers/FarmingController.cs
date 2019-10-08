using EventCallBacks;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static Seeds;

public class FarmingController : MonoBehaviour
{
    //PRIVATE
    private static FarmingController mInstance;
	//TODO figure out a way to not need both of these. cookworker event is having to call both
	//TODO figure out a way to not need both of these. cookworker event is having to call both
	//TODO figure out a way to not need both of these. cookworker event is having to call both
	UIFarmingUpdateEventInfo fuei = new UIFarmingUpdateEventInfo();
    UIResourceUpdateEventInfo ruei = new UIResourceUpdateEventInfo();		

    public Transform FarmingBackgroundPanel;        

    // For Visuals
    public FarmingDetailScript farmingDetailScript;



    //Holds the current coords to spawn a new farm plot
    Transform FarmPlotParent;
    private float plotSpawnX = -605;   //probably be 220
    private float plotSpawnY = 84;   //500




    //PUBLIC
    public List<FarmPlot> mFarmPlots;           //Holds a reference to all farm plots
	public Dictionary<string, Seeds> mFarmingSeeds; 



    public System.Random mRandom = new System.Random();

    int amount;

	public int totalSeededPlots = 0;		//Keeps track of how many plots are seeded - used for workers

	#region connecting UI things
	public Button forageButton;
    public FarmPlot farmPlotPrefab;

    #endregion

    //Cant use constructor to instantiate objects. Using awake() instead
    public void Awake()
    {
		#region Event setup
		fuei.eventGO = gameObject;
		ruei.eventGO = gameObject;
		//Listening for events
		EventController.getInstance().RegisterListener<FarmingWorkerEventInfo>(FarmingWorkerUpdate);
		EventController.getInstance().RegisterListener<CookingWorkerEventInfo>(CookWorkerUpdate);
		EventController.getInstance().RegisterListener<FarmPurchaseEventInfo>(FarmPurchaseUpdate);

		#endregion

		mInstance = this;
        mFarmPlots = new List<FarmPlot>();
        mFarmingSeeds = new Dictionary<string, Seeds>();









		#region Init farm plots

		for (int i = 0; i < 10; i++)
        {
            FarmPlot tmp = Instantiate(farmPlotPrefab, FarmingBackgroundPanel, true);
            //tmp.transform.SetParent(FarmPlotParent, true);      //gets rescaled when we set the parent?
            tmp.transform.localScale = new Vector3(1, 1, 1);    //resetting scale
            float width = tmp.GetComponent<RectTransform>().sizeDelta.x;    //getting our width
            float height = tmp.GetComponent<RectTransform>().sizeDelta.y;
            tmp.transform.localPosition = new Vector3(plotSpawnX + ((width + 20) * (i%5)), //20 is our buffer
                plotSpawnY + -((height + 80) * (int)(i / 5)));                           //20 is our buffer

            //Setting up button click events
            tmp.cornButton.onClick.AddListener(() => PlantSeed(tmp, "Corn"));
            tmp.wheatButton.onClick.AddListener(() => PlantSeed(tmp, "Wheat"));
            tmp.potatoButton.onClick.AddListener(() => PlantSeed(tmp, "Potato"));
            tmp.hopsButton.onClick.AddListener(() => PlantSeed(tmp, "Hops"));


            tmp.ID = i;
			//Disabling all but the first 2 farm plots. Later ones will be purchased later
			if (i > 1)
			{
				tmp.gameObject.SetActive(false);
			}
			mFarmPlots.Add(tmp);

		}
        #endregion
        //SEEDS 
        mFarmingSeeds["Corn"] = new Seeds(5, SEED_TYPE.Corn);
        mFarmingSeeds["Potato"] = new Seeds(5, SEED_TYPE.Potato);
        mFarmingSeeds["Wheat"] = new Seeds(5, SEED_TYPE.Wheat);
        mFarmingSeeds["Hops"] = new Seeds(5, SEED_TYPE.Hops);


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
    private void PlantSeed(FarmPlot plot, string seedName)
    {
        int count = 1;  //Hard coded for now
        if (mFarmingSeeds[seedName].modifyCountCond(-count, count))
        {
            //Seed planted!
            //Planting the appropriate seed on the farmplot
            plot.PlantSeedPlot(mFarmingSeeds[seedName]);   //This might mess with mseeds count
			#region Background Updates
			switch (seedName)
            {
                case "Corn":
                farmingDetailScript.SetPlant(plot.ID, 1);
                break;
                case "Wheat":
                farmingDetailScript.SetPlant(plot.ID, 0);
                break;
                case "Potato":
                farmingDetailScript.SetPlant(plot.ID, 2);
                break;
                case "Hops":
                farmingDetailScript.SetPlant(plot.ID, 3);
                break;
            }
			#endregion
		}
		EventController.getInstance().FireEvent(fuei);
    }
	//Event driven - Farmer update
	private void FarmingWorkerUpdate(FarmingWorkerEventInfo eventInfo)
	{

		float power = eventInfo.workerPower;
		if (totalSeededPlots > 0)
		{
			float totalPower = (float)power / (float)totalSeededPlots;
			foreach (FarmPlot plot in mFarmPlots)
			{
				if (plot.mSeed != null)
				{
					plot.WeedField(totalPower);
					Debug.Log(totalPower);
				}
			}
		}
		

	}
	//TODO fix double event system call
	//TODO actually use the seeds mFood value for food conversion
	//Event Driven 
	private void CookWorkerUpdate(CookingWorkerEventInfo eventInfo)
	{
		Debug.Log("Im in the event!");
		float power = eventInfo.workerPower;
		float count = 0.0f;
		float totalPower = 0.0f;


		//This feels so wasteful. May have a function that does this calculation when you check the
		//seed to be used, so we could just retrieve the value without having to calculate every time
		foreach(KeyValuePair<string, Seeds> seed in mFarmingSeeds)
		{
			if(seed.Value.mToBeCooked)
			{
				count++;
			}
		}
		totalPower = power / count;
		foreach(KeyValuePair<string, Seeds> seed in mFarmingSeeds)
		{
			if (seed.Value.getCount() >= eventInfo.mSeedsToUse & seed.Value.mToBeCooked)			//checking if we have enough and its to be cooked
			{
				Debug.Log("Success!");
				//deduct seeds
				seed.Value.modifyCountCond(-eventInfo.mSeedsToUse, 0);
				GameController.GetInstance().ChangeFoodAmount(totalPower);
			}
		}
		EventController.getInstance().FireEvent(ruei);
		EventController.getInstance().FireEvent(fuei);
	}

	//Event driven
	private void FarmPurchaseUpdate(FarmPurchaseEventInfo eventInfo)
	{
		foreach (FarmPlot farm in mFarmPlots)
		{
			if (farm.gameObject.activeSelf == false)
			{
				farm.gameObject.SetActive(true);
				break;
			}
				
		}
	}

	//breh..
	public void UpdateMeDaddy()
    {
        EventController.getInstance().FireEvent(fuei);
    }
    private void UpgradeCropYield(Seeds seedType)
    {
        //TODO
        //Use this to upgrade the seeds script harvest yield for the 
        //particular seed
    }

    



}







