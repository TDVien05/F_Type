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
        filePath = Path.Combine(Application.persistentDataPath, "DB", "PlayerSetting.txt");

        if (button != null)
        {
            if (buttonText != null)
            {
                string textToSave = buttonText.text;
                textToSave = textToSave.ToLower();

                // Ensure the directory exists
                string directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                if (File.Exists(filePath))
                {
                    string prevSettingJson = File.ReadAllText(filePath);

                    if (!string.IsNullOrEmpty(prevSettingJson))
                    {
                        try
                        {
                            playerSetting = JsonUtility.FromJson<Player>(prevSettingJson);
                        }
                        catch
                        {
                            Debug.LogWarning("Invalid JSON format in file. Creating a new player setting.");
                            playerSetting = CreateDefaultPlayerSetting(textToSave);
                        }
                    }
                    else
                    {
                        Debug.Log("File exists but is empty. Creating a new player setting.");
                        playerSetting = CreateDefaultPlayerSetting(textToSave);
                    }
                }
                else
                {
                    Debug.Log("File does not exist. Creating a new player setting.");
                    playerSetting = CreateDefaultPlayerSetting(textToSave);
                }

                playerSetting.Level = textToSave;
                string currentSettingJson = JsonUtility.ToJson(playerSetting, true);
                Debug.Log("Current setting: " + currentSettingJson);

                File.WriteAllText(filePath, currentSettingJson);
            }
            else
            {
                Debug.LogWarning("Text is null, please assign.");
            }
        }
    }

    private Player CreateDefaultPlayerSetting(string level)
    {
        return new Player
        {
            Level = level,
            Score = 0,
            HighScore = 0,
            TypingTime = 0,
        };
    }
}
