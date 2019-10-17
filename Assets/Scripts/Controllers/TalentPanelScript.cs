using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class TalentPanelScript : MonoBehaviour
{
    public TalentBuffController talentBuffController;

    private int SkillPoints = 100;
    public Text SkillPointsText;

    public List<TalentButtonController> talentButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SkillPointsText.text = "Prestige Points: " + SkillPoints;
    }

    public void BuyTalent(TalentObject talent)
    {
        if (SkillPoints > 0 && talent.canIncRank)
        {
            SkillPoints--;
            talent.incRank();
            talentBuffController.SendMessage(talent.OnBuy, talent);
        }
        BroadcastMessage("UpdateButton");
    }

    public TalentSave CreateSaveGameObject()
    {  
        TalentSave talentSave = new TalentSave();

        talentSave.talentPoints = SkillPoints;

        foreach (TalentButtonController talent in talentButtons)
        {
            talentSave.talentNames.Add(talent.talentObj.name);
            talentSave.talentRanks.Add(talent.talentObj.Rank);
        }

        return talentSave;
    }

    public void SaveGame(string saveName)
    {
        TalentSave save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + saveName + "/TalentSave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Saved talents...");
    }

    public void LoadGame(string loadName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + loadName + "/TalentSave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + loadName + "/TalentSave.save", FileMode.Open);
            TalentSave save = (TalentSave)bf.Deserialize(file);
            file.Close();

            SkillPoints = save.talentPoints;

            for (int i = 0; i < save.talentNames.Count; i++)
            {
                foreach (TalentButtonController talentButton in talentButtons)
                {
                    if (talentButton.talentObj.name == save.talentNames[i])
                    {
                        talentButton.talentObj.SetRank(save.talentRanks[i]);
                        if (save.talentRanks[i] > 0)
                            talentBuffController.SendMessage(talentButton.talentObj.OnBuy, talentButton.talentObj);
                        break;
                    }
                }
            }
            BroadcastMessage("UpdateButton");
        }
        else
        {
            foreach (TalentButtonController talentButton in talentButtons)
                {
                    talentButton.talentObj.SetRank(0);
                    
                }
            Debug.Log("No Talent Save Found");
        }
    }
}
