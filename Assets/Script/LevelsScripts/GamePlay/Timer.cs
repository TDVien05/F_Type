using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
public class Timer : MonoBehaviour
{
    public float time;
    public TMP_Text text;

    public Canvas canvas;
    private string filePath;

    public Transform timerObject;
    // Start is called before the first frame update
    void Start()
    {
        filePath = "DB\\Level.txt";
        LoadSceneSetting();
    }

    private void LoadSceneSetting()
    {
        try
        {
            string SceneName = File.ReadAllText(filePath);
            switch(SceneName)
            {
                case "30s":
                    Debug.Log(SceneName);
                    time = 30;
                    text.text = Mathf.Ceil(time).ToString();
                    break;
                case "60s":
                    Debug.Log(SceneName);
                    time = 60;
                    text.text = Mathf.Ceil(time).ToString();
                    break;
                default:
                    Debug.Log(SceneName);
                    DisableTimerGameObject();
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
            ChangeScene timeOver = FindObjectOfType<ChangeScene>();
            if (timeOver != null) 
            {
                timeOver.change();
            }

            SaveCurrentScore();
        
    }
    private void SaveCurrentScore()
    {
        Score currentScore = FindObjectOfType<Score>();
        if (currentScore != null)
        {
            File.WriteAllText("DB\\CurrentScore.txt", currentScore.GetScore().ToString());
            Debug.Log("Save current score: " + currentScore.GetScore());
        }

    }
}
