using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


[System.Serializable]
public enum SEED_TYPE { None, Wheat, Corn, Potato, Hops }

[System.Serializable]
public class Seeds
{

	public SEED_TYPE mType;
    public float mHarvestTime;
    public float mHarvestTimeCap;
    public int mHarvestYield;
    public int mValue;
	public int mFood;			//The amount of food this harvests into
    public int mCount;
	public bool mToBeCooked;

    public Seeds(int value, SEED_TYPE seed)
    {
        mType = seed;
        mValue = value;
        mCount = 0;
        mHarvestTime = 0.0f;
		mToBeCooked = true;
        switch (mType)
        {
            case SEED_TYPE.Corn:
				mFood = 5;
                mHarvestTimeCap = 5;
                mHarvestYield = 100;
                break;
            case SEED_TYPE.Potato:
				mFood = 10;
                mHarvestTimeCap = 60f;
                mHarvestYield = 110;
                break;
            case SEED_TYPE.Wheat:
				mFood = 20;
                mHarvestTimeCap = 70f;
                mHarvestYield = 200;
                break;
            case SEED_TYPE.Hops:
				mFood = 0;
                mHarvestTimeCap = 80f;
                mHarvestYield = 10;
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
        if (mCount >= conditionAmount)
            mCount += amountToAddToCount;
        else
            passed = false;
        return passed;
    }

    //Creates a shallow copy. May need to look into a deep copy
    public Seeds ShallowCopy()
    {
		return (Seeds)this.MemberwiseClone();
    }
}
