using UnityEngine;
namespace EventCallBacks
{
    public abstract class EventInfo
    {

        //Useful for Debug.Log
        public string EventDescription;
        public int workerCount;


    }
    public class ResourceUpdateEventInfo : EventInfo
    {
        public GameObject eventGO;
    }
    public class WorkerUpdateEventInfo : EventInfo
    {
        public GameObject eventGO;
    }
    
    public class FarmingUpdateEventInfo : EventInfo
    {
        public GameObject eventGO;
    }


    public class BuildingUpdateEventInfo : EventInfo
    {
        public GameObject eventGO;
    }
    public class MiningWorkerEventInfo : EventInfo
    {
        public GameObject eventGO;
        public string eventTargetResource;
        public WORKER_TYPE mType;
        public MiningWorkerEventInfo(string target)
        {
            eventTargetResource = target;
        }
        
    }
}
