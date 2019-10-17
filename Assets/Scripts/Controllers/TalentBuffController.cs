using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentBuffController : MonoBehaviour
{
    #region Farming
    public void TillModPower(TalentObject talentObj)
    {
        TalentBuffs.GetInstance().TillModPower = (int)(1 + talentObj.Rank*0.4);
    }

    public void OvergrowthModSpeed(TalentObject talentObj)
    {
        TalentBuffs.GetInstance().OvergrowthModSpeed = 1 - talentObj.Rank * 0.05f;
    }

    public void OutputModPower(TalentObject talentObj)
    {
        TalentBuffs.GetInstance().SetSeedModOutput(talentObj.Seed_Type, 1 + talentObj.Rank * 0.2f);
    }

    public void SpeedModPower(TalentObject talentObj)
    {
        TalentBuffs.GetInstance().SetGrowthModSpeed(talentObj.Seed_Type, 1 + talentObj.Rank * 0.2f);
    }

    public void CookingModOutput(TalentObject talentObject)
    {
        TalentBuffs.GetInstance().CookingOutput = 1 + talentObject.Rank;
    }
    #endregion

    #region Mining
    public void CoalYield(TalentObject talentObject)
    {
        TalentBuffs.GetInstance().CoalMultiplier = talentObject.Rank * 0.2f + 1f;
    }

    public void CopperYield(TalentObject talentObject)
    {
        TalentBuffs.GetInstance().CopperMultiplier = talentObject.Rank * 0.2f + 1f;
    }

    public void IronYield(TalentObject talentObject)
    {
        TalentBuffs.GetInstance().IronMultiplier = talentObject.Rank * 0.2f + 1f;
    }

    public void StoneYield(TalentObject talentObject)
    {
        TalentBuffs.GetInstance().StoneMultiplier = talentObject.Rank * 0.2f + 1f;
    }

    public void TinYield(TalentObject talentObject)
    {
        TalentBuffs.GetInstance().TinMultiplier = talentObject.Rank * 0.2f + 1f;
    }
    #endregion
}
