using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            if(Input.inputString.Length > 0)
            {
                Debug.Log("key pressed: " + Input.inputString);
                Shoot();
            }
        }
            
    }

    private void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
