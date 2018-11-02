using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    Rigidbody ammoRb;
    float forcez;
    float projectileLifeTime;

    private void Start()
    {

        projectileLifeTime = 5f;

        /*ammoRb = gameObject.GetComponent<Rigidbody>();

        ammoRb.AddRelativeForce(Vector3.forward * forcez, ForceMode.Impulse);
        */
        Destroy(gameObject, projectileLifeTime);
    }
    /*
    public void SetForceZ(float force)
    {
        forcez = force;
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject);
    }
}
