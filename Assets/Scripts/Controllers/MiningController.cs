using EventCallBacks;
using System;
using UnityEngine;

public class MiningController : MonoBehaviour
{
    
    private static MiningController mInstance;
    public System.Random mRandom = new System.Random();
    //Creating our event
    UIResourceUpdateEventInfo ruei = new UIResourceUpdateEventInfo();
    
    int amount = 1;

    void Awake()
    {
        mInstance = this;

        #region Listening for events
        EventController.getInstance().RegisterListener<MiningWorkerEventInfo>(MiningWorkerUpdate);

        #endregion

        //Event system setup
        ruei.eventGO = gameObject;
    }

    public static MiningController GetInstance()
    {
        if (mInstance == null)
        {
            Debug.Log("ABORTMINING");
            GameObject go = new GameObject();
            mInstance = go.AddComponent<MiningController>();
        }
        return mInstance;
    }
    
    //TODO this used to also take an int for the amount. 
    //Unity wouldnt let me have 2 args for the inspector hook ups
    //Find a work around
    public void Mine(string resource_name)
    {
        int amount = 1;
        if (!GameController.GetInstance().mResources.ContainsKey(resource_name))
        {
            return; //should not be accessible, thus performs no action
        }
        int modResult = (int)Math.Round(mRandom.NextDouble() * amount);

        GameController.GetInstance().mResources[resource_name].modifyCountCond(modResult, 0);
        GameController.GetInstance().mResources["Stone"].modifyCountCond(amount - modResult, 0);

        

        
        ruei.EventDescription = "Mining Resource by hand";  //TODO remove this. useful for testing  
        EventController.getInstance().FireEvent(ruei);

        
    }


    //TODO keep working on this ya bum
    //Update method for mining workers
    //Event driven
    void MiningWorkerUpdate(MiningWorkerEventInfo eventInfo)
    {
        
        //Debug.Log(eventInfo.workerCount);
        int amount = eventInfo.workerCount;
        string target = eventInfo.eventTargetResource;




        if (!GameController.GetInstance().mResources.ContainsKey(target))
        {
            return; //should not be accessible, thus performs no action
        }
        int modResult = (int)Math.Round(mRandom.NextDouble() * amount);

        GameController.GetInstance().mResources["Stone"].modifyCountCond(modResult, 0);
        GameController.GetInstance().mResources[target].modifyCountCond(amount - modResult, 0);

        ruei.EventDescription = "Mining Stone with worker";  //TODO remove this. useful for testing
        ruei.eventGO = gameObject;      
        EventController.getInstance().FireEvent(ruei);
    }

    //SELLING
    //Transfer item to GameController to handle the transaction
    public void SellMiningResource(string resource_name)
    {
        GameController.GetInstance().SellResource(resource_name, amount);

        //Firing off our event
        EventController.getInstance().FireEvent(ruei);
    }



    //VVV this chunk will probably be moved to its own controller VVV
    //PROCESSING
    //handles all workers at once
    //overloading for the workers
    public void ProcessStone(int x)
    {
        if (x > 1)
        {
            ProcessStone(x - 1);
        }

        if (GameController.GetInstance().mResources["Stone"].modifyCountCond(-2, 2)) //Stone
        {
            GameController.GetInstance().mResources["Stone Slab"].modifyCountCond(1, 0); //Brick
        }
    }

    public void SmeltIron()
    {
        if (GameController.GetInstance().mResources["Iron Ore"].modifyCountCond(-4, 4))
        {
            GameController.GetInstance().mResources["Iron Ingot"].modifyCountCond(1, 0);
        }
    }
    public void SmeltIron(int x)
    {
        if (x > 1)
        {
            SmeltIron(x - 1);
        }

        if (GameController.GetInstance().mResources["Iron Ore"].modifyCountCond(-4, 4))
        {
            GameController.GetInstance().mResources["Iron Ingot"].modifyCountCond(1, 0);
        }
    }



}
