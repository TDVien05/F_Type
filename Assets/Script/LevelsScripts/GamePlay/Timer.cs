using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using GameLogic;
using Script.LevelsScripts.GamePlay;

public class Timer : MonoBehaviour
{
    private SpawnParagraph spawnParagraph;

    public float time;
    public TMP_Text text;

    private string _filePath;
    private Player _playerSetting; // player object to store json setting
    public Transform timerObject;
    public ChangeScene timeOver; 
    public Score currentScore;

    private bool _isRunning;

    // Start is called before the first frame update
    void Start()
    {
        _isRunning = true;
        _filePath = "DB\\PlayerSetting.txt";
        LoadSceneSetting();
    }

    // Load current level settings
    private void LoadSceneSetting()
    {
        try
        {
            string settings = File.ReadAllText(_filePath);
            if(settings.Length > 0)
            {
                _playerSetting = JsonUtility.FromJson<Player>(settings);
            }else
            {
                _playerSetting = new Player
                {
                    Level = "30s"
                };

            }

            // switch to specific level
            switch(_playerSetting.Level)
            {
                case "30s":
                    Debug.Log(_playerSetting.Level);
                    time = 30;
                    text.text = Mathf.Ceil(time).ToString();
                    break;
                case "60s":
                    Debug.Log(_playerSetting.Level);
                    time = 60;
                    text.text = Mathf.Ceil(time).ToString();
                    break;
                case "Paragraph":
                    Debug.Log(_playerSetting.Level);
                    time = 10;
                    text.text = Mathf.Ceil(time).ToString();
                    break;
                default:
                    Debug.Log(_playerSetting.Level); 
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
        spawnParagraph = GetComponent<SpawnParagraph>();
        Debug.Log("Current Level: " + _playerSetting.Level);
        if (_playerSetting.Level == "Paragraph")
        {
            //if (!_isRunning) return;
            time += Time.deltaTime;
                Debug.Log("Da vao Paragraph");

            // Cap nhat diem so
            text.text = Mathf.Ceil(time).ToString();
            
            if (spawnParagraph.completedText)
            {
                SaveTotalTime();
                Debug.Log("Text is completed!");
                End();
            }
        }
        else 
        {
            if (!_isRunning) return;
                time -= Time.deltaTime;
                //Debug.Log("TypingTime: " + time);
            if (time <= 0)
            {
                text.text = "0";
                End();
            }else
            {
                text.text = Mathf.Ceil(time).ToString();
            }
        }
        
    }

    public void TimeStart()
    {
        _isRunning = true;
    }

    public void TimePause()
    {
        _isRunning = false;
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
            _playerSetting.Score = currentScore.GetScore();
            string currentSetting = JsonUtility.ToJson(_playerSetting, true);
            File.WriteAllText(_filePath,currentSetting);
        }
    }
    private void SaveTotalTime()
    {
        // Cap nhat thoi gian hoan thanh 
        _playerSetting.TypingTime = time;
        SaveCurrentScore();
    }
}
