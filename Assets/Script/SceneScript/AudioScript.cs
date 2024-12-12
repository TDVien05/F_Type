using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        audioSource.Play();
        audioSource.volume = 0.2f;
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
