using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class HighesScore : MonoBehaviour
{
    public TMP_Text text;
    int highestScore;
    int currentScore;

    void Start()
    {
        currentScore = GetCurrentScore();
        highestScore = GetHighestScore();
        ShowResult();
    }
    private int GetCurrentScore()
    {
        try
        {
            string result = File.ReadAllText("DB\\CurrentScore.txt");
            Debug.Log(result);
            if(result.Length > 0)
                return int.Parse(result);
            else
            {
                File.WriteAllText("DB\\CurrentScore.txt", "0");
                return 0;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return 0;
        }

    }

    private int GetHighestScore()
    {
        try
        {
            string result = File.ReadAllText("DB\\HighestScore.txt");
            Debug.Log(result);
            if(result.Length > 0)
                return int.Parse( result );
            else
            {
                File.WriteAllText("DB\\HighestScore.txt", "0");
                return 0;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return 0;
        }

    }


    private void ShowResult()
    {
        if (currentScore <= highestScore)
        {
            text.text = highestScore.ToString();
        }
        else
        {
            text.text = currentScore.ToString();
            File.WriteAllText("DB\\HighestScore.txt", currentScore.ToString());
        }
    }
}
