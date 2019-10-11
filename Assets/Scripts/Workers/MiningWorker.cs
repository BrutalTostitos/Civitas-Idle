using EventCallBacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MiningWorker : Worker
{

    MiningWorkerEventInfo mwei;// = new MiningWorkerEventInfo();
    public string targetResource;
    private GameObject go = new GameObject("Mining Worker");


    public MiningWorker(int value, WORKER_TYPE workerType) : base(value, workerType)
    {
        
        switch (workerType)
        {
            case WORKER_TYPE.StoneMiner:
                targetResource = "Stone";
                break;
            case WORKER_TYPE.CopperMiner:
                targetResource = "Copper Ore";
                break;
            case WORKER_TYPE.TinMiner:
                targetResource = "Tin Ore";
                break;
            case WORKER_TYPE.CoalMiner:
                targetResource = "Coal";
                break;
            case WORKER_TYPE.IronMiner:
                targetResource = "Iron Ore";
                break;
                
        }
        //TODO actually custom-tailor these to the worker types
        mCurTime = 0.0f;
        mMaxTime = 1.0f;

        mCapCount = 10; //TODO start this at zero, increase it with new building unlocks & upgrades
        //Debug.Log(gameObject);

        mwei = new MiningWorkerEventInfo(targetResource);
        mwei.eventGO = go;
    }

    public override void UpdateWorker()
    {
        mwei.workerPower = mCount * mPower;
        EventController.getInstance().FireEvent(mwei);
    }
}
