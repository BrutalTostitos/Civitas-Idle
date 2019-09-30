using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
//using static Building;

/*
    Gamecontroller~~
    Currently designed to delegate tasks to the other controllers
    Manages resources and gold information itself.
*/
public class GameController : MonoBehaviour
{
    private static GameController mInstance;
    
    public Dictionary<string, Resource> mResources;
    public Dictionary<string, int> mWorkerCaps; //May need moved to workercontroller
    public List<string> ItemsToSell;

    public System.Random mRandom = new System.Random();

    Mutex goldMutex;

    private int goldAmount = 100;

    void Awake()
    {
        
    }
    private GameController()
    {
        mInstance = this;   //I dont think this is working right
        goldMutex = new Mutex();
        
        ItemsToSell = new List<string>();
        mResources = new Dictionary<string, Resource>();
        //? mSeeds = new List<Seeds>();



        //UNPROCESSED
        mResources["Stone"] = new Resource("Stone", 1, ResourceType.Unprocessed);
        mResources["Copper Ore"] = new Resource("Copper Ore", 5, ResourceType.Unprocessed);
        mResources["Tin Ore"] = new Resource("Tin Ore", 5, ResourceType.Unprocessed);
        mResources["Coal"] = new Resource("Coal", 10, ResourceType.Unprocessed);
        mResources["Iron Ore"] = new Resource("Iron Ore", 15, ResourceType.Unprocessed);
        //PROCESSED
        mResources["Stone Slab"] = new Resource("Stone Slab", 4, ResourceType.Processed);

        
    }

    public static GameController GetInstance()
    {
        
        if (mInstance == null)
        {
            //mInstance is set on Awake(), however a gameobject is still added to the scene
            //even though this code *shouldnt* run
            GameObject go = new GameObject("GameController");   
            mInstance = go.AddComponent<GameController>();
        }
        return mInstance;
        
    }
    
    

    public int getResourceValue(string resource_id)
    {
        if (mResources.ContainsKey(resource_id))
        {
            return mResources[resource_id].mValue;
        }
        return 0;
    }

    public int getResourceCount(string resource_id)
    {
        if (mResources.ContainsKey(resource_id))
        {
            return mResources[resource_id].getCount();
        }
        return 0;
    }

    //ONLY USE THIS FOR DISPLAYING -- THATS WHY ITS A STRING YOU DUMMY
    public string getGold()
    {
        return goldAmount.ToString();
    }


    public bool changeGold(int amountToAddToCount, int conditionAmount)
    {
        bool passed = true;
        goldMutex.WaitOne();
        if (goldAmount >= conditionAmount)
        {
            goldAmount += amountToAddToCount;
        }
        else
        {
            passed = false;
        }
        goldMutex.ReleaseMutex();
        return passed;
    }
    
    private void AddToSellList(Resource r)
    {
        ItemsToSell.Add(r.mName);
    }

    //Used by merchants
    public void SellGoods(int x)
    {
        foreach (string s in ItemsToSell)
        {
            SellResource(s);
        }
        //for (int i = 0; i < mResources.Count; i++)
        //{
        //    SellResource(i, x);
        //}
    }
    //handles all workers at once
    //overloading for the workers
    

   
    //Sell Helper Function
    public void SellResource(string key, int count = 1)
    {
        if (!mResources.ContainsKey(key) || count <= 0)
            return;

        if (mResources[key].modifyCountCond(-count, count))
        {
            goldMutex.WaitOne();
            goldAmount += mResources[key].mValue * count;
            goldMutex.ReleaseMutex();
        }
    }
    
    //TODO
    /*
    public void CalculateWorkerCaps()
    {
        string[] keys = mWorkerCaps.Keys.ToArray<string>();
        foreach (string key in keys)
        {
            //10 is the default value.
            mWorkerCaps[key] = Buildings.GetInstance().getWorkerPopCap(key) + 10;
        }
    }
    */

 
    //PERHAPS MOVE THIS SOMEWHERE ELSE
    public enum ResourceType { Unprocessed, Processed, Currency, Worker }
   

    public class Resource
    {
        Mutex mMutex;

        public string mName;
        public int mValue;
        private int mCount;
        public ResourceType mType;

        public List<string> mResourceCosts;
        public List<int> mResourceCostsCounts;

        public bool isCraftable;

        public Resource(string name, int value, ResourceType resourceType, List<string> costs1 = null, List<int> costs2 = null)
        {
            mName = name;
            mValue = value;
            mCount = 0;
            mType = resourceType;
            mMutex = new Mutex();

            if (costs1 != null)
            {
                isCraftable = true;

                mResourceCosts = costs1;
                mResourceCostsCounts = costs2;
            }
            else
            {
                isCraftable = false;
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

        public void Craft()
        {
            if (isCraftable)
            {
                bool canCraft = true;
                for (int i = 0; i < mResourceCosts.Count; i++)
                {
                    if (GameController.GetInstance().mResources[mResourceCosts[i]].getCount() < mResourceCostsCounts[i])
                    {
                        canCraft = false;
                    }
                }
                if (canCraft)
                {
                    for (int i = 0; i < mResourceCosts.Count; i++)
                    {
                        GameController.GetInstance().mResources[mResourceCosts[i]].modifyCountCond(-mResourceCostsCounts[i], mResourceCostsCounts[i]);
                    }
                    mCount++;
                }
            }
        }

        public void Sell(int amount)
        {
            if (modifyCountCond(-amount, amount))
            {
                GameController.GetInstance().changeGold(amount * mValue, 0);
            }
        }
    }



}
