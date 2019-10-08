using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentPanelScript : MonoBehaviour
{
    private int SkillPoints = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyTalent(TalentObject talent)
    {
        if (SkillPoints > 0 && talent.canIncRank)
        {
            SkillPoints--;
            talent.incRank();
        }
        BroadcastMessage("UpdateButton");
    }
}
