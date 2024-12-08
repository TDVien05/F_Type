using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameLogic;
using System.IO;
public class SummaryScore : MonoBehaviour
{
    public TMP_Text ScoreText;
    public TMP_Text HighScoreText;
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
        if (Score > HighScore) 
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
    }
}
