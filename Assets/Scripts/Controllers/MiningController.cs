using EventCallBacks;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MiningController : MonoBehaviour
{
    
    private static MiningController mInstance;

	private int stonePower = 0;
	private int copperPower = 0;
	private int tinPower = 0;
	private int coalPower = 0;
	private int ironPower = 0;

	public System.Random mRandom = new System.Random();
    //Creating our event
    UIResourceUpdateEventInfo ruei = new UIResourceUpdateEventInfo();
    
    int amount = 1;
	//Reference to our Mines
	//This is our gross way to get of making a mineshaft class
	public GameObject CopperMineGO;
	public GameObject TinMineGO;
	public GameObject CoalMineGO;
	public GameObject IronMineGO;
	
	//assigned in inspector
	public ProgressBar StoneProgressBar;
	public ProgressBar CopperProgressBar;
	public ProgressBar TinProgressBar;
	public ProgressBar CoalProgressBar;
	public ProgressBar IronProgressBar;

	public float StoneProgress;
	public float StoneProgressCap;
	public float CopperProgress;
	public float CopperProgressCap;
	public float TinProgress;
	public float TinProgressCap;
	public float CoalProgress;
	public float CoalProgressCap;
	public float IronProgress;
	public float IronProgressCap;




	void Awake()
    {
        mInstance = this;

        #region EVENT SYSTEM SETUP
		//Listening for events
        EventController.getInstance().RegisterListener<MiningWorkerEventInfo>(MiningWorkerUpdate);
		EventController.getInstance().RegisterListener<CopperMinePurchaseEventInfo>(UnlockCopperMine);
		EventController.getInstance().RegisterListener<TinMinePurchaseEventInfo>(UnlockTinMine);
		EventController.getInstance().RegisterListener<CoalMinePurchaseEventInfo>(UnlockCoalMine);
		EventController.getInstance().RegisterListener<IronMinePurchaseEventInfo>(UnlockIronMine);
		ruei.eventGO = gameObject;
		#endregion


		#region Mine setup
		CopperMineGO.gameObject.SetActive(false);
		TinMineGO.gameObject.SetActive(false);
		CoalMineGO.gameObject.SetActive(false);
		IronMineGO.gameObject.SetActive(false);

		StoneProgress = StoneProgressBar.current = 0.0f;
		StoneProgressCap = StoneProgressBar.maximum = 10.0f;
		CopperProgress = CopperProgressBar.current = 0.0f;
		CopperProgressCap = CopperProgressBar.maximum = 20.0f;
		TinProgress = TinProgressBar.current = 0.0f;
		TinProgressCap = TinProgressBar.maximum = 50.0f;
		CoalProgress = CoalProgressBar.current = 0.0f;
		CoalProgressCap = CoalProgressBar.maximum  = 100.0f;
		IronProgress = IronProgressBar.current = 0.0f;
		IronProgressCap = IronProgressBar.maximum = 200.0f;
		#endregion
	}
	private void Update()
	{
		AutomatedMining();


		
	}
	public static MiningController GetInstance()
    {
        if (mInstance == null)
        {
            Debug.Log("ABORTMINING");
            GameObject go = new GameObject();
            mInstance = go.AddComponent<MiningController>();
        }
        return mInstance;
    }
    
	//Mining by hand
    public void Mine(string resource_name)
    {
        
		switch (resource_name)
		{
			case "Stone":
				StoneProgress += 1 + TalentBuffs.GetInstance().StoneMultiplier;
				if (StoneProgress > StoneProgressCap)
				{
					StoneProgress = 0;
					GetReward(resource_name);
				}
				StoneProgressBar.current = StoneProgress;
				break;
		
			case "Copper Ore":
				CopperProgress += 1 + TalentBuffs.GetInstance().CopperMultiplier;
				if (CopperProgress > CopperProgressCap)
				{
					CopperProgress = 0;
					GetReward(resource_name);
				}
				CopperProgressBar.current = CopperProgress;
				break;
		
			case "Tin Ore":
				TinProgress += 1 + TalentBuffs.GetInstance().TinMultiplier;
				if (TinProgress > TinProgressCap)
				{
					TinProgress = 0;
					GetReward(resource_name);
				}
				TinProgressBar.current = TinProgress;
				break;
		
			case "Coal":
				CoalProgress += 1 + TalentBuffs.GetInstance().CoalMultiplier;
				if (CoalProgress > CoalProgressCap)
				{
					CoalProgress = 0;
					GetReward(resource_name);
				}
				CoalProgressBar.current = CoalProgress;
				break;
		
			case "Iron Ore":
				IronProgress += 1 + TalentBuffs.GetInstance().IronMultiplier;
				if (IronProgress > IronProgressCap)
				{
					IronProgress = 0;
					GetReward(resource_name);
				}
				IronProgressBar.current = IronProgress;
				break;
			default:
				break;
		}
	}
	//worker mining
	private void AutomatedMining()
	{
		StoneProgress += stonePower * Time.deltaTime;
		if (StoneProgress > StoneProgressCap)
		{
			int rewardAmount = (int)(StoneProgress / StoneProgressCap);
			StoneProgress = (int)(StoneProgress % StoneProgressCap);
			GetReward("Stone", rewardAmount);
		}
		StoneProgressBar.current = StoneProgress;

		CopperProgress += copperPower * Time.deltaTime;
		if (CopperProgress > CopperProgressCap)
		{
			int rewardAmount = (int)(CopperProgress / CopperProgressCap);
			CopperProgress = (int)(CopperProgress % CopperProgressCap);
			GetReward("Copper Ore", rewardAmount);
		}
		CopperProgressBar.current = CopperProgress;


		TinProgress += tinPower * Time.deltaTime;
		if (TinProgress > TinProgressCap)
		{
			int rewardAmount = (int)(TinProgress / TinProgressCap);
			TinProgress = (int)(TinProgress % TinProgressCap);
			GetReward("Tin Ore", rewardAmount);
		}
		TinProgressBar.current = TinProgress;

		CoalProgress += coalPower * Time.deltaTime;
		if (CoalProgress > CoalProgressCap)
		{
			int rewardAmount = (int)(CoalProgress / CoalProgressCap);
			CoalProgress = (int)(CoalProgress % CoalProgressCap);
			GetReward("Coal", rewardAmount);
		}
		CoalProgressBar.current = CoalProgress;

		IronProgress += ironPower * Time.deltaTime;
		if (IronProgress > IronProgressCap)
		{
			int rewardAmount = (int)(IronProgress / IronProgressCap);
			IronProgress = (int)(IronProgress % IronProgressCap);
			GetReward("Iron Ore", rewardAmount);
		}
		IronProgressBar.current = IronProgress;
	}

	//Receiving ore from mining
	public void GetReward(string resource_name, int amount = 1)
	{

		if (!GameController.GetInstance().mResources.ContainsKey(resource_name))
		{
			return; //should not be accessible, thus performs no action
		}
		int modResult = (int)Math.Round(mRandom.NextDouble() * amount);

		GameController.GetInstance().mResources[resource_name].modifyCountCond(Mathf.Max( modResult, 1), 0);
		GameController.GetInstance().mResources["Stone"].modifyCountCond(amount - modResult, 0);




		ruei.EventDescription = "Mining Resource by hand";  //TODO remove this. useful for testing  
		EventController.getInstance().FireEvent(ruei);
	}



	//called whenever a worker purchase occurs. Updates the mining power.
	//Event driven
	void MiningWorkerUpdate(MiningWorkerEventInfo eventInfo)
    {
        
        //Debug.Log(eventInfo.workerCount);
        int power = eventInfo.workerPower;
        string target = eventInfo.eventTargetResource;

		switch (target)
		{
			case "Stone":
				stonePower = power;
				break;
			case "Copper Ore":
				copperPower = power;
				break;
			case "Tin Ore":
				tinPower = power;
				break;
			case "Coal":
				coalPower = power;
				break;
			case "Iron Ore":
				ironPower = power;
				break;
		}
    }

    //SELLING
    //Transfer item to GameController to handle the transaction
    public void SellMiningResource(string resource_name)
    {
        GameController.GetInstance().SellResource(resource_name, amount);

        //Firing off our event
        EventController.getInstance().FireEvent(ruei);
    }

	//UNLOCKS AND UPGRADES
	//
	//~~~~~~~~~~~~~~~~~~~~

	private void UnlockCopperMine(CopperMinePurchaseEventInfo eventInfo)
	{
		CopperMineGO.gameObject.SetActive(true);
	}
	private void UnlockTinMine(TinMinePurchaseEventInfo eventInfo)
	{
		TinMineGO.gameObject.SetActive(true);
	}
	private void UnlockCoalMine(CoalMinePurchaseEventInfo eventInfo)
	{
		CoalMineGO.gameObject.SetActive(true);
	}
	private void UnlockIronMine(IronMinePurchaseEventInfo eventInfo)
	{
		IronMineGO.gameObject.SetActive(true);
	}




	//SAVE GAME STUFF
	private MiningControllerSave CreateSaveGameObject()
	{
		MiningControllerSave save = new MiningControllerSave();
		//assign wariables

		save.stoneProgress = StoneProgress;
		save.stoneProgressCap = StoneProgressCap;
		save.copperProgress = CopperProgress;
		save.copperProgressCap = CopperProgressCap;
		save.tinProgress = TinProgress;
		save.tinProgressCap = TinProgressCap;
		save.coalProgress = CoalProgress;
		save.coalProgressCap = CoalProgressCap;
		save.ironProgress = IronProgress;
		save.ironProgressCap = IronProgressCap;



		return save;

	}

	public void SaveGame(string saveName)
	{
		MiningControllerSave save = CreateSaveGameObject();
		BinaryFormatter bf = new BinaryFormatter();
		System.IO.FileStream file = File.Create(Application.persistentDataPath + "/" + saveName + "/MiningControllerSave.save");
		bf.Serialize(file, save);
		file.Close();

		Debug.Log("Saved Mining Controller...");
	}

	public void LoadGame(string loadName)
	{
		if (File.Exists(Application.persistentDataPath + "/" + loadName + "/MiningControllerSave.save"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + loadName + "/MiningControllerSave.save", FileMode.Open);
			MiningControllerSave save = (MiningControllerSave)bf.Deserialize(file);
			file.Close();

			//Reassign wariables here
			StoneProgress = save.stoneProgress;
			StoneProgressCap = save.stoneProgressCap;
			CopperProgress = save.copperProgress;
			CopperProgressCap = save.copperProgressCap;
			TinProgress = save.tinProgress;
			TinProgressCap = save.tinProgressCap;
			CoalProgress = save.coalProgress;
			CoalProgressCap = save.coalProgressCap;
			IronProgress = save.ironProgress;
			IronProgressCap = save.ironProgressCap;



		}
		else
		{
			Debug.Log("No MiningController save found");
		}
	}













	//VVV this chunk will probably be moved to its own controller VVV
	//PROCESSING
	//handles all workers at once
	//overloading for the workers
	public void ProcessStone(int x)
    {
        if (x > 1)
        {
            ProcessStone(x - 1);
        }

        if (GameController.GetInstance().mResources["Stone"].modifyCountCond(-2, 2)) //Stone
        {
            GameController.GetInstance().mResources["Stone Slab"].modifyCountCond(1, 0); //Brick
        }
    }

    public void SmeltIron()
    {
        if (GameController.GetInstance().mResources["Iron Ore"].modifyCountCond(-4, 4))
        {
            GameController.GetInstance().mResources["Iron Ingot"].modifyCountCond(1, 0);
        }
    }
    public void SmeltIron(int x)
    {
        if (x > 1)
        {
            SmeltIron(x - 1);
        }

        if (GameController.GetInstance().mResources["Iron Ore"].modifyCountCond(-4, 4))
        {
            GameController.GetInstance().mResources["Iron Ingot"].modifyCountCond(1, 0);
        }
    }



}
