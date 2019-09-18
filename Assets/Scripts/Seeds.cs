using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Seeds : MonoBehaviour
{
    public enum SeedType { Wheat, Corn, Potato, Hops }
    
    public SeedType mType;
    public float harvestTime;
    public int mValue;
    public int mCount;
    public Mutex mMutex;

    public Seeds(int value, SeedType seed)
    {
        mType = seed;
        mValue = value;
        mCount = 0;
        mMutex = new Mutex();
        switch (mType)
        {
            case SeedType.Corn:
                harvestTime = 60;
                break;
            case SeedType.Potato:
                harvestTime = 70;
                break;
            case SeedType.Wheat:
                harvestTime = 150;
                break;
            case SeedType.Hops:
                harvestTime = 200;
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
