using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : MonoBehaviour
{
    public float heal = 100f;
    public float damage = 50f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("bullet"))
        {
            TakeDamage();
            Destroy(col.gameObject);
        }
    }

    private void TakeDamage()
    {
        heal -= damage;
        if (heal <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        Score score = FindObjectOfType<Score>();
        if (score != null) 
        {
            score.UpdateScore(1);
        }

    }
}
