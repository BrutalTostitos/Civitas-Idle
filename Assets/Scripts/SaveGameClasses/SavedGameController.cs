using EventCallBacks;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavedGameController : MonoBehaviour
{
    public BuildingController buildingController;
	public GameController gameController;
	public FarmingController farmingController;
	public WorkerController workerController;
	public MiningController miningController;
	public TalentPanelScript talentPanelScript;
    public MapController mapController;
    UIResourceUpdateEventInfo ruei = new UIResourceUpdateEventInfo();

    public float autoSaveTargetTime = 300.0f;

    void Start()
    {
        LoadGame("SaveGame");
    }

    void Update()
    {
        autoSaveTargetTime -= Time.deltaTime;

        if (autoSaveTargetTime <= 0.0f)
        {
            SaveGame("SaveGame");
            autoSaveTargetTime = 300.0f;
        }
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
		workerController.SaveGame(saveName);
		miningController.SaveGame(saveName);
		talentPanelScript.SaveGame(saveName);
        mapController.SaveGame(saveName);
	}

    public void LoadGame(string loadName)
    {
        //BroadcastMessage("LoadGame", loadName);
        buildingController.LoadGame(loadName);
		gameController.LoadGame(loadName);
		farmingController.LoadGame(loadName);
		workerController.LoadGame(loadName);
		miningController.LoadGame(loadName);
        talentPanelScript.LoadGame(loadName);
        mapController.LoadGame(loadName);
        EventController.getInstance().FireEvent(ruei);
    }

    void OnApplicationQuit()
    {
        SaveGame("SaveGame");
    }
}
