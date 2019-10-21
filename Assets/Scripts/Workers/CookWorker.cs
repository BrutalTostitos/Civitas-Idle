using EventCallBacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CookWorker : Worker
{

	CookingWorkerEventInfo cwei;
	private GameObject go = new GameObject("Cook Worker");
	private int amountOfSeedsToUse;


	public CookWorker(int value, WORKER_TYPE workerType) : base(value, workerType)
	{

		Debug.Log("Cookworker has arrived");
		mCurTime = 0.0f;
		mMaxTime = 1.0f;
		mCapCount = 10;         //TODO set this to zero and make it unlockable

		amountOfSeedsToUse = 1;
		cwei = new CookingWorkerEventInfo();		// this isnt going to update when we change?
		cwei.eventGO = go;
	}





	public override void UpdateWorker()
	{
		cwei.workerPower = (int)(mCount * mPower * TalentBuffs.GetInstance().CookingOutput);
		cwei.mSeedsToUse = amountOfSeedsToUse;
		cwei.mWorkerCount = mCount;
		EventController.getInstance().FireEvent(cwei);
	}
}
