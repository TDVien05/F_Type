using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class CurrentScore : MonoBehaviour
{
    public TMP_Text text;

    void Start()
    {
        GetResult();
    }
    private void GetResult()
    {
        try
        {
            string result = File.ReadAllText("DB\\CurrentScore.txt");
            Debug.Log(result);
            text.text = result;
        }
        catch(Exception e){ 
            Debug.LogException(e);
        }

    }
}
