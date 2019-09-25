using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Seeds : MonoBehaviour
{
    public enum SEED_TYPE { Wheat, Corn, Potato, Hops }
    
    public SEED_TYPE mType;
    public float harvestTime;
    public int harvestYield;
    public int mValue;
    public int mCount;
    public Mutex mMutex;

    public Seeds(int value, SEED_TYPE seed)
    {
        mType = seed;
        mValue = value;
        mCount = 0;
        mMutex = new Mutex();
        switch (mType)
        {
            case SEED_TYPE.Corn:
                harvestTime = 60;
                harvestYield = 100;
                break;
            case SEED_TYPE.Potato:
                harvestTime = 70;
                harvestYield = 110;
                break;
            case SEED_TYPE.Wheat:
                harvestTime = 150;
                harvestYield = 200;
                break;
            case SEED_TYPE.Hops:
                harvestTime = 200;
                harvestYield = 10;
                break;
            default:
                break;
        }
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
