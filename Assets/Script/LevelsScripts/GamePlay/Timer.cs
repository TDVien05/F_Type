using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using GameLogic;
public class Timer : MonoBehaviour
{
    public float time;
    public TMP_Text text;

    private string filePath;
    private Player playerSetting; // player object to store json setting
    public Transform timerObject;
    public ChangeScene timeOver; 
    public Score currentScore;



    // Start is called before the first frame update
    void Start()
    {
        filePath = "DB\\PlayerSetting.txt";
        LoadSceneSetting();
    }

    // Load current level settings
    private void LoadSceneSetting()
    {
        try
        {
            string settings = File.ReadAllText(filePath);
            if(settings.Length > 0)
            {
                playerSetting = JsonUtility.FromJson<Player>(settings);
            }else
            {
                playerSetting = new Player
                {
                    Level = "30s"
                };

            }

            // switch to specific level
            switch(playerSetting.Level)
            {
                case "30s":
                    Debug.Log(playerSetting.Level);
                    time = 30;
                    text.text = Mathf.Ceil(time).ToString();
                    break;
                case "60s":
                    Debug.Log(playerSetting.Level);
                    time = 60;
                    text.text = Mathf.Ceil(time).ToString();
                    break;
                default:
                    Debug.Log(playerSetting.Level); 
                    DisableTimerGameObject(); // failure mode turn on
                    break;  
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private void DisableTimerGameObject()
    {
        try
        {
            timerObject.gameObject.SetActive(false);
        }catch(Exception e)
        {
            Debug.Log(e);
        }
    }
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            text.text = "0";
            End();
        }else
        {
            text.text = Mathf.Ceil(time).ToString();
        }
    }

    private void End()
    {
        Debug.Log("End");
        SaveCurrentScore();
        if (timeOver != null) 
        {
            timeOver.change();
        }
    }
    private void SaveCurrentScore()
    {
        if (currentScore != null)
        {
            playerSetting.Score = currentScore.GetScore();
            string currentSetting = JsonUtility.ToJson(playerSetting, true);
            File.WriteAllText(filePath,currentSetting);
        }

    }
}
