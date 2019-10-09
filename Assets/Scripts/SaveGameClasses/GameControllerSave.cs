using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameControllerSave
{
    public List<string> resourceNames = new List<string>();
    public List<int> resourceAmmounts = new List<int>();

    public int goldAmount = 0;
    public int foodAmount = 0;

	public float foodUpdateTimer = 0f;
	public float foodUpdateTimerMax = 0f;
}

[System.Serializable]
public class FarmingSave
{

}

[System.Serializable]
public class BuildingSave
{
    public List<string> ownedBuildings = new List<string>();
}