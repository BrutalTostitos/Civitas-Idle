using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
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

        //for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateMap();
        }
    }

    void UpdateMap()
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
                    temp.transform.localPosition = buildingTileControllers[v2].gameObject.transform.localPosition - new Vector3(174.6f, 0);
                    buildingTileControllers2[v2 - new Vector2(-1, 0)] = temp.GetComponent<BuildingTileController>();
                    btc = temp.GetComponent<BuildingTileController>();
                    int i = (int)Random.Range(0, 99);
                    if (i >= 80)
                    {
                        btc.isResourceTile = true;
                        btc.BuildingSprite.gameObject.SetActive(true);
                        btc.BuildingSprite.sprite = ResourceSprites[i - 80];
                        Debug.Log("GOT RESOURCE");
                    }
                }
                if (!buildingTileControllers.ContainsKey(v2 - new Vector2(1, 0)) && !buildingTileControllers2.ContainsKey(v2 - new Vector2(1, 0)))
                {
                    GameObject temp = GameObject.Instantiate(TileTemplate, map.transform);
                    temp.transform.localPosition = buildingTileControllers[v2].gameObject.transform.localPosition - new Vector3(-174.6f, 0);
                    buildingTileControllers2[v2 - new Vector2(1, 0)] = temp.GetComponent<BuildingTileController>();
                    btc = temp.GetComponent<BuildingTileController>();
                    int i = (int)Random.Range(0, 99);
                    if (i >= 80)
                    {
                        btc.isResourceTile = true;
                        btc.BuildingSprite.gameObject.SetActive(true);
                        btc.BuildingSprite.sprite = ResourceSprites[i - 80];
                        Debug.Log("GOT RESOURCE");
                    }
                }
                if (!buildingTileControllers.ContainsKey(v2 - new Vector2(0, 1)) && !buildingTileControllers2.ContainsKey(v2 - new Vector2(0, 1)))
                {
                    GameObject temp = GameObject.Instantiate(TileTemplate, map.transform);
                    temp.transform.localPosition = buildingTileControllers[v2].gameObject.transform.localPosition - new Vector3(0, -174.6f);
                    buildingTileControllers2[v2 - new Vector2(0, 1)] = temp.GetComponent<BuildingTileController>();
                    btc = temp.GetComponent<BuildingTileController>();
                    int i = (int)Random.Range(0, 99);
                    if (i >= 80)
                    {
                        btc.isResourceTile = true;
                        btc.BuildingSprite.gameObject.SetActive(true);
                        btc.BuildingSprite.sprite = ResourceSprites[i - 80];
                        Debug.Log("GOT RESOURCE");
                    }
                }
                if (!buildingTileControllers.ContainsKey(v2 - new Vector2(0, -1)) && !buildingTileControllers2.ContainsKey(v2 - new Vector2(0, -1)))
                {
                    GameObject temp = GameObject.Instantiate(TileTemplate, map.transform);
                    temp.transform.localPosition = buildingTileControllers[v2].gameObject.transform.localPosition - new Vector3(0, 174.6f);
                    buildingTileControllers2[v2 - new Vector2(0, -1)] = temp.GetComponent<BuildingTileController>();
                    btc = temp.GetComponent<BuildingTileController>();
                    int i = (int)Random.Range(0, 99);
                    if (i >= 80)
                    {
                        btc.isResourceTile = true;
                        btc.BuildingSprite.gameObject.SetActive(true);
                        btc.BuildingSprite.sprite = ResourceSprites[i - 80];
                        Debug.Log(btc.BuildingSprite.sprite.name);
                        Debug.Log("GOT RESOURCE");
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

    public void Save()
    {
        
    }

    public void Load()
    {

    }
}
