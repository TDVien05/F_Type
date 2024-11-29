using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
public class Timer : MonoBehaviour
{
    public float time;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = Mathf.Ceil(time).ToString();
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
        GameObject bg1 = GameObject.Find("Bg1");
        GameObject bg2 = GameObject.Find("Bg2");
        GameObject firePoint = GameObject.Find("FirePoint");

        if (bg1 != null && bg2 != null && firePoint != null)
        {
            RepeatBg rbg = bg1.GetComponent<RepeatBg>();
            if (rbg != null)
            {
                Rigidbody2D rb1 = rbg.GetComponent<Rigidbody2D>();
                if (rb1 != null)
                {
                    rb1.velocity = Vector2.zero;
                }

            }

            RepeatBg rbg2 = bg2.GetComponent<RepeatBg>();
            if (rbg2 != null)
            {
                Rigidbody2D rb2 = rbg2.GetComponent<Rigidbody2D>();
                if (rb2 != null)
                {
                    rb2.velocity = Vector2.zero;
                }
            }

            Shooting firePointScript = firePoint.GetComponent<Shooting>();
            if (firePointScript != null)
            {
                firePointScript.enabled = false;
            }


            ChangeScene timeOver = FindObjectOfType<ChangeScene>();
            if (timeOver != null) 
            {
                timeOver.change();
            }

            SaveCurrentScore();
        }
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
