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
        if (!_isRunning) return;
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
}
