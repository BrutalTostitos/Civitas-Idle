using EventCallBacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningWorker : Worker
{

    MiningWorkerEventInfo mwei;// = new MiningWorkerEventInfo();
    public string targetResource;
    private GameObject go = new GameObject("Mining Worker");


    public MiningWorker(int value, WORKER_TYPE workerType) : base(value, workerType)
    {
        
        switch (workerType)
        {
            case WORKER_TYPE.Miner:
                targetResource = "Stone";
                break;
            case WORKER_TYPE.CopperMiner:
                targetResource = "Copper Ore";
                break;
                //todo - fill this out

        }

        //Debug.Log(gameObject);

        mwei = new MiningWorkerEventInfo(targetResource);
        mwei.eventGO = go;
    }

    public override void UpdateWorker()
    {
        mwei.workerCount = mCount;
        EventController.getInstance().FireEvent(mwei);
    }
}
