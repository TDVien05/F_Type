using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    // Start is called before the first frame update

    private int currentScore = 0;
    public TMP_Text text;
    // Update is called once per frame

    public void Start()
    {
        text.text = "0";
        currentScore = 0;
    }

    public void UpdateScore(int score) { 
        currentScore += score; 
        text.text = currentScore.ToString();
    
    }
    public int GetScore() {  return currentScore; }
}
