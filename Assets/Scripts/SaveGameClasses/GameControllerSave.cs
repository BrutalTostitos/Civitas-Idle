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
public class MiningControllerSave
{
	public float stoneProgress;
	public float stoneProgressCap;
	public float copperProgress;
	public float copperProgressCap;
	public float tinProgress;
	public float tinProgressCap;
	public float coalProgress;
	public float coalProgressCap;
	public float ironProgress;
	public float ironProgressCap;
}

[System.Serializable]
public class FarmingControllerSave
{
	public List<string> FarmingSeedsKey = new List<string>();
	public List<Seeds> FarmingSeedsValue = new List<Seeds>();
	public List<FarmPlotData> FarmPlots = new List<FarmPlotData>();

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
public class WorkerControllerSave
{
	
	public List<string> WorkerNames = new List<string>();
	//public List<Worker> WorkerValues = new List<Worker>();
	public List<WorkerControllerData> workers = new List<WorkerControllerData>();
	public int popCap;
	//public List<MiningWorker> miningValues = new List<MiningWorker>();
	//public List<FarmingWorker> farmingValues = new List<FarmingWorker>();
	//public List<CookWorker> cookValues = new List<CookWorker>();
}

[System.Serializable]
public class WorkerControllerData
{
	public string mName = "none";
	public int mValue = 0;
	public int mCount = 0;
	public int mCapCount = 0;       //Assign this in subclass
	public int mPower = 0;          //potentially useful to determine how effective a worker is at their job
	public float mCurTime = 0;       //Assign this in subclass - used for updateticks
	public float mMaxTime = 0;       //Assign this in subclass

	public WORKER_TYPE mType = WORKER_TYPE.None;
}




[System.Serializable]
public class BuildingSave
{
    public List<string> ownedBuildings = new List<string>();
}

[System.Serializable]
public class TalentSave
{
	public int talentPoints = 0;

	public List<string> talentNames = new List<string>();
	public List<int> talentRanks = new List<int>();
}