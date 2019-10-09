using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavedGameController : MonoBehaviour
{
    public BuildingController buildingController;

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
        //BroadcastMessage("SaveGame", saveName);
        if (!Directory.Exists(Application.persistentDataPath + "/" + saveName + "/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + saveName + "/");
        }
        buildingController.SaveGame(saveName);
    }

    public void LoadGame(string loadName)
    {
        //BroadcastMessage("LoadGame", loadName);
        buildingController.LoadGame(loadName);
    }
}
