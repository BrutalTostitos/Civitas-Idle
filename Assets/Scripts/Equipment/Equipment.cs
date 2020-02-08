using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


[System.Serializable]
public abstract class Equipment
{

    [System.Serializable]
    public enum EQUIPMENT_TYPE
    {
        None, Pickaxe, Axe, Hammer, Sword
    }



    Mutex mMutex;
    public int mValue;
    public int mCount;
    public int mCapCount;           //Assign this in subclass
    public int mPower;              //the modifier this tool will have for the user
    public float mCurTime;          //Assign this in subclass - used for updateticks
    public float mMaxTime;          //Assign this in subclass

    public EQUIPMENT_TYPE mType;

    public Equipment(int value, EQUIPMENT_TYPE workerType)
    {

        mValue = value;
        mCount = 0;
        mPower = 1;
        mType = workerType;
        mMutex = new Mutex();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
