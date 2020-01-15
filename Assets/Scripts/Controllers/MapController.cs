using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    //Figure out loading and saving.
    List<GameObject> gameObjects = new List<GameObject>();
    Vector2 mapPos = new Vector2(0, 0);
    public GameObject map;
    Vector3 lastPos = new Vector3(0, 0, 0);
    Vector3 defaultPos = new Vector3(0, 0, 0);
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (lastPos != defaultPos)
            {
                map.transform.Translate((Input.mousePosition - lastPos)/64);
            }
            lastPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            lastPos = defaultPos;
        }
    }
}
