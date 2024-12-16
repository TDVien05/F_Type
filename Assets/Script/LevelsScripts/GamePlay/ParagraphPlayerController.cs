using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Script.LevelsScripts.GamePlay
{
    public class ParagraphPlayerController : MonoBehaviour
    {
        public GameObject player;
        public GameObject bullet;
        public Transform firePoint;
        public  ParagraphSpawn _paragraphSpawn;
        public  Timer timer;
        private string _paragraphWord; 
        private string _nextChar;
        private List<GameObject> _obstalceList;
        private int index;
        private Accuracy acc;
        public AudioSource shootAudio;
        public AudioSource warningAudio;
        
        
        // Start is called before the first frame update
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f); // wait for text generation
            _obstalceList = _paragraphSpawn.GetListWords();
            shootAudio.volume = 0.1f;
            warningAudio.volume = 0.5f;
            acc = GetComponent<Accuracy>();
            index = 0;
            Debug.Log("number of words: " + _obstalceList.Count);
            if (_obstalceList.Count > 0)
            {
                _paragraphWord = _obstalceList[index].GetComponentInChildren<TMP_Text>().text;
                _nextChar = _paragraphWord.Substring(0,1);
                Debug.Log("new key: " + _nextChar);
            }
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
        
        // check player input key
        private void CheckInputKey(string inputKey)
        {
            // Ignore Caps Lock key input
            if (Input.GetKeyDown(KeyCode.CapsLock))
            {
                Debug.Log("Caps Lock key pressed, ignoring.");
                return;
            }
            if (inputKey == _nextChar)
            {
                acc.SetCorrectChar(1);
                RotatePlayerTowardsTarget(_obstalceList[index].GetComponentInChildren<TMP_Text>().gameObject);
                Shoot();
            }
            else
            {
                RotatePlayerTowardsTarget(_obstalceList[index].GetComponentInChildren<TMP_Text>().gameObject);
                acc.SetIncorrectChar(1);
                Debug.Log("Wrong key");
                warningAudio.Play();
            }
            UpdateWord();
            
        }

        void UpdateWord()
        {
            if (_paragraphWord.Length > 1)
            {
                // Remove the first character and update the current word
                _obstalceList[index].GetComponentInChildren<TMP_Text>().text = _paragraphWord.Substring(1);
            }
            else
            {
                // Clear the word in the current obstacle
                _obstalceList[index].GetComponentInChildren<TMP_Text>().text = "";
            }

            // Get the remaining part of the current paragraph word
            _paragraphWord = _obstalceList[index].GetComponentInChildren<TMP_Text>().text;

            if (_paragraphWord.Length > 0)
            {
                // If there are remaining characters, update new key input 
                _nextChar = _paragraphWord.Substring(0, 1);
                Debug.Log("New key: " + _nextChar);
            }
            else
            {
                index++;
                // check remaining text in current paragraph
                if (index >= _obstalceList.Count)
                {
                    Debug.Log("End of paragraph");
                    _paragraphSpawn.SetWordsToPrefabs();
                    // Check if no more paragraphs can be generated
                    if (_paragraphSpawn.IsEnd())
                    {
                        Debug.Log("No more words in paragraph.");
                        timer.End();
                        return;
                    }

                    // Reset for the next set of words
                    index = 0;
                    
                }

                _paragraphWord = _obstalceList[index].GetComponentInChildren<TMP_Text>().text;
                if (_paragraphWord.Length > 0)
                {
                    _nextChar = _paragraphWord.Substring(0, 1);
                }
            }
        }


        void GetNewListWord()
        {
            _paragraphSpawn.ResetIndexPosition();
            _paragraphSpawn.SpawnAllParagraphs();
            _obstalceList = _paragraphSpawn.GetListWords();
        }


        private void RotatePlayerTowardsTarget(GameObject target)
        {
            // Calculate direction to the target
            Vector2 directionToTarget = (target.transform.position - player.transform.position).normalized;

            // Get the angle in degrees relative to the player's current forward direction
            float angleToTarget = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            angleToTarget -= 90;
            
            // Apply rotation
            player.transform.rotation = Quaternion.Euler(0, 0, angleToTarget);
        }

        private void Shoot()
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            shootAudio
                .Play();
        }
    }
}
