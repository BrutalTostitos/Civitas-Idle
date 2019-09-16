using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    private static EventController mInstance;

    //public delegate void updateWorkerUI();
    //public static event updateWorkerUI WorkerUpdate;

    //Setting up our own events to send out
    public delegate void UpdateResourceUI();
    public static event UpdateResourceUI ResourceUpdateUI;
    public delegate void UpdateWorkerUI();
    public static event UpdateWorkerUI WorkerUpdateUI;

    void Awake()
    {
        mInstance = this;
        #region Listening for events
        MiningController.NotifyResourceUpdate += ResourceUpdate;
        WorkerController.NotifyWorkerUpdate += WorkerUpdate;
        #endregion
    }

    public static EventController getInstance()
    {
        if (mInstance == null)
        {
            GameObject go = new GameObject();
            mInstance = go.AddComponent<EventController>();
        }
        return mInstance;
    }
    

    //Tells UIController to update resource info
    public void ResourceUpdate()
    {
        if (ResourceUpdateUI != null)
        {
            ResourceUpdateUI();
        }
    }
    public void WorkerUpdate()
    {
        if (WorkerUpdateUI != null)
        {
            WorkerUpdateUI();
        }
    }

}
