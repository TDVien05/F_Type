using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Script.LevelsScripts.GamePlay
{
    public class ParagraphController : MonoBehaviour
    {
        public float speed = 0.001f; // Toc do roi
        private TextMeshPro textMesh; // Text cua prefabs
        //private Vector3 startPosition;
        //private List<TextMeshPro> listWords = new List<TextMeshPro>();
        private Transform squareTransform;
        private SpawnSpace spawnSpace = new SpawnSpace();
        private float delayTime;
        //private List<string> checkWords = new List<string>();
        private bool isDelaying;
        public bool completedText;

        //public void SetIsDelaying(bool isDelay)
        //{
        //    this.isDelaying = isDelay;
        //}

        private void Start()
        {
            Delay();

            // Lay textMeshPro tu prefabs
            textMesh = GetComponentInChildren<TextMeshPro>();
            squareTransform = textMesh.transform.parent;
            if (textMesh != null)
            {
                //startPosition = transform.position;
                GenerateWords(textMesh);
            }
        }

        private void Delay()
        {
            isDelaying = true;
            StartCoroutine(MovingTextDown());
        }

        //public void SetDelayTime(float delayTime)
        //{
        //    this.delayTime = delayTime;
        //}

        private void Update()
        {
            if (!isDelaying)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            else Delay();
        }

        IEnumerator MovingTextDown()
        {
            yield return new WaitForSeconds(delayTime);
            isDelaying = false;
        }

        // Tao vi tri moi sau khi va cham Bullet, ...
        //public void RandomSpace()
        //{
        //    spawnSpace.checkSpawnSpaceX.Clear();
        //    float x = spawnSpace.RanDomX();
        //    float y = squareTransform.position.y;
        //    squareTransform.position = new Vector3(x, y);
        //    transform.position = squareTransform.position;
        //}

        // Tao chu theo chu de
        public void GenerateWords(TextMeshPro textMesh)
        {
            if (!completedText)
            {
                string nextWord = GetNextWord();
                if (nextWord != null)
                {
                    textMesh.text = nextWord;
                    textMesh.color = GenerateRandomColor();
                }
            }
        }

        // Tao mau ngau nhien (chi chua cac mau sang)
        private Color GenerateRandomColor()
        {
            float r = Random.Range(0.5f, 1f);
            float g = Random.Range(0.5f, 1f);
            float b = Random.Range(0.5f, 1f);

            return new Color(r, g, b);
        }

        //Lua chon chu de ngau nhien 
        public string SelectedTopic()
        {
            string theme = "";            
            switch (Random.Range(1, 5))
            {
                case 1:
                    theme = "Noi dung chu de mot co khoang hai tram chu";
                    break;
                case 2:
                    theme = "Noi dung chu de hai co khoang hai tram chu";
                    break;
                case 3:
                    theme = "Noi dung chu de ba co khoang hai tram chu";
                    break;
                case 4:
                    theme = "Noi dung chu de bon co khoang hai tram chu";
                    break;
            }
            return theme;
        }

        //Tao chu cua chu de
        private string GetNextWord()
        {
            int indexOfWord = 0;
            string[] word = SelectedTopic().Split(' ');

            // 
            if (indexOfWord < word.Length)
            {
                string nextWord = word[indexOfWord];
                indexOfWord++;
                return nextWord;
            }
            else
            {
                completedText = true;
                return null;
            }                
            //return word[indexOfWord];
        }

    }
}
