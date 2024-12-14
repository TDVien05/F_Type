    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using System.IO;
    using System;
    using GameLogic;
    using Script.LevelsScripts.GamePlay;

    public class Timer : MonoBehaviour
    {
        public float time;
        public TMP_Text text;
        private GameObject spawnTextBase; // Base use to spawn text in 30s, 60s, and failure
        private string _filePath;
        private Player _playerSetting; // player object to store json setting
        public Transform timerObject;
        public ChangeScene timeOver; 
        public Score currentScore;
        private bool _isParagraphLevel = false;
        private bool _isRunning;
        public PlayerController _playerController;
        public ParagraphPlayerController _paragraphPlayerController;
        
        
        // Start is called before the first frame update
        void Start()
        {
            _isRunning = true;
            spawnTextBase = GameObject.Find("Base");
            if (spawnTextBase == null)
            {
                Debug.LogError("No Base");
            } else Debug.Log("Get spawn base successfully.");
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
                        _isParagraphLevel = false;
                        _paragraphPlayerController.enabled = false;
                        break;
                    case "60s":
                        Debug.Log(_playerSetting.Level);
                        time = 60;
                        text.text = Mathf.Ceil(time).ToString();
                        _paragraphPlayerController.enabled = false;
                        _isParagraphLevel = false;
                        break;
                    case "paragraph":
                        Debug.Log(_playerSetting.Level);
                        time = 0;
                        _playerController.enabled = false; 
                        _paragraphPlayerController.enabled = true;
                        currentScore.gameObject.SetActive(false);
                        text.text = Mathf.Ceil(time).ToString();
                        spawnTextBase.SetActive(false);
                        _isParagraphLevel = true;
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
            if (!_isParagraphLevel)
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
            else
            {
                time += Time.deltaTime;
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
            }
            else
            {
                _playerSetting.TypingTime = 0;
                acc = _playerController.GetComponent<Accuracy>().CalculateAccuracy();
                    ;
            }

            _playerSetting.Accuracy =  (float)Math.Round(acc, 2);
            string currentSetting = JsonUtility.ToJson(_playerSetting, true);
            File.WriteAllText(_filePath,currentSetting);
        }
    }
