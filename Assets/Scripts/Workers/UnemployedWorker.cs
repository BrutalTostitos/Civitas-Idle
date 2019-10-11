﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnemployedWorker : Worker
{
    public string targetResource;
    private GameObject go = new GameObject("Unemployed Worker");


    public UnemployedWorker(int value, WORKER_TYPE workerType) : base(value, workerType)
    {

        switch (workerType)
        {
            case WORKER_TYPE.Unemployed:
                targetResource = "Stone";	//TODO change this
                break;

        }

        mCapCount = 10;
    }

    public override void UpdateWorker()
    {
        //mwei.workerCount = mCount;
        //EventController.getInstance().FireEvent(mwei);
    }
}
