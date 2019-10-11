using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameController;

[System.Serializable]
public class GameControllerSave
{
	//Resource
    public List<string> resourceNames = new List<string>();
    public List<Resource> resourceValues = new List<Resource>();

	//Gamecontroller
	public List<string> itemsToSell = new List<string>();
    public int goldAmount = 0;
    public float foodAmount = 0;

	public float foodUpdateTimer = 0f;
	public float foodUpdateTimerMax = 0f;

	
}

[System.Serializable]
public class FarmingControllerSave
{
	public List<string> FarmingSeedsKey = new List<string>();
	public List<Seeds> FarmingSeedsValue = new List<Seeds>();
	public List<FarmPlotData> FarmPlots = new List<FarmPlotData>();



	
}

[System.Serializable]
public class WorkerControllerSave
{
	
	public List<string> WorkerNames = new List<string>();
	public List<UnemployedWorker> unemployedValues = new List<UnemployedWorker>();
	public List<MiningWorker> miningValues = new List<MiningWorker>();
	public List<FarmingWorker> farmingValues = new List<FarmingWorker>();
	public List<CookWorker> cookValues = new List<CookWorker>();


	//UnemployedWorker unemployed = new UnemployedWorker(15, WORKER_TYPE.Unemployed);
	//MiningWorker stoneMiner = new MiningWorker(15, WORKER_TYPE.StoneMiner);
	//MiningWorker copperMiner = new MiningWorker(15, WORKER_TYPE.CopperMiner);
	//MiningWorker tinMiner = new MiningWorker(15, WORKER_TYPE.TinMiner);
	//MiningWorker coalMiner = new MiningWorker(15, WORKER_TYPE.CoalMiner);
	//MiningWorker ironMiner = new MiningWorker(15, WORKER_TYPE.IronMiner);
	//FarmingWorker farmWorker = new FarmingWorker(20, WORKER_TYPE.Farmer);
	//CookWorker cookWorker = new CookWorker(20, WORKER_TYPE.Cook);

}

[System.Serializable]
public class FarmPlotData
{
	public float TillProgress = 0;
	public float TillProgressCap = 0;

	public float OverGrownProgress = 0;
	public float OverGrownCap = 0;
	public bool isTilled = false;
	public Seeds Seed = null;
}

[System.Serializable]
public class BuildingSave
{
    public List<string> ownedBuildings = new List<string>();
}