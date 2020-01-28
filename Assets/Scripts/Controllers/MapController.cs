using EventCallBacks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MapController : MonoBehaviour
{
    //TODO: Load from save the current map.
    public Dictionary<Vector2, BuildingTileController> buildingTileControllers = new Dictionary<Vector2, BuildingTileController>();

    public List<Sprite> ResourceSprites;

    public GameObject map;

    public BuildingTileController mapOriginTile;

    public Canvas discoveryCanvas;
    public SpriteMask spriteMask;

    public GameObject TileTemplate;

    //Camera Controlling Variables
    Vector3 lastPos = new Vector3(0, 0, 0);
    Vector3 defaultPos = new Vector3(0, 0, 0);

    MapUpdateEventInfo mapUpdateEventInfo = new MapUpdateEventInfo();


    void Start()
    {
        EventController.getInstance().RegisterListener<MapUpdateEventInfo>(UpdateMap);
        buildingTileControllers[new Vector2(0, 0)] = mapOriginTile;
    }

    void Update()
    {
        //Make more efficient later.
        spriteMask.enabled = discoveryCanvas.enabled;

        if (Input.GetMouseButton(0))
        {
            if (lastPos != defaultPos)
            {
                //The value may need to be adjusted for the resolution that the game is at.
                map.transform.Translate((Input.mousePosition - lastPos)/64);
            }
            lastPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            lastPos = defaultPos;
        }
    }

    

    public void UpdateMap(MapUpdateEventInfo meui)
    {
        Dictionary<Vector2, BuildingTileController> buildingTileControllers2 = new Dictionary<Vector2, BuildingTileController>();
        BuildingTileController btc;
        foreach (Vector2 v2 in buildingTileControllers.Keys)
        {
            if (buildingTileControllers[v2].isDiscovered)
            {
                if (!buildingTileControllers.ContainsKey(v2 - new Vector2(-1, 0)) && !buildingTileControllers2.ContainsKey(v2 - new Vector2(-1, 0)))
                {
                    GameObject temp = GameObject.Instantiate(TileTemplate, map.transform);
                    temp.transform.localPosition = buildingTileControllers[v2].gameObject.transform.localPosition - new Vector3(173.7521f, 0);
                    buildingTileControllers2[v2 - new Vector2(-1, 0)] = temp.GetComponent<BuildingTileController>();
                    btc = temp.GetComponent<BuildingTileController>();
                    int i = (int)UnityEngine.Random.Range(0, 99);
                    if (i >= 80)
                    {
                        btc.isResourceTile = true;
                        btc.BuildingSprite.gameObject.SetActive(true);
                        btc.BuildingSprite.sprite = ResourceSprites[i - 80];
                    }
                }
                if (!buildingTileControllers.ContainsKey(v2 - new Vector2(1, 0)) && !buildingTileControllers2.ContainsKey(v2 - new Vector2(1, 0)))
                {
                    GameObject temp = GameObject.Instantiate(TileTemplate, map.transform);
                    temp.transform.localPosition = buildingTileControllers[v2].gameObject.transform.localPosition - new Vector3(-173.7521f, 0);
                    buildingTileControllers2[v2 - new Vector2(1, 0)] = temp.GetComponent<BuildingTileController>();
                    btc = temp.GetComponent<BuildingTileController>();
                    int i = (int)UnityEngine.Random.Range(0, 99);
                    if (i >= 80)
                    {
                        btc.isResourceTile = true;
                        btc.BuildingSprite.gameObject.SetActive(true);
                        btc.BuildingSprite.sprite = ResourceSprites[i - 80];
                        btc.resourceType = i - 80;
                    }
                }
                if (!buildingTileControllers.ContainsKey(v2 - new Vector2(0, 1)) && !buildingTileControllers2.ContainsKey(v2 - new Vector2(0, 1)))
                {
                    GameObject temp = GameObject.Instantiate(TileTemplate, map.transform);
                    temp.transform.localPosition = buildingTileControllers[v2].gameObject.transform.localPosition - new Vector3(0, -173.7521f);
                    buildingTileControllers2[v2 - new Vector2(0, 1)] = temp.GetComponent<BuildingTileController>();
                    btc = temp.GetComponent<BuildingTileController>();
                    int i = (int)UnityEngine.Random.Range(0, 99);
                    if (i >= 80)
                    {
                        btc.isResourceTile = true;
                        btc.BuildingSprite.gameObject.SetActive(true);
                        btc.BuildingSprite.sprite = ResourceSprites[i - 80];
                    }
                }
                if (!buildingTileControllers.ContainsKey(v2 - new Vector2(0, -1)) && !buildingTileControllers2.ContainsKey(v2 - new Vector2(0, -1)))
                {
                    GameObject temp = GameObject.Instantiate(TileTemplate, map.transform);
                    temp.transform.localPosition = buildingTileControllers[v2].gameObject.transform.localPosition - new Vector3(0, 173.7521f);
                    buildingTileControllers2[v2 - new Vector2(0, -1)] = temp.GetComponent<BuildingTileController>();
                    btc = temp.GetComponent<BuildingTileController>();
                    int i = (int)UnityEngine.Random.Range(0, 99);
                    if (i >= 80)
                    {
                        btc.isResourceTile = true;
                        btc.BuildingSprite.gameObject.SetActive(true);
                        btc.BuildingSprite.sprite = ResourceSprites[i - 80];
                    }
                }
            }
        }

        //Avoids the collection being modified while this update is performed.
        foreach (Vector2 v2 in buildingTileControllers2.Keys)
        {
            buildingTileControllers[v2] = buildingTileControllers2[v2];
        }
    }

    //Handles the saving and loading of the map on game start up and close.
    public void SaveGame(string saveName)
    {
        MapSave save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + saveName + "/MapSave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Saved maps...");
    }

    public void LoadGame(string loadName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + loadName + "/MapSave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + loadName + "/MapSave.save", FileMode.Open);
            MapSave save = (MapSave)bf.Deserialize(file);
            file.Close();


            BuildingTileController btc;
            foreach (TileSave ts in save.tileSaves)
            {
                GameObject temp = GameObject.Instantiate(TileTemplate, map.transform);
                temp.transform.localPosition = new Vector3(ts.worldPosX, ts.worldPosY, ts.worldPosZ);
                buildingTileControllers.Add(new Vector2(ts.posX, ts.posY), temp.GetComponent<BuildingTileController>());

                btc = temp.GetComponent<BuildingTileController>();

                if (ts.isResource == true)
                {
                    btc.isResourceTile = true;
                    btc.BuildingSprite.gameObject.SetActive(true);
                    btc.BuildingSprite.sprite = ResourceSprites[ts.resourceType];
                }
            }
        }
        else
        {
            Debug.Log("No Map Save Found");
        }

        UpdateMap(null);
    }

    private MapSave CreateSaveGameObject()
    {
        MapSave save = new MapSave();


        foreach (Vector2 key in buildingTileControllers.Keys)
        {
            if (key.x == 0 && key.y == 0)
            {
                continue;
            }
            TileSave tileSave = new TileSave();

            tileSave.posX = key.x;
            tileSave.posY = key.y;

            tileSave.isResource = buildingTileControllers[key].isResourceTile;

            tileSave.resourceType = buildingTileControllers[key].resourceType;

            tileSave.worldPosX = buildingTileControllers[key].gameObject.transform.localPosition.x;
            tileSave.worldPosY = buildingTileControllers[key].gameObject.transform.localPosition.y;
            tileSave.worldPosZ = buildingTileControllers[key].gameObject.transform.localPosition.z;

            save.tileSaves.Add(tileSave);
        }

        return save;
    }
}
