using EventCallBacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FarmingWorker : Worker
{
	FarmingWorkerEventInfo fwei;// = new MiningWorkerEventInfo();
	public string targetResource;
	private GameObject go = new GameObject("Farming Worker");


	public FarmingWorker(int value, WORKER_TYPE workerType) : base(value, workerType)
	{

		
		mCurTime = 0.0f;
		mMaxTime = 1.0f;

		mCapCount = 10; //TODO start this at zero, increase it with new building unlocks & upgrades
						//Debug.Log(gameObject);

		fwei = new FarmingWorkerEventInfo();
		fwei.eventGO = go;
	}

	public override void UpdateWorker()
	{
		fwei.workerPower = mCount * mPower;	//will probably want a way to calculate worker power and send that over instead
		EventController.getInstance().FireEvent(fwei);
	}
}
