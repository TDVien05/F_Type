using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace GameLogic
{
    [System.Serializable]
    public class Player
    {
        public string Level;
        public int Score;
        public int HighScore;
        public float TypingTime;
    }

    [System.Serializable]
    public class Obstacle
    {
        public Text Text { get; set; }
        public string LeftoverText { get; set; }

        public bool HasNextText()
        {
            return LeftoverText != "";
        }
    }
}
