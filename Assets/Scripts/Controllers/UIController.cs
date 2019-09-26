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

    //Mining text block
    public Text StoneCountText;
    public Text StoneMinerCountText;

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
    //~~~~~~

    
    //PRIVATES
    private static UIController mInstance;

    //Canvas switching
    private Dictionary<int, Canvas> mCanvasDict;
    private Dictionary<int, GameObject> mDetailDict;
    private int CurrentCanvasIdx = 1;       //This could cause problems in the future


    #region Events
    //Senders
    //aint got none
    #endregion


    void Awake()
    {
        mInstance = this;

        #region Listening for events
        EventController.getInstance().RegisterListener<ResourceUpdateEventInfo>(UpdateResourceUI);
        EventController.getInstance().RegisterListener<WorkerUpdateEventInfo>(UpdateWorkerUI);
        EventController.getInstance().RegisterListener<FarmingUpdateEventInfo>(UpdateFarmingUI);
        
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

            mCanvasDict[i].gameObject.SetActive(false);

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

    



    //Event driven
    //call this whenever a resource action is taken
    void UpdateResourceUI(ResourceUpdateEventInfo eventInfo)
    {
        //HUD
        GoldCountText.text = GameController.GetInstance().getGold();    
        StoneCountText.text = GameController.GetInstance().mResources["Stone"].getCount().ToString();
        CopperOreCountText.text = GameController.GetInstance().mResources["Copper Ore"].getCount().ToString();
        TinOreCountText.text = GameController.GetInstance().mResources["Tin Ore"].getCount().ToString();
        CoalCountText.text = GameController.GetInstance().mResources["Coal"].getCount().ToString();
        IronOreCountText.text = GameController.GetInstance().mResources["Iron Ore"].getCount().ToString();
    }
    //Event driven
    void UpdateWorkerUI(WorkerUpdateEventInfo eventInfo)
    {
        
        //TODO~
        //Caps need to be updated once buildings are implemented
        GoldCountText.text = GameController.GetInstance().getGold();
        StoneMinerCountText.text = WorkerController.GetInstance().mWorkers["Miner"].getCount().ToString();
        CopperMinerCountText.text = WorkerController.GetInstance().mWorkers["Copper Miner"].getCount().ToString();
        //CopperCapCountText.text = WorkerController.GetInstance().mWorkers["Copper Miner"].getCount().ToString();
        TinMinerCountText.text = WorkerController.GetInstance().mWorkers["Tin Miner"].getCount().ToString();
        //TinCapCountText;
        CoalMinerCountText.text = WorkerController.GetInstance().mWorkers["Coal Miner"].getCount().ToString();
        //CoalCapCountText;
        IronMinerCountText.text = WorkerController.GetInstance().mWorkers["Iron Miner"].getCount().ToString();
        //IronCapCountText;
    }
    //Event driven
    void UpdateFarmingUI(FarmingUpdateEventInfo eventInfo)
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
        mCanvasDict[CurrentCanvasIdx].gameObject.SetActive(false);
        CurrentCanvasIdx = idx;
        mCanvasDict[idx].gameObject.SetActive(true);
        mDetailDict[idx].SetActive(true);

    
        //Disable current zone @ zoneIndex
        //assign new idx to zone idx
        //Init new zone @ zoneIndex
    
    }


}
