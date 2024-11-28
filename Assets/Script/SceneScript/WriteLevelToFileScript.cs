using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WriteLevelToFileScript : MonoBehaviour
{
    public Button button;
    public Text buttonText;

    private string filePath;

    public void SaveButtonTextToFile()
    {
        filePath = Path.Combine(Application.persistentDataPath, "Level.txt");

        if (button != null)
        {
            if (buttonText != null)
            {
                string textToSave = buttonText.text;
                textToSave = textToSave.ToLower();

                File.WriteAllText(filePath, textToSave);

                Debug.Log("Level already save to file: " + filePath);
            }
            else
            {
                Debug.LogWarning("Text is null, please assign.");
            }
        }
        
    }
}
