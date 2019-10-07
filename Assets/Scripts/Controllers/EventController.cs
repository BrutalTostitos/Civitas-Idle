using EventCallBacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//maybe make this static and not a monobehaviour
public class EventController : MonoBehaviour
{
    private static EventController mInstance;

    
    //EVENTS
    delegate void EventListener(EventInfo e);
    Dictionary<System.Type, List<EventListener>> eventListeners; //maybe use set so we dont duplicate

	//events for scriptable objects
	CopperMinePurchaseEventInfo cmpei = new CopperMinePurchaseEventInfo();
	TinMinePurchaseEventInfo tmpei = new TinMinePurchaseEventInfo();
	CoalMinePurchaseEventInfo coalmpei = new CoalMinePurchaseEventInfo();
	IronMinePurchaseEventInfo impei = new IronMinePurchaseEventInfo();
	FarmPurchaseEventInfo fpei = new FarmPurchaseEventInfo();
	void Awake()
    {
        mInstance = this;
		cmpei.eventGO = tmpei.eventGO = coalmpei.eventGO = impei.eventGO = fpei.eventGO = gameObject;
		
			

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



    

    public void RegisterListener<T>(System.Action<T> listener) where T : EventInfo
    {
        System.Type eventType = typeof(T);
        #region Error checking
        if (eventListeners == null)
        {
            eventListeners = new Dictionary<System.Type, List<EventListener>>();
        }
        if (!eventListeners.ContainsKey(eventType) || eventListeners[eventType] == null)
        {
            eventListeners[eventType] = new List<EventListener>();
        }
        #endregion

        EventListener wrapper = (e) => { listener((T)e); };

        eventListeners[eventType].Add(wrapper);
        
    }


    public void UnregisterListener<T>(System.Action<T> listener) where T : EventInfo
    {
        //TODO remove ourselves
    }

    //This happens when other code launches an event
    public void FireEvent(EventInfo eventInfo )
    {
        System.Type trueEventInfoClass = eventInfo.GetType();
        if (eventListeners == null || eventListeners[trueEventInfoClass] == null)
        {
            //No one is listening
            return;
        }
        foreach(EventListener e in eventListeners[trueEventInfoClass]) //TODO change to a standard forloop for garbage collection
        {
            e(eventInfo);
        }

    }

	//BUILDING UPGRADES FROM SCRIPTABLE OBJECTS
	public void OnBuyCopperMine()
	{
		FireEvent(cmpei);
	}
	public void OnBuyTinMine()
	{
		FireEvent(tmpei);
	}
	public void OnBuyCoalMine()
	{
		FireEvent(coalmpei);
	}
	public void OnBuyIronMine()
	{
		FireEvent(impei);
	}
	public void OnBuyFarm()
	{
		FireEvent(fpei);
	}
}
