using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmPlot : MonoBehaviour
{

    public Button tillButton;
    public ProgressBar tillProgressBar;     //horizontal bar
    public ProgressBar harvestProgressBar;  //Radial bar
    public Button cornButton;
    public Button wheatButton;
    public Button potatoButton;
    public Button hopsButton;
    public List<Button> mButtonList;
    //public GameObject go;   //maybe not needed
    public Seeds mSeed = null;
    public Sprite cornSprite;
    public Sprite wheatSprite;
    public Sprite potatoSprite;
    public Sprite hopsSprite;

    
    public float tillProgress = 0.0f;
    public float tillProgressCap = 10.0f;

    

    private void Awake()
    {
        mButtonList.Add(cornButton);
        mButtonList.Add(wheatButton);
        mButtonList.Add(potatoButton);
        mButtonList.Add(hopsButton);
        cornButton.gameObject.SetActive(false);
        wheatButton.gameObject.SetActive(false);
        potatoButton.gameObject.SetActive(false);
        hopsButton.gameObject.SetActive(false);
        harvestProgressBar.gameObject.SetActive(false);
        tillButton.onClick.AddListener(() => TillField());  //adding the click event
        

    }
    private void Update()
    {
        //mSeed member variables can be accessed once planted & assigned, why is it still null?
        if (mSeed != null)
        {
            //Debug.Log(mSeed.harvestTime);
            mSeed.harvestTime += Time.deltaTime;
            harvestProgressBar.current = (int)mSeed.harvestTime;
            if(mSeed.harvestTime >= mSeed.harvestTimeCap)
            {
                HarvestCrops();
            }
        }
    }
    public void TillField(int amount = 1)
    {
        tillProgress += amount;
        
        if (tillProgress >= tillProgressCap)
        {
            tillProgress = 0.0f;
            ToggleButtonsTilled();
        }
        tillProgressBar.current = (int)tillProgress;
    }
    public void HarvestCrops()
    {
        //Determining our harvest and updating farmingcontrollers seed count
        switch(mSeed.mType)
        {
            case Seeds.SEED_TYPE.Corn:
                FarmingController.GetInstance().mFarmingSeeds["Corn"].modifyCountCond(mSeed.harvestYield, 0);
                break;
            case Seeds.SEED_TYPE.Wheat:
                FarmingController.GetInstance().mFarmingSeeds["Wheat"].modifyCountCond(mSeed.harvestYield, 0);
                break;
            case Seeds.SEED_TYPE.Potato:
                FarmingController.GetInstance().mFarmingSeeds["Potato"].modifyCountCond(mSeed.harvestYield, 0);
                break;
            case Seeds.SEED_TYPE.Hops:
                FarmingController.GetInstance().mFarmingSeeds["Hops"].modifyCountCond(mSeed.harvestYield, 0);
                break;

        }
        //change this to take mSeed.harvestYield
        //gameObject.GetComponent<Image>().sprite = cornSprite;
        IconSpawner.GetInstance().SpawnIcons(1,
            gameObject.GetComponent<Image>().sprite,
            gameObject.transform.position);
        //Resetting the plot
        mSeed = null;
        gameObject.GetComponent<Image>().sprite = null;
        ToggleButtonsHarvested();


        //Sending notification to farmcontroller that we need an update
        FarmingController.GetInstance().UpdateMeDaddy();

    }
    

    public void PlantSeedPlot(Seeds seedType)
    {
        if (mSeed != null)
        {
            return;
        }
        mSeed = seedType.ShallowCopy();
        switch (seedType.mType)
        {
            //Updating UI
            case Seeds.SEED_TYPE.Corn:
                //gameObject.sprite = cornSprite;
                gameObject.GetComponent<Image>().sprite = cornSprite;
                //gameObject.GetComponent<Image>().transform.localScale = new Vector3(.5f, .5f);
                break;
            case Seeds.SEED_TYPE.Wheat:
                gameObject.GetComponent<Image>().sprite = wheatSprite;
                break;
            case Seeds.SEED_TYPE.Potato:
                gameObject.GetComponent<Image>().sprite = potatoSprite;
                break;
            case Seeds.SEED_TYPE.Hops:
                gameObject.GetComponent<Image>().sprite = hopsSprite;
                break;
        }
        harvestProgressBar.maximum = (int)mSeed.harvestTimeCap;
        ToggleButtonsPlanted();
        Debug.Log("Planted seed of type" + mSeed.mType);
    }
    //BUTTON TOGGLING STUFF. THERE IS A SMARTER WAY OF DOING THIS
    //TODO - cleanup and optimization

    //Toggles all buttons to their appropriate active state on tilled
    private void ToggleButtonsTilled()
    {
        foreach (Button tmp in mButtonList)
        {
            tmp.gameObject.SetActive(!tmp.gameObject.activeSelf);                           //Toggles plant
        }
        tillButton.gameObject.SetActive(!tillButton.gameObject.activeSelf);                 //Toggles till button
        tillProgressBar.gameObject.SetActive(!tillProgressBar.gameObject.activeSelf);       //toggles till progress bar
    }

    private void ToggleButtonsPlanted()
    {
        foreach (Button tmp in mButtonList)
        {
            tmp.gameObject.SetActive(!tmp.gameObject.activeSelf);
        }

        harvestProgressBar.gameObject.SetActive(!harvestProgressBar.gameObject.activeSelf);
    }
    private void ToggleButtonsHarvested()
    {

        tillButton.gameObject.SetActive(!tillButton.gameObject.activeSelf);
        tillProgressBar.gameObject.SetActive(!tillProgressBar.gameObject.activeSelf);
        harvestProgressBar.gameObject.SetActive(!harvestProgressBar.gameObject.activeSelf);
    }

    //public void PlantSeeds(SeedType seed, int count = 1)
    //{


    //    mButtPanel.Visible = false;
    //    plantSeedButt.Visible = false;
    //    mCurrentSeed = seed;
    //    switch (seed.ToString())
    //    {

    //        case "Corn":

    //            mPlotSubList[mCount].BackgroundImage = Assets.corn;
    //            mPlotSubList[mCount].BackgroundImageLayout = ImageLayout.Zoom;

    //            mCount += 1;
    //            break;
    //        case "Potato":
    //            mPlotPanel.BackgroundImage = Assets.potato;
    //            mPlotPanel.BackgroundImageLayout = ImageLayout.Zoom;

    //            mCount += 1;
    //            break;
    //        case "Wheat":
    //            mPlotPanel.BackgroundImage = Assets.wheat;
    //            mPlotPanel.BackgroundImageLayout = ImageLayout.Zoom;

    //            mCount += 1;
    //            break;
    //        case "Hops":
    //            mPlotPanel.BackgroundImage = Assets.hops;
    //            mPlotPanel.BackgroundImageLayout = ImageLayout.Zoom;

    //            mCount += 1;
    //            break;
    //        default:
    //            break;
    //    }
    //    mPlotPanel.Visible = true;
    //}

    /*
    public FarmPlot()
    {





        //TODO
        //mPlotSubList = new List<Panel>();
        //mMutex = new Mutex();
        //mCount = 0; //our number of seeds
        //mSeedCountCap = 10;
        //harvestTime = -1;

        //tillTime = 0;
        //tillResetTime = 100;

            
        ////Main panel and buttons
        //initMainPanel();
        //initButtPanel();
        //initPlotPanel();





    }
    
    public bool modifyCountCond(int amountToAddToCount, int conditionAmount)
    {
        bool passed = true;
        mMutex.WaitOne();
        if (mCount >= conditionAmount)
            mCount += amountToAddToCount;
        else
            passed = false;
        mMutex.ReleaseMutex();
        return passed;
    }
    //This method is potentially dangerous
    //compars seedcount to current cap AND ensures we have extra seeds in mFarmingSeeds
    public bool compareCountToCap(int amountToAddToCount, SeedType seed)
    {
        bool passed = true;
        //we must lock both mutexs. this might be dumb
        mMutex.WaitOne();
        FarmingController.GetInstance().mFarmingSeeds[seed.ToString()].mMutex.WaitOne();

        if (mCount + amountToAddToCount <= mSeedCountCap && 
            FarmingController.GetInstance().mFarmingSeeds[seed.ToString()].getCount() >= amountToAddToCount)
        {
            mCount += amountToAddToCount;
            FarmingController.GetInstance().mFarmingSeeds[seed.ToString()].mCount -= amountToAddToCount;
        }
                
        else
            passed = false;

        //Releasing both mutex
        mMutex.ReleaseMutex();
        FarmingController.GetInstance().mFarmingSeeds[seed.ToString()].mMutex.ReleaseMutex();
        return passed;
    }


    private void WheatButton_Click(object sender, EventArgs e)
    {
        int count = 1; //hard coded for now
            
        if (FarmingController.GetInstance().mFarmingSeeds["Wheat"].modifyCountCond(-count, count)) //test if we have the resources
        {
                
            PlantSeeds(SeedType.Wheat); 
        }
        Program.MainForm.UpdateList();
    }
    private void PotatoButton_Click(object sender, EventArgs e)
    {
        int count = 1; //hard coded for now
           
        if (FarmingController.GetInstance().mFarmingSeeds["Potato"].modifyCountCond(-count, count)) //test if we have the resources
        {
            PlantSeeds(SeedType.Potato); 
        }
        Program.MainForm.UpdateList();
    }

    private void CornButton_Click(object sender, EventArgs e)
    {
        int count = 1; //hard coded for now
           
        if (FarmingController.GetInstance().mFarmingSeeds["Corn"].modifyCountCond(-count, count)) //test if we have the resources
        {
            PlantSeeds(SeedType.Corn); 
        }
        Program.MainForm.UpdateList();
    }
    private void HopsButton_Click(object sender, EventArgs e)
    {
        int count = 1; //hard coded for now
           
        if (FarmingController.GetInstance().mFarmingSeeds["Hops"].modifyCountCond(-count, count)) //test if we have the resources
        {
            PlantSeeds(SeedType.Hops); 
        }
        Program.MainForm.UpdateList();
    }

        
    private void mPlantAdditionalCropsButton_Click(object sender, EventArgs e)
    {
        int count = 1; //hard coded for now//if (modifyCountCond(count, Farming.GetInstance().mFarmingSeeds["Wheat"].getCount()))

        if (compareCountToCap(count, mCurrentSeed)) //this function is incrementing mCount, all uses should be -1
        {
            switch (mCurrentSeed.ToString())
            {
                case "Corn":
                    mPlotSubList[mCount-1].BackgroundImage = Assets.corn;
                    mPlotSubList[mCount-1].BackgroundImageLayout = ImageLayout.Zoom;
                    break;

                case "Potato":
                    mPlotSubList[mCount-1].BackgroundImage = Assets.potato;
                    mPlotSubList[mCount-1].BackgroundImageLayout = ImageLayout.Zoom;
                    break;
                case "Wheat":
                    mPlotSubList[mCount-1].BackgroundImage = Assets.wheat;
                    mPlotSubList[mCount-1].BackgroundImageLayout = ImageLayout.Zoom;
                    break;
                case "Hops":
                    mPlotSubList[mCount-1].BackgroundImage = Assets.hops;
                    mPlotSubList[mCount-1].BackgroundImageLayout = ImageLayout.Zoom;
                    break;
                default:
                    break;
            }

                
        }
            
        Program.MainForm.UpdateList();
    }

        

    public void transitionFunctionThing()
    {
        tillProgress.Visible = !tillProgress.Visible;
        plantSeedButt.Visible = !plantSeedButt.Visible;
    }

    public void updateTillProgress(ProgressBar tillBar)
    {
        tillBar.PerformStep();
        if (tillBar.Value == tillBar.Maximum)
        {
            transitionFunctionThing();
        }
        tillLabel.Text = tillProgress.Value + " / " + tillProgress.Maximum;
        //tillBar.Value = tillTime;
    }

    private void TillProgress_Click(object sender, EventArgs e)
    {
        tillTime += 1;  //literally pointless atm
        //Since this method is also being called by clicking on the display label,
        //this ensures that we grab a reference to the progress bar
            
        if (!(sender is ProgressBar))
        {
            sender = tillLabel.Parent;
        }
        updateTillProgress((ProgressBar)sender);
            
    }

    private void Butt_Click(object sender, EventArgs e)
    {
        //((Button)sender).Text = ((double)((Button)sender).Tag).ToString();
        mButtPanel.Visible = !mButtPanel.Visible;
        if (((Button)sender).Text == "Plant seeds")
        {
            ((Button)sender).Text = "Cancel";
        }
        else
        {
            ((Button)sender).Text = "Plant seeds";
        }
    }




    //Not thread safe
    //TODO make threadsafe function that compares to supply cap
    public void PlantSeeds(SeedType seed, int count = 1)
    {
            

        mButtPanel.Visible = false;
        plantSeedButt.Visible = false;
        mCurrentSeed = seed;
        switch(seed.ToString())
        {

            case "Corn":

                mPlotSubList[mCount].BackgroundImage = Assets.corn;
                mPlotSubList[mCount].BackgroundImageLayout = ImageLayout.Zoom;

                mCount += 1;
                break;
            case "Potato":
                mPlotPanel.BackgroundImage = Assets.potato;
                mPlotPanel.BackgroundImageLayout = ImageLayout.Zoom;

                mCount += 1;
                break;
            case "Wheat":
                mPlotPanel.BackgroundImage = Assets.wheat;
                mPlotPanel.BackgroundImageLayout = ImageLayout.Zoom;

                mCount += 1;
                break;
            case "Hops":
                mPlotPanel.BackgroundImage = Assets.hops;
                mPlotPanel.BackgroundImageLayout = ImageLayout.Zoom;

                mCount += 1;
                break;
            default:
                break;
        }

            
        mPlotPanel.Visible = true;
           

    }

       
    public void initMainPanel()
    {
        width = 156;
        height = 156;
        mPanel = new Panel();
        mPanel.Location = new Point(6 + (162 * (int)(incrementer % 6)), 259 + (162 * (int)Math.Floor(incrementer / 6)));
        mPanel.Size = new Size(width, height);
        mPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
        mPanel.BackColor = Color.ForestGreen;
        mPanel.BringToFront();
            

        plantSeedButt = new Button();
        plantSeedButt.Width = 150;
        plantSeedButt.Height = 40;
        plantSeedButt.Click += Butt_Click;
        plantSeedButt.Tag = incrementer++;
        plantSeedButt.Text = "Plant seeds";
        plantSeedButt.Visible = false;



        tillProgress = new ProgressBar();
        tillProgress.Width = 150;
        tillProgress.Height = 40;
        tillProgress.Click += TillProgress_Click;
        tillProgress.Location = new Point(0, height - 45);
        tillProgress.Maximum = tillResetTime;
        tillProgress.Minimum = 0;
        tillProgress.Step = 10;
        tillProgress.RightToLeftLayout = true;


        tillLabel = new Label();
        tillLabel.Text = "  0 / 100";
        tillLabel.Size = tillLabel.PreferredSize; //new Size(30, 20);
        tillLabel.Location = new Point(tillProgress.Width / 2 - tillLabel.Width / 2, tillProgress.Height / 2 - tillLabel.Height / 2);
        tillLabel.BackColor = Color.Azure;  //testing purposes only
            
        tillLabel.Click += TillProgress_Click;



        tillProgress.Controls.Add(tillLabel);

        mPanel.Controls.Add(plantSeedButt);
        mPanel.Controls.Add(tillProgress);
    }

    //this occurs when the player has harvested crops
    public void harvest(SeedType seed)
    {
        //Farming.GetInstance().mFarmingSeeds
        FarmingController.GetInstance().mFarmingSeeds[seed.ToString()].modifyCountCond(mCount * 10, -mCount);
        mCount = 0;
            
    }
    public void initPlotPanel()
    {
        //buttPanel holds all the plant seed buttons
        mPlotPanel = new Panel();
        mPlotPanel.Location = new Point(1, 1);
        mPlotPanel.Size = new Size(width - 2, height - 2);
        mPlotPanel.BorderStyle = BorderStyle.None;
        mPlotPanel.BackColor = Color.Azure;    //FOR TESTING
        mPlotPanel.BringToFront();
        mPlotPanel.Visible = false;
        mPlotPanel.BackgroundImage = Assets.wheat;
        mPlotPanel.BackgroundImageLayout = ImageLayout.Zoom;

        mCropsLabel = new Label();
        mCropsLabel.Visible = true;
        mCropsLabel.UseMnemonic = true;
        mCropsLabel.Location = new Point(0, mPlotPanel.Height-15);
        mCropsLabel.Text = "0000" + " / " + "0000";
        mCropsLabel.Size = new Size(mCropsLabel.PreferredWidth, mCropsLabel.PreferredHeight);   //this needs to come after text
        mCropsLabel.Text = mCount.ToString() + " / " + mSeedCountCap.ToString();
        mCropsLabel.BringToFront();

        mPlantAdditionalCropsButton = new Button();
        mPlantAdditionalCropsButton.Visible = true;
        mPlantAdditionalCropsButton.Location = new Point(0, 0);
        mPlantAdditionalCropsButton.Text = "Plant additional seed";
        mPlantAdditionalCropsButton.Size = mPlantAdditionalCropsButton.PreferredSize;
        mPlantAdditionalCropsButton.Click += mPlantAdditionalCropsButton_Click;

        mPanel.Controls.Add(mPlotPanel);
        mPlotPanel.Controls.Add(mCropsLabel);
        mPlotPanel.Controls.Add(mPlantAdditionalCropsButton);
        ///
        for (int i = 0; i < 10; i++)
        {
            Panel tmp = new Panel();
            mPlotSubList.Add(tmp);
            tmp.Width = mPlotPanel.Width / 2;   //TODO adjust these
            tmp.Height = mPlotPanel.Height / 5; //TODO adjust these
            tmp.Location = new Point(tmp.Width * (int)(i / 5), tmp.Height * (i%5));
            //TESTING VVV
            mPlotPanel.Controls.Add(tmp);
        }


            
    }

        

    public void initButtPanel()
    {
        //buttPanel holds all the plant seed buttons
        mButtPanel = new Panel();
        mButtPanel.Location = new Point(5, 20);
        mButtPanel.Size = new Size(width - 17, height - 25);
        //mButtPanel.BackColor = Color.Azure;    //FOR TESTING
        mButtPanel.BringToFront();
        mButtPanel.Visible = false;
        mPanel.Controls.Add(mButtPanel);


        

        Button cornButton = new Button();
        Button potatoButton = new Button();
        Button wheatButton = new Button();
        Button hopsButton = new Button();
        int buttonWidth = 65;
        int buttonHeight = 30;

        cornButton.Width = potatoButton.Width = wheatButton.Width = hopsButton.Width = buttonWidth;
        cornButton.Height = potatoButton.Height = wheatButton.Height = hopsButton.Height = buttonHeight;

        cornButton.Location = new Point(0, buttonHeight * 1);
        potatoButton.Location = new Point(0, buttonHeight * 2);
        wheatButton.Location = new Point((buttonWidth * 1) + 5, buttonHeight * 1);
        hopsButton.Location = new Point((buttonWidth * 1) + 5, buttonHeight * 2);

        cornButton.Text = "Corn";
        potatoButton.Text = "Potato";
        wheatButton.Text = "Wheat";
        hopsButton.Text = "Hops";

        cornButton.Click += CornButton_Click;
        potatoButton.Click += PotatoButton_Click;
        wheatButton.Click += WheatButton_Click;
        hopsButton.Click += HopsButton_Click;

        mButtPanel.Controls.Add(cornButton);
        mButtPanel.Controls.Add(potatoButton);
        mButtPanel.Controls.Add(wheatButton);
        mButtPanel.Controls.Add(hopsButton);
    }


*/
}
