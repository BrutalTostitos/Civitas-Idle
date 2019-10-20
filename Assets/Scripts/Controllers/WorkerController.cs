using EventCallBacks;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using UnityEngine;


//Handles workers and the general population
public class WorkerController : MonoBehaviour
{

    
    private static WorkerController mInstance;
    //Creating our events
    UIWorkerUpdateEventInfo uwuei = new UIWorkerUpdateEventInfo();
    //MiningWorkerEventInfo mwei = new MiningWorkerEventInfo();   //worker->event->mining
    
    public Dictionary<string, Worker> mWorkers;
    private int popCap = 100;

    void Awake()
    {
        mInstance = this;
        mWorkers = new Dictionary<string, Worker>();

        #region Events
        //LISTENERS
        EventController.getInstance().RegisterListener<BuyWorkerUpdateEventInfo>(BuyStandardWorker);
        //SENDERS
        uwuei.eventGO = gameObject;
        #endregion
        


        //initializing the types of workers
        //TODO these arent all mining workers you dummy
        mWorkers["Unemployed"] = new UnemployedWorker(15, WORKER_TYPE.Unemployed);
        mWorkers["Stone Miner"] = new MiningWorker(15, WORKER_TYPE.StoneMiner);
        mWorkers["Copper Miner"] = new MiningWorker(15, WORKER_TYPE.CopperMiner);
        mWorkers["Tin Miner"] = new MiningWorker(15, WORKER_TYPE.TinMiner);
        mWorkers["Coal Miner"] = new MiningWorker(15, WORKER_TYPE.CoalMiner);
        mWorkers["Iron Miner"] = new MiningWorker(15, WORKER_TYPE.IronMiner);
		mWorkers["Farmer"] = new FarmingWorker(20, WORKER_TYPE.Farmer);
		mWorkers["Cook"] = new CookWorker(20, WORKER_TYPE.Cook);
        //mWorkers["Stone Mason"] = new MiningWorker(15, WORKER_TYPE.StoneMason);
        //mWorkers["Forge Worker"] = new MiningWorker(15, WORKER_TYPE.Forgeworker);
        //mWorkers["Merchant"] = new MiningWorker(15, WORKER_TYPE.Merchant);

        //TODO
        //Initializing WorkerCaps. All are set to 10 for testing purposes
        //Should init to 0. Will need to adjust for unlocking buildings. 
        //mWorkerCaps["Copper Mine"] = 10;
        //mWorkerCaps["Tin Mine"] = 10;
        //mWorkerCaps["Coal Mine"] = 10;
        //mWorkerCaps["Iron Mine"] = 10;

    }

	//depricated - workers will now function inside of their appropriate controllers
    private void Update()
    {

        //For worker automation
        foreach (KeyValuePair<string, Worker> worker in mWorkers)
        {
            worker.Value.UpdateTick();
        }


    }



    public static WorkerController GetInstance()
    {
        if (mInstance == null)
        {
            GameObject go = new GameObject();
            mInstance = go.AddComponent<WorkerController>();
        }
        return mInstance;
    }

	//This is also being used to update UI. May be wasteful to recount every single time there is a UI update
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

    


    //Event Driven. rcvd from UIController
    public void BuyStandardWorker(BuyWorkerUpdateEventInfo eventInfo)
    {
        int cost = mWorkers["Unemployed"].mValue;
        int amount = 1;


        if ((getPop() <= (getPopCap() - amount)) && GameController.GetInstance().changeGold(-cost * amount, cost * amount))
        {
            mWorkers["Unemployed"].modifyCountCond(amount, -amount);
        }


        //if (mWorkers["Unemployed"].getCount() >= mWorkers["Unemployed"].getCapCount())
        //{
        //    return;
        //}
        //mWorkers["Unemployed"].modifyCountCond(amount, -amount);
        //Firing off our event
        EventController.getInstance().FireEvent(uwuei);

    }
  



