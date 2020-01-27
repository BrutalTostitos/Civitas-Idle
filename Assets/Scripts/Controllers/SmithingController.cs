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


        mHeatProggressBar.gameObject.transform.GetChild(0)
            .gameObject.transform.GetChild(0).GetComponent<Image>().color =
            Color.Lerp(Color.blue, Color.red, mHeatProggressBar.current / mHeatProggressBar.maximum);
    }

}
