﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{


    public float current = 0;		//just changed from int to float
    public float maximum = 10;
    public Image mask;      //FOR A RADIAL BAR, THE FILL WILL BE PLACED IN THIS SLOT

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        mask.fillAmount = (float)current / (float)maximum;  //tillprogressbar is causing this error
    }

}
