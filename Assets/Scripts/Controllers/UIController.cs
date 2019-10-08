using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using EventCallBacks;

//So far, just updates ui text elements
public class UIController : MonoBehaviour
{
    //PUBLICS
    //ASSIGNED IN EDITOR
    public Canvas MainCanvas;
    public Canvas MiningCanvas;
    public Canvas FarmingCanvas;
    public Canvas ForestryCanvas;
    public Canvas BuildingCanvas;
    public Canvas MarketCanvas;
    public Canvas SmithingCanvas;

    public GameObject MiningDetail;
    public GameObject FarmingDetail;
    public GameObject ForestryDetail;
    public GameObject BuildingDetail;
    public GameObject MarketDetail;
    public GameObject SmithingDetail;
    
    //HUD text
    public Text GoldCountText;
	public Text UnemployedWorkerCountText;
	public Text PopulationCountText;
	public Text FoodCountText;
    //Resource Panel things
    public Button BuyWorkerButton;

    //Inventory Panel things


    //Mining text block
    public Text StoneCountText;
    public Text StoneMinerCountText;
    public Text StoneCapCountText;

    public Text CopperOreCountText;
    public Text CopperMinerCountText;
    public Text CopperCapCountText;

    public Text TinOreCountText;
    public Text TinMinerCountText;
    public Text TinCapCountText;

    public Text CoalCountText;
    public Text CoalMinerCountText;
    public Text CoalCapCountText;

    public Text IronOreCountText;
    public Text IronMinerCountText;
    public Text IronCapCountText;

    //Farming text block
    public Text CornCountText;
    public Text WheatCountText;
    public Text PotatoCountText;
    public Text HopsCountText;

	public Text FarmerCountText;
	public Text CookCountText;
	//~~~~~~


	//MINING THINGS


	//PRIVATES
	private static UIController mInstance;

    //Canvas switching
    private Dictionary<int, Canvas> mCanvasDict;
    private Dictionary<int, GameObject> mDetailDict;
    private int CurrentCanvasIdx = 1;       //This could cause problems in the future


    #region Events
    //This event is used to inform the workercontroller that we are attempting to purchase a worker
    BuyWorkerUpdateEventInfo bwuei = new BuyWorkerUpdateEventInfo();
    #endregion
    

    void Awake()
    {
        mInstance = this;

        #region Events
        //Listeners
        EventController.getInstance().RegisterListener<UIResourceUpdateEventInfo>(CallUpdateResourceUI);
		EventController.getInstance().RegisterListener<FarmPurchaseEventInfo>(CallUpdateResourceUI);
		EventController.getInstance().RegisterListener<CopperMinePurchaseEventInfo>(CallUpdateResourceUI);
		EventController.getInstance().RegisterListener<TinMinePurchaseEventInfo>(CallUpdateResourceUI);
		EventController.getInstance().RegisterListener<CoalMinePurchaseEventInfo>(CallUpdateResourceUI);
		EventController.getInstance().RegisterListener<IronMinePurchaseEventInfo>(CallUpdateResourceUI);

		

		EventController.getInstance().RegisterListener<UIWorkerUpdateEventInfo>(UpdateWorkerUI);
        EventController.getInstance().RegisterListener<UIFarmingUpdateEventInfo>(UpdateFarmingUI);
        //Senders
        bwuei.eventGO = gameObject;
        #endregion

        #region Canvas region
        mCanvasDict = new Dictionary<int, Canvas>();
        mCanvasDict[0] = MainCanvas;
        mCanvasDict[1] = MiningCanvas;
        mCanvasDict[2] = FarmingCanvas;
        mCanvasDict[3] = ForestryCanvas;
        mCanvasDict[4] = BuildingCanvas;
        mCanvasDict[5] = MarketCanvas;
        mCanvasDict[6] = SmithingCanvas;
        #endregion

        #region Detail region
        mDetailDict = new Dictionary<int, GameObject>();
        mDetailDict[0] = null;
        mDetailDict[1] = MiningDetail;
        mDetailDict[2] = FarmingDetail;
        mDetailDict[3] = ForestryDetail;
        mDetailDict[4] = BuildingDetail;
        mDetailDict[5] = MarketDetail;
        mDetailDict[6] = SmithingDetail;
        #endregion

        //We skip ahead because MainCanvas and MiningCanvas are already where they are needed
        for (int i = 2; i < mCanvasDict.Count; i++)
        {
            mCanvasDict[i].transform.position = MiningCanvas.transform.position;

            mCanvasDict[i].gameObject.GetComponent<Canvas>().enabled = false;
            mDetailDict[i].SetActive(false);
        }



    }
    
    public static UIController GetInstance()
    {

        if (mInstance == null)
        {
            GameObject go = new GameObject();
            mInstance = go.AddComponent<UIController>();
        }
        return mInstance;
    }

    
    //~~SENDING EVENTS
    //Notifies the workercontroller that we are attempting to purchase a worker
    public void PurchaseWorker()
    {
        EventController.getInstance().FireEvent(bwuei);
    }

