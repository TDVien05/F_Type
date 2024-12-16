using System;
using System.Collections;
using System.IO;
using GameLogic;
using Script.LevelsScripts.GamePlay;
using TMPro;
using UnityEngine;

namespace Script.ProgressionScripts.GamePlay.Properties
{
    public class Timer : MonoBehaviour
    {
        public float time;
        private float _maxTime = 10000f;
        private float _currentTime = 0f;
        public TMP_Text text;
        private GameObject spawnBase; // Base use to spawn text in 30s, 60s, failure and paragraph
        private string _filePath;
        private Player _playerSetting; // player object to store json setting
        public Transform timerObject;
        public ChangeScene timeOver; 
        public Score currentScore;
        private bool _isParagraphLevel = false;
        private bool _isRunning;
        public PlayerController.PlayerController _playerController;
        public ParagraphPlayerController _paragraphPlayerController;
        private bool _isFailureMode;

        private bool isPaused = false;
        // Start is called before the first frame update
        void Start()
        {
            _isRunning = true;
            spawnBase = GameObject.Find("Base");
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
                        _paragraphPlayerController.enabled = false;
                        spawnBase.GetComponent<ParagraphSpawn>().enabled = false;
                        _isParagraphLevel = false;
                        _isFailureMode = false;
                        break;
                    case "60s":
                        Debug.Log(_playerSetting.Level);
                        time = 60;
                        text.text = Mathf.Ceil(time).ToString();
                        _paragraphPlayerController.enabled = false;
                        spawnBase.GetComponent<ParagraphSpawn>().enabled = false;
                        _isParagraphLevel = false;
                        _isFailureMode = false;
                        break;
                    case "paragraph":
                        Debug.Log(_playerSetting.Level);
                        time = 0;
                        _playerController.enabled = false; 
                        _paragraphPlayerController.enabled = true;
                        currentScore.gameObject.SetActive(false);
                        text.text = Mathf.Ceil(time).ToString();
                        spawnBase.GetComponent<SpawnSpace>().enabled = false;
                        _isParagraphLevel = true;
                        break;
                    default:
                        Debug.Log(_playerSetting.Level); 
                        spawnBase.GetComponent<ParagraphSpawn>().enabled = false;
                        _paragraphPlayerController.enabled = false;
                        _isParagraphLevel = false;
                        _isFailureMode = true;
                        time = 0;
                        text.text = Mathf.Ceil(time).ToString();
                        
                        break;  
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        
        // Update is called once per frame
        void Update()
        {
            if (!_isRunning) return;
            if (!_isParagraphLevel && !_isFailureMode)
            {
                time -= Time.deltaTime;
                _currentTime += Time.deltaTime;
                if (time <= 0)
                {
                    text.text = "0";
                    End();
                }else
                {
                    text.text = Mathf.Ceil(time).ToString();
                }
            }
            else
            {
                time += Time.deltaTime;
                text.text = Mathf.Ceil(time).ToString();
                if (Mathf.Approximately(time, _maxTime))
                {
                    End();
                    Debug.Log("Reach max time");
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
        
        
        public void End()
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
            float acc = 0;
            if (currentScore != null)
            {
                _playerSetting.Score = currentScore.GetScore();
            }
            if (_playerSetting.Level == "paragraph")
            {
                _playerSetting.TypingTime = time;
                acc = _paragraphPlayerController.GetComponent<Accuracy>().CalculateAccuracy();
            }else if (_playerSetting.Level == "failure")
            {
                _playerSetting.TypingTime = time;
                acc = _playerController.GetComponent<Accuracy>().CalculateAccuracy();
            }
            else
            {
                _playerSetting.TypingTime = _currentTime;
                acc = _playerController.GetComponent<Accuracy>().CalculateAccuracy();
            }

            _playerSetting.Accuracy =  (float)Math.Round(acc, 2);
            string currentSetting = JsonUtility.ToJson(_playerSetting, true);
            File.WriteAllText(_filePath,currentSetting);
        }
    }
}
