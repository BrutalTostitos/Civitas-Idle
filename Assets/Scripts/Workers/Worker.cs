using EventCallBacks;
using System.Threading;
using UnityEngine;

public enum WORKER_TYPE
{
    Miner, CopperMiner, TinMiner, CoalMiner, IronMiner,
    StoneMason, Forgeworker, Smith, Merchant
}
public abstract class Worker : MonoBehaviour
{
    Mutex mMutex;
    public int mValue;
    public int mCount;
    public int mCapCount;       //Assign this in subclass
    public int mPower;          //potentially useful to determine how effective a worker is at their job
    public float mCurTime;       //Assign this in subclass - used for updateticks
    public float mMaxTime;       //Assign this in subclass

    public WORKER_TYPE mType;

    public Worker(int value, WORKER_TYPE workerType)
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
    public int getCapCount()
    {
        return mCapCount;
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

    //Will be used for increasing the cap on this worker type through buildings & upgrades
    //this is just a generic function. we may want something that is event driven
    public void increaseCapCount(int amount)
    {
        mCapCount += amount;
    }
    public void UpdateTick()
    {
        mCurTime += Time.deltaTime;
        if (mCurTime >= mMaxTime)
        {
            mCurTime = 0.0f;
            UpdateWorker();
        }
    }

    public abstract void UpdateWorker();

    

}