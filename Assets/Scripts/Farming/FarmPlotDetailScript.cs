using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmPlotDetailScript : MonoBehaviour
{
    [SerializeField]
    public GameObject[] plantPresets;

    int CurrentPlant = -1;

    public void SetPlant(int i)
    {
        if (CurrentPlant != -1)
        {
            plantPresets[CurrentPlant].SetActive(false);
        }
        plantPresets[i].SetActive(true);
        CurrentPlant = i;
    }

    public void ClearPlant()
    {
        if (CurrentPlant != -1)
            plantPresets[CurrentPlant].SetActive(false);
        CurrentPlant = -1;
    }
}
