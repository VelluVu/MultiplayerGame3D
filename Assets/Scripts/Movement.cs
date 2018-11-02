using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Photon.MonoBehaviour {

    float moveSpeed;
    float rotationSpeed;
    float jumpForce;
    float bulletforceZ;
    float nextShot;

    bool readyToFire;
    bool onGround;
    Rigidbody rb;

    public GameObject gunRotator;
    public Ammo ammo;
    public Transform firePoint;

    public Camera myCam;

    void Start() {
        readyToFire = true;
        jumpForce = 5f;
        moveSpeed = 5f * Time.deltaTime;
        rotationSpeed = 5f;
        rb = gameObject.GetComponent<Rigidbody>();
        bulletforceZ = 15f;

        if (photonView.isMine)
        {

            myCam.enabled = true;

        }

    }

    private void Update()
    {
        if (photonView.isMine)
        {
            PlayerMove();
            Jump();
            Shoot();
        }
    }

    private void Shoot()
    {

        if (Input.GetMouseButton(0) && readyToFire && Time.time > nextShot)
        {

            nextShot = Time.time + 1;
            Ammo ammoInstance = Instantiate(ammo, firePoint.position, firePoint.rotation) as Ammo;
            ammoInstance.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * 10, ForceMode.Impulse);
            readyToFire = false;
            photonView.RPC("ShootBall", PhotonTargets.Others, firePoint.transform.position, firePoint.transform.forward);


        }
        else
        {
            readyToFire = true;
        }

    }



    private void PlayerMove()
    {
        //Tallennetaan muuttujiin x ja z axis liike
        float xMovement = Input.GetAxis("Horizontal") * moveSpeed;
        //transform.Rotate(0, rotationSpeed * Input.GetAxis("Horizontal"), 0);
        float zMovement = Input.GetAxis("Vertical") * moveSpeed;
        transform.Translate(xMovement, 0, zMovement);

        //Tallennetaan muuttujaan hiiren X-liike
        float mouseInput = Input.GetAxis("Mouse X");
        Vector3 lookHere = new Vector3(0, mouseInput * rotationSpeed, 0);
        transform.Rotate(lookHere);

        float mouseInputy = Input.GetAxis("Mouse Y");
        Vector3 moveGun = new Vector3(-mouseInputy * rotationSpeed, 0, 0);
        gunRotator.transform.Rotate(moveGun);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            onGround = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            onGround = true;
        }
    }


    [PunRPC]
    void ShootBall(Vector3 location, Vector3 forward)
    {

        Ammo ammoInstance = Instantiate(ammo, location, Quaternion.identity) as Ammo;
        ammoInstance.GetComponent<Rigidbody>().AddForce(forward * 10, ForceMode.Impulse);

    }

    /*
      Kotitehtävä:

        Anna kaikille pelaajille health parametri ja 
        jonkun muun oman pallon osuessa pelaajaan , 
        vähennetään healthiä kun health on 0, 
        kuollut pelaaja siirretään aloituspisteeseen 
        ja health palautetaan 100:aan.
     */
}
