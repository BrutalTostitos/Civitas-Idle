using EventCallBacks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


//Handles workers and the general population
public class WorkerController : MonoBehaviour
{

    
    private static WorkerController mInstance;
    //Creating our event
    WorkerUpdateEventInfo wuei = new WorkerUpdateEventInfo();


    public Dictionary<string, Worker> mWorkers;
    public Dictionary<string, int> mWorkerCaps;
    private int popCap = 100;

    void Awake()
    {
        mInstance = this;
        mWorkerCaps = new Dictionary<string, int>();
        mWorkers = new Dictionary<string, Worker>();
        //? mSeeds = new List<Seeds>();

        //Event system setup
        wuei.eventGO = gameObject;

        //initializing the types of workers
        mWorkers["Miner"] = new Worker(15, WorkerType.Miner);
        mWorkers["Copper Miner"] = new Worker(15, WorkerType.CopperMiner);
        mWorkers["Tin Miner"] = new Worker(15, WorkerType.TinMiner);
        mWorkers["Coal Miner"] = new Worker(15, WorkerType.CoalMiner);
        mWorkers["Iron Miner"] = new Worker(15, WorkerType.IronMiner);
        mWorkers["Stone Mason"] = new Worker(15, WorkerType.StoneMason);
        mWorkers["Forge Worker"] = new Worker(15, WorkerType.Forgeworker);
        mWorkers["Merchant"] = new Worker(15, WorkerType.Merchant);


        //TODO
        //Initializing WorkerCaps. All are set to 10 for testing purposes
        //Should init to 0. Will need to adjust for unlocking buildings. 
        mWorkerCaps["Copper Mine"] = 10;
        mWorkerCaps["Tin Mine"] = 10;
        mWorkerCaps["Coal Mine"] = 10;
        mWorkerCaps["Iron Mine"] = 10;

    }
    

    public static WorkerController GetInstance()
    {
        if (mInstance == null)
        {
            Debug.Log("ABORTWORKER");
            GameObject go = new GameObject();
            mInstance = go.AddComponent<WorkerController>();
        }
        return mInstance;
    }

    public int getPop()
    {
        int population = 0;
        foreach (KeyValuePair<string, Worker> entry in mWorkers)
        {
            // do something with entry.Value or entry.Key
            population += entry.Value.getCount();
        }
        return population;
    }

    //TODO!!! Not rcving any info from buildings atm
    public int getPopCap()
    {
        return popCap; //+ Buildings.GetInstance().getPopulationCapIncrease();
    }

    //Legacy: used to take string key, int amount = 1. Amount hard coded in the function
    //Due to the inspector not liking multiple args
    //Make a work around
    //This is obnoxious and maybe unecessary in Unity. More research needed
    public void BuyWorker(string key)
    {
        int amount = 1;
        int cost = mWorkers[key].mValue;
        switch (key)
        {
            //special cases for miner specialties

            case "Copper Miner":
                if (mWorkers[key].getCount() >= mWorkerCaps["Copper Mine"] ||
                    !mWorkers["Miner"].modifyCountCond(-amount, amount)) //mWorkers[key].getCount() >= getPopCap())
                {
                    //No purchase made
                    return;
                }
                //Purchase success! Adjusting counts and notifying UI
                mWorkers[key].modifyCountCond(amount, -amount);
                //Firing off our event
                //WorkerUpdateEventInfo wuei = new WorkerUpdateEventInfo();
                //wuei.eventGO = gameObject;      //This feels overkill
                EventController.getInstance().FireEvent(wuei);
                return;

            case "Tin Miner":
                if (mWorkers[key].getCount() >= mWorkerCaps["Tin Mine"] ||
                    !mWorkers["Miner"].modifyCountCond(-amount, amount)) //mWorkers[key].getCount() >= getPopCap())
                {
                    return;
                }
                //Purchase success! Adjusting counts and notifying UI
                mWorkers[key].modifyCountCond(amount, -amount);
                //Firing off our event
                EventController.getInstance().FireEvent(wuei);
                return;
            case "Coal Miner":
                if (mWorkers[key].getCount() >= mWorkerCaps["Coal Mine"] ||
                    !mWorkers["Miner"].modifyCountCond(-amount, amount)) //mWorkers[key].getCount() >= getPopCap())
                {
                    return;
                }
                //Purchase success! Adjusting counts and notifying UI
                mWorkers[key].modifyCountCond(amount, -amount);
                EventController.getInstance().FireEvent(wuei);
                return;
            case "Iron Miner":
                if (mWorkers[key].getCount() >= mWorkerCaps["Iron Mine"] ||
                    !mWorkers["Miner"].modifyCountCond(-amount, amount)) //mWorkers[key].getCount() >= getPopCap())
                {
                    return;
                }
                //Purchase success! Adjusting counts and notifying UI
                mWorkers[key].modifyCountCond(amount, -amount);
                EventController.getInstance().FireEvent(wuei);
                return;

            default:
                break;
        }
        //No special case, we're just buying standard miners
        if ((getPop() <= (getPopCap() - amount)) && GameController.GetInstance().changeGold(-cost * amount, cost * amount))
        {
            mWorkers[key].modifyCountCond(amount, -amount);
        }
        EventController.getInstance().FireEvent(wuei);

    }

    //used to accept count as an arg. Unity inspector no like. Fix
    public void SellWorker(string key)
    {
        int count = 1;
        if (count > mWorkers[key].getCount() || count <= 0)
        {
            return;
        }
        switch (key)
        {
            //this is akin to having OR statements
            case "Copper Miner":
            case "Tin Miner":
            case "Coal Miner":
            case "Iron Miner":
                //mWorkerCaps["Quarry"] 
                mWorkers["Miner"].modifyCountCond(count, -count);


                break;
            default:
                break;
        }

        mWorkers[key].modifyCountCond(-count, count);   //no if check needed, as we are not making gold from this

        //Check to see if we have observers listening in
        EventController.getInstance().FireEvent(wuei);

    }

    

    public enum WorkerType
    {
        Miner, CopperMiner, TinMiner, CoalMiner, IronMiner,
        StoneMason, Forgeworker, Smith, Merchant
    }
    public class Worker
    {
        Mutex mMutex;
        public int mValue;
        private int mCount;
        public int mPower;
        public WorkerType mType;

        public Worker(int value, WorkerType workerType)
        {
            mValue = value;
            mCount = 0;
            mPower = 1;
            mType = workerType;
            mMutex = new Mutex();
        }
        public int getCount()
        {
            return mCount;
        }
        public bool modifyCountCond(int amountToAddToCount, int conditionAmount)
        {
            bool passed = true;
            mMutex.WaitOne();
            if (mCount >= conditionAmount)
                mCount += amountToAddToCount;
            else
                passed = false;
            mMutex.ReleaseMutex();
            return passed;
        }
    }

}
