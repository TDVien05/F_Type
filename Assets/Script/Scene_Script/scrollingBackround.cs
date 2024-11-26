using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingBackround : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private Renderer bg;

    void Update()
    {
        bg.material.mainTextureOffset += new Vector2((-1) * speed * Time.deltaTime, 0);
    }

}
