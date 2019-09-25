using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSpawner : MonoBehaviour
{
    private static IconSpawner mInstance;

    private List<GameObject> mObjects;


    // Start is called before the first frame update
    void Start()
    {
        mInstance = this;
    }

    //Singleton Constructor.
    //TODO update for unity
    public static IconSpawner GetInstance()
    {
        if (mInstance == null)
        {
            GameObject go = new GameObject("Icon Spawner");
            mInstance = go.AddComponent<IconSpawner>();
        }
        return mInstance;
    }




    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnIcons(int count, Sprite img, Vector2 pos)
    {
        GameObject go = new GameObject("Spawned Icon");                     
        go.transform.localScale = new Vector2(.5f, .5f);                    //rescales to appropriate size
        go.transform.SetParent(GameObject.Find("MainCanvas").transform);    //sets parent so it appears
        go.AddComponent<Image>().sprite = img;                              //attaching our image
        go.transform.position = pos;
        

        ////img = go.AddComponent<Image>().sprite;     //maybe doesnt work
    }

}
