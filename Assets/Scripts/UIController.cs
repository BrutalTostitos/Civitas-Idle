using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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
    
    //HUD text
    public Text GoldCountText;

    //text block
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
    //~~~~~~

    
    //PRIVATES
    private static UIController mInstance;

    //Canvas switching
    private Dictionary<int, Canvas> mCanvasDict;
    private int CurrentCanvasIdx = 1;       //This could cause problems in the future

    void Awake()
    {
        mInstance = this;

        #region Event Subscribing
        WorkerController.WorkerUpdate += UpdateWorkerUI;
        MiningController.ResourceUpdate += UpdateResourceUI;
        

        //Running these once just to initialize the values on screen
        //UpdateResourceUI();
        //UpdateWorkerUI();
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

        //We skip ahead because MainCanvas and MiningCanvas are already where they are needed
        for (int i = 2; i < mCanvasDict.Count; i++)
        {
            mCanvasDict[i].transform.position = MiningCanvas.transform.position;
            mCanvasDict[i].gameObject.SetActive(false);
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
    void UpdateResourceUI()
    {
        //HUD
        GoldCountText.text = GameController.GetInstance().getGold();    //This probably shouldnt be in here

        StoneCountText.text = GameController.GetInstance().mResources["Stone"].getCount().ToString();
        CopperOreCountText.text = GameController.GetInstance().mResources["Copper Ore"].getCount().ToString();
        TinOreCountText.text = GameController.GetInstance().mResources["Tin Ore"].getCount().ToString();
        CoalCountText.text = GameController.GetInstance().mResources["Coal"].getCount().ToString();
        IronOreCountText.text = GameController.GetInstance().mResources["Iron Ore"].getCount().ToString();
    }
    //Event driven
    void UpdateWorkerUI()
    {
        GoldCountText.text = GameController.GetInstance().getGold();    //This probably shouldnt be in here
        //MINING CONTROLLER
        //TODO~
        //Caps need to be updated once buildings are implemented

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



    //0 = Mining, 1 = Farming, 2 = Forestry. 3 = Buildings. 4 = Market. 5= Smithing
    public void ChangeCavas(int idx)
    {
        if (CurrentCanvasIdx == idx)
        {
            return;
        }
        mCanvasDict[CurrentCanvasIdx].gameObject.SetActive(false);
        CurrentCanvasIdx = idx;
        mCanvasDict[idx].gameObject.SetActive(true);

    
        //Disable current zone @ zoneIndex
        //assign new idx to zone idx
        //Init new zone @ zoneIndex
    
    }


}
