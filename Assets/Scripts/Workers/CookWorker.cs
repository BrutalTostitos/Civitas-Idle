using EventCallBacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		cwei = new CookingWorkerEventInfo(amountOfSeedsToUse);		// this isnt going to update when we change?
		cwei.eventGO = go;
	}





	public override void UpdateWorker()
	{
		cwei.workerPower = mCount * mPower;
		EventController.getInstance().FireEvent(cwei);
	}
}
