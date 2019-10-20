using EventCallBacks;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static Seeds;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class FarmingController : MonoBehaviour
{
    //PRIVATE
    private static FarmingController mInstance;

	private float weedingPower = 0;



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
	private void Update()
	{
		AutomatedWeeding();
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

	private void AutomatedWeeding()
	{
		if (totalSeededPlots > 0)
		{
			float totalPower = ((float)weedingPower / (float)totalSeededPlots) * Time.deltaTime;
			foreach (FarmPlot plot in mFarmPlots)
			{

				plot.UpdateFieldStatus();
				if (plot.mSeed != null)
				{
					plot.WeedField(totalPower);
				}
			}
		}
	}

	//Event driven - Farmer update
	private void FarmingWorkerUpdate(FarmingWorkerEventInfo eventInfo)
	{
		weedingPower = eventInfo.workerPower;
	}
	//TODO fix double event system call
	//TODO actually use the seeds mFood value for food conversion
	//TODO only draining 1 of each type of resource
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

	//Saved Game
	private FarmingControllerSave CreateSaveGameObject()
	{
		FarmingControllerSave save = new FarmingControllerSave();
		//assign wariables
		save.FarmingSeedsKey.AddRange(mFarmingSeeds.Keys);
		save.FarmingSeedsValue.AddRange(mFarmingSeeds.Values);
		
		foreach (FarmPlot farm in mFarmPlots)
		{
			FarmPlotData tmp = new FarmPlotData();
			tmp.TillProgress = farm.tillProgress;
			tmp.TillProgressCap = farm.tillProgressCap;

			tmp.OverGrownProgress = farm.overGrownProgress;
			tmp.OverGrownCap = farm.overGrownCap;
			tmp.Seed = farm.mSeed;
			tmp.isTilled = farm.isTilled;
			save.FarmPlots.Add(tmp);
		}

		return save;

	}

	public void SaveGame(string saveName)
	{
		FarmingControllerSave save = CreateSaveGameObject();
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/" + saveName + "/FarmingControllerSave.save");
		bf.Serialize(file, save);
		file.Close();

		Debug.Log("Saved FarmingController...");
	}

	public void LoadGame(string loadName)
	{
		if (File.Exists(Application.persistentDataPath + "/" + loadName + "/FarmingControllerSave.save"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			System.IO.FileStream file = File.Open(Application.persistentDataPath + "/" + loadName + "/FarmingControllerSave.save", FileMode.Open);
			FarmingControllerSave save = (FarmingControllerSave)bf.Deserialize(file);
			file.Close();

			//Reassign wariables here
			for (int i = 0; i < (save.FarmingSeedsKey.Count); i++)
			{
				mFarmingSeeds[save.FarmingSeedsKey[i]] = save.FarmingSeedsValue[i];
			}
			//Farmplots
			for (int i = 0; i < mFarmPlots.Count; i++)
			{
				

				if (save.FarmPlots[i].Seed.mType != SEED_TYPE.None)
				{


					mFarmPlots[i].PlantSeedPlot(save.FarmPlots[i].Seed);
					mFarmPlots[i].ToggleButtonsTilled();
				}
				else if (save.FarmPlots[i].isTilled)
				{
					mFarmPlots[i].ToggleButtonsTilled();

				}


				mFarmPlots[i].tillProgressBar.current = mFarmPlots[i].tillProgress = save.FarmPlots[i].TillProgress;
				mFarmPlots[i].tillProgressBar.maximum = mFarmPlots[i].tillProgressCap = save.FarmPlots[i].TillProgressCap;

				mFarmPlots[i].overgrowthProgressBar.current = mFarmPlots[i].overGrownProgress = save.FarmPlots[i].OverGrownProgress;
				mFarmPlots[i].overgrowthProgressBar.maximum = mFarmPlots[i].overGrownCap = save.FarmPlots[i].OverGrownCap;

				mFarmPlots[i].harvestProgressBar.current = mFarmPlots[i].mSeed.mHarvestTime = save.FarmPlots[i].Seed.mHarvestTime;
				mFarmPlots[i].harvestProgressBar.maximum = mFarmPlots[i].mSeed.mHarvestTimeCap = save.FarmPlots[i].Seed.mHarvestTimeCap;

			}
		}
		else
		{
			Debug.Log("No FarmingController save found");
		}
	}



}







