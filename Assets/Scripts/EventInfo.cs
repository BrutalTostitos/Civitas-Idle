using UnityEngine;
namespace EventCallBacks
{
    public abstract class EventInfo
    {

        //Useful for Debug.Log
        public string EventDescription;


    }
    public class ResourceUpdateEventInfo : EventInfo
    {
        public GameObject eventGO;
    }
    public class WorkerUpdateEventInfo : EventInfo
    {
        public GameObject eventGO;
    }
}
