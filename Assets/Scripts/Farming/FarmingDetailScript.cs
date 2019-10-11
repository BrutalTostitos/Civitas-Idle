using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingDetailScript : MonoBehaviour
{
    public FarmingController farmingController;
    public GameObject farmPlotPrefab;
    public List<GameObject> farmPlots;

    void Start()
    {
        farmPlots = new List<GameObject>();

        int y = 0;
        for (int x = 0; x < 6; x++)
        {
            GameObject temp = Instantiate(farmPlotPrefab, this.gameObject.transform);
            temp.transform.Translate(10*x, 0, -10*y);
            farmPlots.Add(temp);
            if (x == 4)
            {
                x = -1;
                y++;
                if (y == 2)
                {
                    break;
                }
            }
        }
    }

    void Update()
    {
        
    }

    public void SetPlant(int plotID, int plantID)
    {
        farmPlots[plotID].GetComponent<FarmPlotDetailScript>().SetPlant(plantID);
    }

    internal void ClearPlot(int iD)
    {
        farmPlots[iD].GetComponent<FarmPlotDetailScript>().ClearPlant();
    }
}
