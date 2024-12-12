using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Script.LevelsScripts.GamePlay
{
    public class ParagraphPlayerController : PlayerController
    {
        private int _mistake;
        private string _paragraph; 
        private string _nextChar;
        private List<GameObject> _obstalceList;
        private int index;

        // Start is called before the first frame update
        void Start()
        {
            _paragraph = GetParagraph();
            _mistake = 0;
            _nextChar = _paragraph.Substring(0,1);
            _paragraph = _paragraph.Substring(1);
            index = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.anyKeyDown)
            {
                string inputKey = Input.inputString;
                CheckInputKey(inputKey);
            }
        }

        private string GetParagraph()
        {
            return "";
        }
        
        // protected void RotatePlayerTowardsTarget(GameObject target)

        private void CheckInputKey(string inputKey)
        {
            if (inputKey == _nextChar)
            {
                RotatePlayerTowardsTarget(_obstalceList[index]);
                Shoot();
            }
            else
            {
                RotatePlayerTowardsTarget(_obstalceList[index]);
                _mistake++;
                Debug.Log("Wrong key");
            }

            if (_paragraph.Length > 0)
            {
                _nextChar = _paragraph.Substring(0,1);
                _paragraph = _paragraph.Substring(1);
            }
            else
            {
                index++;
            }
        }
    
    }
}