    public void BuySpecializedWorker(string key)
    {
        int amount = 1;
        int cost = mWorkers[key].mValue;

		//this switch case could probably be removed
		switch (key)
        {
            //special cases for miner specialties

            case "Stone Miner":
                Debug.Log("Attempting to buy stone miner");
                if (mWorkers[key].getCount() >= mWorkers[key].getCapCount() ||
                    !mWorkers["Unemployed"].modifyCountCond(-amount, amount)) //mWorkers[key].getCount() >= getPopCap())
                {
                    Debug.Log("No purchase made");
                    //No purchase made
                    return;
                }
                //Purchase success! Adjusting counts and notifying UI
                mWorkers[key].modifyCountCond(amount, -amount);
				//Firing off our event
				mWorkers[key].UpdateWorker();
                EventController.getInstance().FireEvent(uwuei);
                return;
            case "Copper Miner":
                if (mWorkers[key].getCount() >= mWorkers[key].getCapCount() ||
                    !mWorkers["Unemployed"].modifyCountCond(-amount, amount)) //mWorkers[key].getCount() >= getPopCap())
                {
                    //No purchase made
                    return;
                }
                //Purchase success! Adjusting counts and notifying UI
                mWorkers[key].modifyCountCond(amount, -amount);
                //Firing off our event
                    
                EventController.getInstance().FireEvent(uwuei);
                return;
            //TODO implement this. You will then need to re-add them to mWorkers up in awake
            case "Tin Miner":
                if (mWorkers[key].getCount() >= mWorkers[key].getCapCount() ||
                    !mWorkers["Unemployed"].modifyCountCond(-amount, amount)) //mWorkers[key].getCount() >= getPopCap())
                {
                    return;
                }
                //Purchase success! Adjusting counts and notifying UI
                mWorkers[key].modifyCountCond(amount, -amount);
                //Firing off our event
                EventController.getInstance().FireEvent(uwuei);
                return;
            case "Coal Miner":
                if (mWorkers[key].getCount() >= mWorkers[key].getCapCount() ||
                    !mWorkers["Unemployed"].modifyCountCond(-amount, amount)) //mWorkers[key].getCount() >= getPopCap())
                {
                    return;
                }
                //Purchase success! Adjusting counts and notifying UI
                mWorkers[key].modifyCountCond(amount, -amount);
                EventController.getInstance().FireEvent(uwuei);
                return;
            case "Iron Miner":
                if (mWorkers[key].getCount() >= mWorkers[key].getCapCount() ||
                    !mWorkers["Unemployed"].modifyCountCond(-amount, amount)) //mWorkers[key].getCount() >= getPopCap())
                {
                    return;
                }
                //Purchase success! Adjusting counts and notifying UI
                mWorkers[key].modifyCountCond(amount, -amount);
                EventController.getInstance().FireEvent(uwuei);
                return;

			case "Farmer":
				if (mWorkers[key].getCount() >= mWorkers[key].getCapCount() ||
					!mWorkers["Unemployed"].modifyCountCond(-amount, amount)) //mWorkers[key].getCount() >= getPopCap())
				{
					return;
				}
				//Purchase success! Adjusting counts and notifying UI
				mWorkers[key].modifyCountCond(amount, -amount);
				EventController.getInstance().FireEvent(uwuei);
				return;
			case "Cook":
				if (mWorkers[key].getCount() >= mWorkers[key].getCapCount() ||
					!mWorkers["Unemployed"].modifyCountCond(-amount, amount)) //mWorkers[key].getCount() >= getPopCap())
				{
					return;
				}
				//Purchase success! Adjusting counts and notifying UI
				mWorkers[key].modifyCountCond(amount, -amount);
				EventController.getInstance().FireEvent(uwuei);
				return;
			default:
                break;
        }

        //TODO
        //No special case, we're just buying Unemplyed workers
        //if ((getPop() <= (getPopCap() - amount)) && GameController.GetInstance().changeGold(-cost * amount, cost * amount))
        //{
        //    mWorkers["Miner"].modifyCountCond(amount, -amount);
        //}
        //EventController.getInstance().FireEvent(uwuei);

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
            case "Stone Miner":
            case "Copper Miner":
            case "Tin Miner":
            case "Coal Miner":
            case "Iron Miner":
			case "Farmer":
			case "Cook":
                //mWorkerCaps["Quarry"] 
                mWorkers["Unemployed"].modifyCountCond(count, -count);


                break;
            default:
                break;
        }

        mWorkers[key].modifyCountCond(-count, count);   //no if check needed, as we are not making gold from this
		mWorkers[key].UpdateWorker();
        //Check to see if we have observers listening in
        EventController.getInstance().FireEvent(uwuei);

    }






	//Saved Game
	private WorkerControllerSave CreateSaveGameObject()
	{
		WorkerControllerSave save = new WorkerControllerSave();
		//assign wariables


		foreach (KeyValuePair<string, Worker> worker in mWorkers)
		{
			save.WorkerNames.Add(worker.Key);
			WorkerControllerData tmp = new WorkerControllerData();
			tmp.mName = worker.Key;
			tmp.mType = worker.Value.mType;
			tmp.mValue = worker.Value.mValue;
			tmp.mCount = worker.Value.mCount;
			tmp.mCapCount = worker.Value.mCapCount;
			tmp.mPower = worker.Value.mPower;
			tmp.mCurTime = worker.Value.mCurTime;
			tmp.mMaxTime = worker.Value.mMaxTime;
			save.workers.Add(tmp);
		}

		return save;
		
	}

	public void SaveGame(string saveName)
	{
		WorkerControllerSave save = CreateSaveGameObject();
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/" + saveName + "/WorkerControllerSave.save");
		bf.Serialize(file, save);
		file.Close();

		Debug.Log("Saved Worker controller...");
	}

	public void LoadGame(string loadName)
	{
		if (File.Exists(Application.persistentDataPath + "/" + loadName + "/WorkerControllerSave.save"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + loadName + "/WorkerControllerSave.save", FileMode.Open);
			WorkerControllerSave save = (WorkerControllerSave)bf.Deserialize(file);
			file.Close();

			//Assign variables
			foreach (KeyValuePair<string, Worker> worker in mWorkers)
			{
				int i = save.WorkerNames.IndexOf(worker.Key);
				//Debug.Log("worker value " + worker.Value.mValue);
				//Debug.Log("saved worker value " + save.workers[i].mValue);
				//Debug.Log("worker count " + worker.Value.mCount);
				//Debug.Log("saved worker count " + save.workers[i].mCount);
				//Debug.Log("worker capcount " + worker.Value.mCapCount);
				//Debug.Log("saved worker capcount " + save.workers[i].mCapCount);
				//Debug.Log(worker.Value.mPower);
				//Debug.Log(save.workers[i].mPower);
				//Debug.Log(worker.Value.mCurTime);
				//Debug.Log(save.workers[i].mCurTime);
				//Debug.Log(worker.Value.mMaxTime);
				//Debug.Log(save.workers[i].mMaxTime);
				//Debug.Log(worker.Value.mType);
				//worker.Value.mType = save.workers[i].mType;
				worker.Value.mValue = save.workers[i].mValue;
				worker.Value.mCount = save.workers[i].mCount;
				worker.Value.mCapCount = save.workers[i].mCapCount;
				worker.Value.mPower = save.workers[i].mPower;
				worker.Value.mCurTime = save.workers[i].mCurTime;
				worker.Value.mMaxTime = save.workers[i].mMaxTime;
			}

		}
		else
		{
			Debug.Log("No Workercontroller save found");
		}
	}

}
