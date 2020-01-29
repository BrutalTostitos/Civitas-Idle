using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmithingController : MonoBehaviour
{
    private static SmithingController mInstance;

    #region Variables
    public ProgressBar mHeatProggressBar;   //assigned in inspector

    private float mFuelAmount;
    private float mFuelCap;
    private int mFurnaceDecrementAmount = 5;
    private float mFurnaceUpdateTimer = 5.0f;
    private float mFurnaceTimerReset = 5.0f;
	#endregion

    

	#region Unity Methods

    // Start is called before the first frame update
    void Awake()
    {
        mInstance = this;

        mFuelAmount = mHeatProggressBar.current = 0.0f;
        mFuelCap = mHeatProggressBar.maximum = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        mFurnaceUpdateTimer -= Time.deltaTime;
        if (mFurnaceUpdateTimer <= 0)
        {
            FurnaceUpdate();
            mFurnaceUpdateTimer = mFurnaceTimerReset;
        }
    }

    #endregion
    public static SmithingController GetInstance()
    {
        if (mInstance == null)
        {
            mInstance = new SmithingController();
        }
        return mInstance;
    }

    public void AddFuel(int amount = 1)
    {
        mFuelAmount += amount;
        if (mFuelAmount > mFuelCap)
        {
            mFuelAmount = mFuelCap;
        }

        mHeatProggressBar.current = mFuelAmount;
        FurnaceGraphicUpdate();

        }

    public void FurnaceUpdate()
    {
        mFuelAmount -= mFurnaceDecrementAmount;
        if (mFuelAmount < 0)
        {
            mFuelAmount = 0;
        }

        mHeatProggressBar.current = mFuelAmount;
        FurnaceGraphicUpdate();
    }
    public void FurnaceGraphicUpdate()
    {
        mHeatProggressBar.gameObject.transform.GetChild(0)
            .gameObject.transform.GetChild(0).GetComponent<Image>().color =
            Color.Lerp(Color.blue, Color.red, mHeatProggressBar.current / mHeatProggressBar.maximum);

    }

}
