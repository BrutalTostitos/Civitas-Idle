using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class SavesWindow : EditorWindow
{
    [MenuItem("Civitas-Idle/Saves")]
    public static void ShowWindow()
    {
        SavesWindow window = (SavesWindow)EditorWindow.GetWindow(typeof(SavesWindow));
        window.Show();
        
    }

    void OnGUI()
    {
        GUILayout.Label("Save Settings", EditorStyles.boldLabel);
        if (GUILayout.Button("Clear Saves"))
        {
            if (!Directory.Exists(Application.persistentDataPath + "/SaveGame/"))
                return;
            string[] files = Directory.GetFiles(Application.persistentDataPath + "/SaveGame/");
            foreach (string s in files)
            {
                File.Delete(s);
            }
            Directory.Delete(Application.persistentDataPath + "/SaveGame/");
            Debug.Log("Cleared Saved Games!");
        }
    }
}
