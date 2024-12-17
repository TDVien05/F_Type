using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Script.GamePlay.ParagraphControl
{
    public class ParagraphSpawn : MonoBehaviour
    {
        public GameObject paragraphPrefabs;// Gan prefabs
        public Transform spawnPoint;// Diem spawn
        private int numberOfPrefabs = 5;// So luong prefabs muon spawn 
        private List<GameObject> spawnedparagraphs = new List<GameObject>();// Mang luu cac prefabs da spawn
        private int indexOfPositions = 0;
        private float[] positions = new float[5]
        {
            -7.45f, -3.96f, -0.58f, 3.33f, 6.75f
        };
        private string[] wordsList;
        private int indexOfWordsList = 0;
        private bool _isEnd;
        void Start()
        {
            CreateWordsList();
            SpawnAllParagraphs();
            _isEnd = false;
            // SetWordsToPrefabs();
        }

        // Ham tao words list
        public void CreateWordsList()
        {
            int random = Random.Range(1, 3);
            string list = ChooseTopic(random);
            wordsList = list.Split(' ');
        }

        // Spawn tat ca prefabs
        public void SpawnAllParagraphs()
        {
            for (int i = 0; i < numberOfPrefabs; i++)
            {
                GameObject paragraph = Spawn();
                spawnedparagraphs.Add(paragraph);
            }
            Debug.Log("Spawned all paragraphs");
        }
    
        // Spawn paragraph prefab 
        GameObject Spawn()
        {
            float x = positions[indexOfPositions];
            indexOfPositions++;
            float y = spawnPoint.position.y;
            Vector3 position = new Vector3(x, y, 0);
            GameObject gameObject = Instantiate(paragraphPrefabs, position, Quaternion.identity);
            SetWord(gameObject.GetComponentInChildren<TMP_Text>());
            return gameObject;
        }

        // Random lay topic
        string ChooseTopic(int number)
        {
            string topic = "Food is an essential part " +
                           "of life that provides the " +
                           "energy and nutrients our bodies " +
                           "need to function. It comes " +
                           "in a variety of flavors, " +
                           "textures, and cultural styles, making " +
                           "it a source of joy " +
                           "and creativity. From fresh fruits " +
                           "and vegetables to hearty meals " +
                           "and sweet desserts, food brings " +
                           "people together, whether at family " +
                           "dinners, celebrations, or casual outings. " +
                           "It reflects the traditions and " +
                           "history of a culture, with " +
                           "dishes passed down through generations. " +
                           "Beyond nutrition, food is also " +
                           "an art, allowing chefs and " +
                           "home cooks to express themselves " +
                           "through flavors and presentation. Sharing " +
                           "food creates bonds and unforgettable " +
                           "memories. I love chicken breast!";
            switch (number)
            {
                case 1:
                    topic = "Family is the foundation of " +
                            "love, support, and understanding in " +
                            "our lives. It consists of " +
                            "the people we trust and " +
                            "rely on, including parents, siblings, " +
                            "and extended relatives. A family " +
                            "provides emotional support during difficult " +
                            "times and celebrates achievements together. " +
                            "They teach us values, traditions, " +
                            "and life lessons that shape " +
                            "our identity. Spending quality time " +
                            "with family strengthens relationships and " +
                            "creates lasting memories. Family bonds " +
                            "are built on trust, communication, " +
                            "and care. No matter where " +
                            "we are, the love of " +
                            "family provides comfort and a " +
                            "sense of belonging. It is " +
                            "a source of strength that " +
                            "lasts a lifetime. Love family.";
                    break;  
                case 2:
                    topic = "A dream is a series " +
                            "of thoughts, images, or feelings " +
                            "that occur in our minds " +
                            "while we sleep. Dreams can " +
                            "be mysterious, inspiring, or even " +
                            "puzzling. They often reflect our " +
                            "desires, fears, and emotions, offering " +
                            "insights into our subconscious. Some " +
                            "dreams are vivid and memorable, " +
                            "while others fade quickly after " +
                            "we wake up. People have always " +
                            "been fascinated by dreams, leading " +
                            "to interpretations and theories about " +
                            "their meanings. Beyond sleep, a " +
                            "dream can also mean our " +
                            "aspirations and goals in life. " +
                            "Pursuing our dreams requires determination, " +
                            "creativity, and belief in ourselves, " +
                            "turning imagination into reality. Good dream!";
                    break;
                case 3: 
                    topic = "Being patient means staying calm " +
                            "and understanding in situations that " +
                            "require waiting, enduring challenges, or " +
                            "facing difficulties. Patience is a " +
                            "valuable quality that helps us " +
                            "handle stress and maintain a " +
                            "positive attitude. It allows us " +
                            "to make thoughtful decisions instead " +
                            "of reacting impulsively. Whether we " +
                            "are dealing with people, learning " +
                            "new skills, or overcoming obstacles, " +
                            "patience plays a key role " +
                            "in achieving success. It also " +
                            "fosters better relationships, as it " +
                            "shows respect and empathy for " +
                            "others. Though it can be " +
                            "hard to practice at times, patience " +
                            "helps us grow stronger and " +
                            "more resilient in life. Goodbye!";
                    break;
            }
            return topic;
        }

        // Set cho all prefabs tu ban dau
        public void SetWordsToPrefabs()
        {
            Debug.Log("The size of array of prefabs : " + spawnedparagraphs.Count);
            Debug.Log("The size of words list : " + wordsList.Length);
            for (int i = 0; i < spawnedparagraphs.Count; i++)
            {
                SetWord(spawnedparagraphs[i].GetComponentInChildren<TMP_Text>());
            }
        }

        // Set chu tu topic vao prefabs
        public void SetWord(TMP_Text Text)
        {
            if (indexOfWordsList >= wordsList.Length)
            {
                _isEnd = true;
                return;
            }
            Debug.Log("The index of words list is : " + indexOfWordsList);
            Text.text = wordsList[indexOfWordsList];
            indexOfWordsList++;
        }

        public bool IsEnd() { return _isEnd; }
        // Lay ra chuoi topic
    
        public void ResetIndexPosition() { indexOfPositions = 0; }
        public List<GameObject> GetListWords()
        {
            return spawnedparagraphs;
        }
    }
}
