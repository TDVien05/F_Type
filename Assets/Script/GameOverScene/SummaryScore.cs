using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;
using GameLogic;
using System.IO;
public class SummaryScore : MonoBehaviour
{
    public TMP_Text ScoreText;
    public TMP_Text HighScoreText;
    public TMP_Text AccuracyText;
    public TMP_Text TypingText;
    private Player playerSetting;
    private string settings;
    private string filePath;


    // Start is called before the first frame update
    void Start()
    {
        filePath = "DB\\PlayerSetting.txt";
        settings = File.ReadAllText(filePath);
        if (settings.Length > 0)
        {
            playerSetting = JsonUtility.FromJson<Player>(settings);
            ShowResult();
        }
        else
            Debug.Log("Setting not found");
    }

    private void ShowResult()
    {
        int Score = playerSetting.Score;
        int HighScore = playerSetting.HighScore;
        float acc =(float)Math.Round(playerSetting.Accuracy, 2) ;
        int typingTime = (int)Math.Ceiling(playerSetting.TypingTime);
        if (Score > HighScore && playerSetting.Level != "paragraph") 
        {
            ScoreText.text = HighScoreText.text = Score.ToString();
            playerSetting.HighScore = Score;
            settings = JsonUtility.ToJson(playerSetting, true);
            File.WriteAllText(filePath, settings);
            Debug.Log("Save new high score");
        }else
        {
            ScoreText.text = Score.ToString();
            HighScoreText.text = HighScore.ToString();
        }
        if (acc >= 0)
        {
            AccuracyText.text = acc.ToString();
        }

        

        switch (playerSetting.Level)
        {
            case "30s":
                if (typingTime > 30)
                {
                    TypingText.text = "30";
                }
                else
                {
                    if (typingTime >= 0)
                    {
                        TypingText.text = typingTime.ToString();
                    }
                }
                break;
            case "60s":
                if (typingTime > 60)
                {
                    TypingText.text = "30";
                }
                else
                {
                    if (typingTime >= 0)
                    {
                        TypingText.text = typingTime.ToString();
                    }
                }
                break;
            default:
                if (typingTime >= 0)
                {
                    TypingText.text = typingTime.ToString();
                }
                break;
        }
    }
}
