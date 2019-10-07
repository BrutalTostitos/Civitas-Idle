using UnityEngine;
namespace EventCallBacks
{
    public abstract class EventInfo
    {

        //Useful for Debug.Log
        public string EventDescription;
        public int workerCount;


    }
    public class UIResourceUpdateEventInfo : EventInfo
    {
        public GameObject eventGO;
    }

    
    
    //Used for updating ui info after a farm update
    public class UIFarmingUpdateEventInfo : EventInfo
    {
        public GameObject eventGO;
    }

    

    public class BuildingUpdateEventInfo : EventInfo
    {
        public GameObject eventGO;
    }
    //Used when the player clicks the Buy Worker button
    public class BuyWorkerUpdateEventInfo : EventInfo
    {
        public GameObject eventGO;
    }
    //Used to update UI whenever any type of worker is purchased
    public class UIWorkerUpdateEventInfo : EventInfo
    {
        public GameObject eventGO;
    }
    //Used for mining worker autmation
	public class FarmingWorkerEventInfo : EventInfo
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
