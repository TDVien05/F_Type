using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    // Start is called before the first frame update

    private int _currentScore = 0;
    public TMP_Text text;
    // Update is called once per frame

    public void Start()
    {
        text.text = "0";
        _currentScore = 0;
    }

    public void UpdateScore(int score) { 
        _currentScore += score; 
        text.text = _currentScore.ToString();
    }
    public int GetScore() {  return _currentScore; }
}
