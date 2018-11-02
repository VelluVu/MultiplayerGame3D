using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    Rigidbody ammoRb;
    float forcez;
    float projectileLifeTime;
    bool hitT;

    private void Start()
    {
        hitT = false;
        projectileLifeTime = 5f;
      
        Destroy(gameObject, projectileLifeTime);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.collider.CompareTag("Player") && !hitT)
        {
            collision.gameObject.GetComponent<PlayerHP>().takeDamage(10);
            Debug.Log("hit");
            hitT = true;
            Destroy(gameObject);
            
        }       
    }
}
