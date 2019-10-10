using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavedGameController : MonoBehaviour
{
    public BuildingController buildingController;
	public GameController gameController;
	public FarmingController farmingController;
    public TalentPanelScript talentPanelScript;

    // Start is called before the first frame update
    void Start()
    {
        LoadGame("SaveGame");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame(string saveName)
    {
		Debug.Log(Application.persistentDataPath);
        //BroadcastMessage("SaveGame", saveName);
        if (!Directory.Exists(Application.persistentDataPath + "/" + saveName + "/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + saveName + "/");
        }
        buildingController.SaveGame(saveName);
		gameController.SaveGame(saveName);
		farmingController.SaveGame(saveName);
		talentPanelScript.SaveGame(saveName);
	}

    public void LoadGame(string loadName)
    {
        //BroadcastMessage("LoadGame", loadName);
        buildingController.LoadGame(loadName);
		gameController.LoadGame(loadName);
		farmingController.LoadGame(loadName);
        talentPanelScript.LoadGame(loadName);
    }

    void OnApplicationQuit()
    {
        SaveGame("SaveGame");
    }
}