	//~~RECEIVING EVENTS
	//Event helpers
	private void CallUpdateResourceUI(FarmPurchaseEventInfo eventInfo)
	{
		UpdateResourceUI();
	}
	private void CallUpdateResourceUI(UIResourceUpdateEventInfo eventInfo)
	{
		UpdateResourceUI();
	}
	private void CallUpdateResourceUI(CopperMinePurchaseEventInfo eventInfo)
	{
		UpdateResourceUI();
	}
	private void CallUpdateResourceUI(TinMinePurchaseEventInfo eventInfo)
	{
		UpdateResourceUI();
	}
	private void CallUpdateResourceUI(CoalMinePurchaseEventInfo eventInfo)
	{
		UpdateResourceUI();
	}
	private void CallUpdateResourceUI(IronMinePurchaseEventInfo eventInfo)
	{
		UpdateResourceUI();
	}

	//called whenever a resource action is taken
	void UpdateResourceUI()
    {
        //HUD
        GoldCountText.text = GameController.GetInstance().getGold();
		FoodCountText.text = "Food " + GameController.GetInstance().getFoodCount().ToString();
        StoneCountText.text = GameController.GetInstance().mResources["Stone"].getCount().ToString();
        CopperOreCountText.text = GameController.GetInstance().mResources["Copper Ore"].getCount().ToString();
        TinOreCountText.text = GameController.GetInstance().mResources["Tin Ore"].getCount().ToString();
        CoalCountText.text = GameController.GetInstance().mResources["Coal"].getCount().ToString();
        IronOreCountText.text = GameController.GetInstance().mResources["Iron Ore"].getCount().ToString();
    }
    //Event driven
    //called whenever a worker is purchased
    void UpdateWorkerUI(UIWorkerUpdateEventInfo eventInfo)
    {
        
        //TODO~
        //Caps need to be updated once buildings are implemented
        GoldCountText.text = GameController.GetInstance().getGold();
		UnemployedWorkerCountText.text = "Unemployed: " +
			WorkerController.GetInstance().mWorkers["Unemployed"].getCount().ToString();
		PopulationCountText.text = "Population: " + WorkerController.GetInstance().getPop() +
			" / " + WorkerController.GetInstance().getPopCap();
        StoneMinerCountText.text = WorkerController.GetInstance().mWorkers["Stone Miner"].getCount().ToString();
        StoneCapCountText.text = WorkerController.GetInstance().mWorkers["Stone Miner"].getCapCount().ToString();
        CopperMinerCountText.text = WorkerController.GetInstance().mWorkers["Copper Miner"].getCount().ToString();
        CopperCapCountText.text = WorkerController.GetInstance().mWorkers["Copper Miner"].getCapCount().ToString();
        TinMinerCountText.text = WorkerController.GetInstance().mWorkers["Tin Miner"].getCount().ToString();
        TinCapCountText.text = WorkerController.GetInstance().mWorkers["Tin Miner"].getCapCount().ToString();
        
        CoalMinerCountText.text = WorkerController.GetInstance().mWorkers["Coal Miner"].getCount().ToString();
        CoalCapCountText.text = WorkerController.GetInstance().mWorkers["Coal Miner"].getCapCount().ToString();
        
        IronMinerCountText.text = WorkerController.GetInstance().mWorkers["Iron Miner"].getCount().ToString();
        IronCapCountText.text = WorkerController.GetInstance().mWorkers["Iron Miner"].getCapCount().ToString();
		FarmerCountText.text = "Farmers: " + WorkerController.GetInstance().mWorkers["Farmer"].getCount().ToString();
		CookCountText.text = "Cooks: " + WorkerController.GetInstance().mWorkers["Cook"].getCount().ToString();
		//insert line here for farmer cap count
        
    }
    //Event driven
    void UpdateFarmingUI(UIFarmingUpdateEventInfo eventInfo)
    {
        CornCountText.text = FarmingController.GetInstance().mFarmingSeeds["Corn"].getCount().ToString();
        WheatCountText.text = FarmingController.GetInstance().mFarmingSeeds["Wheat"].getCount().ToString();
        PotatoCountText.text = FarmingController.GetInstance().mFarmingSeeds["Potato"].getCount().ToString();
        HopsCountText.text = FarmingController.GetInstance().mFarmingSeeds["Hops"].getCount().ToString();
    }


    //0 = Mining, 1 = Farming, 2 = Forestry. 3 = Buildings. 4 = Market. 5= Smithing
    public void ChangeCavas(int idx)
    {
        if (CurrentCanvasIdx == idx)
        {
            return;
        }
        mDetailDict[CurrentCanvasIdx].SetActive(false);
        mCanvasDict[CurrentCanvasIdx].gameObject.GetComponent<Canvas>().enabled = false;
        CurrentCanvasIdx = idx;
        mCanvasDict[idx].gameObject.GetComponent<Canvas>().enabled = true;
        mDetailDict[idx].SetActive(true);

    
        //Disable current zone @ zoneIndex
        //assign new idx to zone idx
        //Init new zone @ zoneIndex
    
    }


}
