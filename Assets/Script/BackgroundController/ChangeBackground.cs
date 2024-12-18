using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using GameLogic;
public class ChangeBackground : MonoBehaviour
{
    private SpriteRenderer Bg1Render;
    private SpriteRenderer Bg2Render;

    public Sprite newSprite1;
    public Sprite newSprite2;

    public GameObject bg1;
    public GameObject bg2;
    private string filePath;
    private string settings;
    private Player playerSetting;
    // Start is called before the first frame update
    void Start()
    {
        string directoryPath = Path.Combine(Application.persistentDataPath, "DB");
        filePath = Path.Combine(directoryPath, "PlayerSetting.txt");
        Bg1Render = bg1.GetComponent<SpriteRenderer>();
        Bg2Render = bg2.GetComponent<SpriteRenderer>();
        ChangeBg();
    }

    private void ChangeBg()
    {
        try
        {
            settings =  File.ReadAllText(filePath);
            if (settings.Length > 0) 
            {
                playerSetting = JsonUtility.FromJson<Player>(settings);
            }
            switch(playerSetting.Level)
            {
                case "failure":
                    Bg1Render.sprite = newSprite1;
                    Bg2Render.sprite = newSprite2;
                    bg1.AddComponent<RepeatBg>();
                    bg2.AddComponent<RepeatBg>();
                    break;
            }
        }catch(Exception e) {
            Debug.Log(e);
        }
    }
}
