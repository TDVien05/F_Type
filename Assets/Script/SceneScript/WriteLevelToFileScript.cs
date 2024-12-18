using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using GameLogic;
public class WriteLevelToFileScript : MonoBehaviour
{
    public Button button;
    public Text buttonText;

    private string filePath;
    private Player playerSetting;
    public void SaveButtonTextToFile()
    {
        string directoryPath = Path.Combine(Application.persistentDataPath, "DB");
        string filePath = Path.Combine(directoryPath, "PlayerSetting.txt");

        // Create folder if does not exist
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            Debug.Log("Created directory: " + directoryPath);
        }
        // Create file if does not exist
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close(); // Create empty file
            Debug.Log("File created: " + filePath);
        }

        if (button != null)
        {
            if (buttonText != null)
            {
                string textToSave = buttonText.text;
                textToSave = textToSave.ToLower();

                string prevSettingJson = File.ReadAllText(filePath);
                if(prevSettingJson.Length > 0)
                {
                    playerSetting = JsonUtility.FromJson<Player>(prevSettingJson);
                    playerSetting.Level = textToSave;
                }else
                {
                    playerSetting = new Player
                    {
                        Level = textToSave,
                        Score = 0,
                        HighScore = 0,
                        TypingTime = 0,
                    };

                }
                string currentSettingJson = JsonUtility.ToJson(playerSetting, true);
                Debug.Log("current setting: " + currentSettingJson);

                File.WriteAllText(filePath, currentSettingJson);

            }
            else
            {
                Debug.LogWarning("Text is null, please assign.");
            }
        }
        
    }
}
