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
}
