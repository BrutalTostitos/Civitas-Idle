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

[System.Serializable]
public class TalentSave
{
	public int talentPoints = 0;

	public List<string> talentNames = new List<string>();
	public List<int> talentRanks = new List<int>();
}