using EventCallBacks;
using System.Threading;
using UnityEngine;

public enum WorkerType
{
    Miner, CopperMiner, TinMiner, CoalMiner, IronMiner,
    StoneMason, Forgeworker, Smith, Merchant
}
public abstract class Worker : MonoBehaviour
{
    Mutex mMutex;
    public int mValue;
    public int mCount;
    public int mPower;
    public WorkerType mType;
    //private GameObject go;

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
    public abstract void UpdateWorker();

}