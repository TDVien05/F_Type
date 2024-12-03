using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float heal = 100f;
    public float damage = 50f;


    public float GetHeal() { return heal; }
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

    // if the obstacle die, change its position outside of the camera view
    private void Die()
    {
        ChangePosition();
        Score score = FindObjectOfType<Score>();
        if (score != null) 
        {
            score.UpdateScore(1);
        }
         
    }
   
    private void ChangePosition()
    {
        Vector3 newPosition = this.transform.position;
        newPosition.x *= 10;
        newPosition.y *= -10;

        this.transform.position = newPosition;
    }
}
