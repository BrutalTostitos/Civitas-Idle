using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    //TODO: Load from save the current map.
    List<BuildingTileController> buildingTileControllers = new List<BuildingTileController>();
    public GameObject map;
    Vector3 lastPos = new Vector3(0, 0, 0);
    Vector3 defaultPos = new Vector3(0, 0, 0);

    public Canvas discoveryCanvas;
    public SpriteMask spriteMask;
    void Update()
    {
        //Make more efficient later.
        spriteMask.enabled = discoveryCanvas.enabled;


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

        //for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateMap();
        }
    }

    void UpdateMap()
    {

    }
}
