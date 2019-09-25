using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Seeds
{
    public enum SEED_TYPE { Wheat, Corn, Potato, Hops }
    
    public SEED_TYPE mType;
    public float harvestTime;
    public float harvestTimeCap;
    public int harvestYield;
    public int mValue;
    public int mCount;
    public Mutex mMutex;

    public Seeds(int value, SEED_TYPE seed)
    {
        mType = seed;
        mValue = value;
        mCount = 0;
        harvestTime = 0f;
        mMutex = new Mutex();
        switch (mType)
        {
            case SEED_TYPE.Corn:
                harvestTimeCap = 50f;
                harvestYield = 100;
                break;
            case SEED_TYPE.Potato:
                harvestTimeCap = 60f;
                harvestYield = 110;
                break;
            case SEED_TYPE.Wheat:
                harvestTimeCap = 70f;
                harvestYield = 200;
                break;
            case SEED_TYPE.Hops:
                harvestTimeCap = 80f;
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

    //Creates a shallow copy. May need to look into a deep copy
    public Seeds ShallowCopy()
    {
        return (Seeds)this.MemberwiseClone();
    }
}
