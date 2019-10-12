using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGame : MonoBehaviour
{
    public SavedGameController savedGameController;

    public void OnClick()
    {
        savedGameController.SaveGame("SaveGame");
        Application.Quit();
    }
}
