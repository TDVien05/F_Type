using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Script.GamePlay.TextControl
{
    public class TextController : MonoBehaviour
    {
        public float speed = 0.3f; // Toc do roi
        private TextMeshPro textMesh; // Text cua prefabs
        private Vector3 startPosition;
        private List<TextMeshPro> listWords = new List<TextMeshPro>();
        private Transform squareTransform;
        private SpawnSpace spawnSpace = new SpawnSpace();
        public float delayTime;
        private List<string> checkWords = new List<string>();
        private bool isDelaying = true;

        public void SetIsDelaying(bool isDelay)
        {
            this.isDelaying = isDelay;
        }

        private void Start()
        {
            Delay();
        
            // Lay textMeshPro tu prefabs
            textMesh = GetComponentInChildren<TextMeshPro>();
            squareTransform = textMesh.transform.parent;
            if (textMesh != null)
            {
                startPosition = transform.position;
                GenerateWords(textMesh);
            }
        }

        private void Delay()
        {
            StartCoroutine(MovingTextDown());
        }

        public void SetDelayTime(float delayTime)
        {
            this.delayTime = delayTime;
        }

        private void Update()
        {
            if (textMesh.text == "") return;
            if (!isDelaying)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            } else Delay();
        }

        IEnumerator MovingTextDown()
        {
            yield return new WaitForSeconds(delayTime);
            isDelaying = false;
        }

        // Tao vi tri moi sau khi va cham Bullet, ...
        public void RandomSpace()
        {
            spawnSpace.checkSpawnSpaceX.Clear();
            float x = spawnSpace.RanDomX();
            float y = squareTransform.position.y;
            squareTransform.position = new Vector3(x, y);
            transform.position = squareTransform.position;
        }

        // Tao chu v mau ngau nhien
        public void GenerateWords(TextMeshPro textMesh)
        {
            speed += 0.3f;
            textMesh.text = GenerateRandomWord();
            textMesh.color = GenerateRandomColor();
        }

        // Tao chu ngau nhien
        private string GenerateRandomWord()
        {
            string[] randomWords = { "apple", "banana", "cherry", "dream", "eagle", "forest", "garden", "honey", "island", "jungle",
                "kangaroo", "lemon", "mountain", "nebula", "ocean", "planet", "queen", "river", "sunshine", "tiger",
                "umbrella", "valley", "whale", "white", "yacht", "zebra", "adventure", "blossom", "cloud", "diamond",
                "energy", "freedom", "galaxy", "horizon", "imagine", "jewel", "kindness", "lighthouse", "miracle", "nature",
                "oasis", "paradise", "quest", "rainbow", "starlight", "tranquility", "unity", "voyage", "wisdom", "zenith" };
            int randomIndex = Random.Range(0, randomWords.Length);
            string randomWord = randomWords[randomIndex];
            do
            {
                if (checkWords.Contains(randomWord))
                {
                    randomIndex = Random.Range(0, randomWords.Length);
                    randomWord = randomWords[randomIndex];
                }
                else
                {
                    checkWords.Add(randomWord);
                    break;
                }
            } while (checkWords.Contains(randomWord));
            return randomWords[randomIndex];
        }

        // Tao mau ngau nhien (chi chua cac mau sang)
        private Color GenerateRandomColor()
        {
            float r = Random.Range(0.5f, 1f);
            float g = Random.Range(0.5f, 1f);
            float b = Random.Range(0.5f, 1f);

            return new Color(r, g, b);
        }
    }
}
