using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentBuffs
{
    #region Farming
    //These are multipliers
    public float OvergrowthModSpeed = 1f;
    
    public float TillModPower = 1f;

    public float CookingOutput = 1f;
    
    private Dictionary<SEED_TYPE, float> SeedGrowthModSpeed = new Dictionary<SEED_TYPE, float>();

    private Dictionary<SEED_TYPE, float> SeedModOutput = new Dictionary<SEED_TYPE, float>();


    public float GetGrowthModSpeed(SEED_TYPE seed_type)
    {
        if (SeedGrowthModSpeed.ContainsKey(seed_type))
        {
            return SeedGrowthModSpeed[seed_type];
        }
        return SeedGrowthModSpeed[seed_type] = 1f;
    }

    public void SetGrowthModSpeed(SEED_TYPE seed_type, float mod)
    {
        SeedGrowthModSpeed[seed_type] = mod;
    }

    public float GetSeedModOutput(SEED_TYPE seed_type)
    {
        if (SeedModOutput.ContainsKey(seed_type))
        {
            return SeedModOutput[seed_type];
        }

        return SeedModOutput[seed_type] = 1f;
    }

    public void SetSeedModOutput(SEED_TYPE seed_type, float mod)
    {
        SeedModOutput[seed_type] = mod;
    }
    #endregion

    #region Mining
    public float CoalMultiplier = 1f;
    public float CopperMultiplier = 1f;
    public float TinMultiplier = 1f;
    public float IronMultiplier = 1f;
    public float StoneMultiplier = 1f;
    #endregion

    //Singleton Variables and Functions
    private static TalentBuffs mInstance;

    public static TalentBuffs GetInstance()
    {
        if (mInstance == null)
        {
            mInstance = new TalentBuffs();
        }
        return mInstance;
    }

    private TalentBuffs()
    {

    }
}
