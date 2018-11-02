using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Photon.MonoBehaviour {

    float moveSpeed;
    float rotationSpeed;
    float jumpForce;
    float bulletforceZ;
    float nextShot;
    public Vector3 startPos;
    bool readyToFire;
    bool onGround;
    Rigidbody rb;

    public GameObject gunRotator;
    public GameObject ammo;
    public Transform firePoint;
    public Camera myCam;
    public PlayerHP myHP;
    public Canvas myInfoCanvas;

    void Start() {
        readyToFire = true;
        jumpForce = 5f;
        moveSpeed = 5f * Time.deltaTime;
        rotationSpeed = 5f;
        rb = gameObject.GetComponent<Rigidbody>();
        bulletforceZ = 15f;
        startPos = transform.position;

        if (photonView.isMine)
        {

            myHP.InitHealth(100);
            myCam.enabled = true;
            myCam.tag = "MyCam";
            //gameObject.tag = "Me";
        }
        

    }

    private void Update()
    {
       
        if (photonView.isMine)
        {        
            myHP.UpdateHealth();
            Jump();
            PlayerMove();
            Shoot();
        }
       
        myInfoCanvas.transform.LookAt(GameObject.FindGameObjectWithTag("MyCam").transform);

    }

    private void Shoot()
    {

        if (Input.GetMouseButton(0) && readyToFire && Time.time > nextShot)
        {

            nextShot = Time.time + 1;
            GameObject ammoInstance = Instantiate(ammo, firePoint.position, Quaternion.identity);
            ammoInstance.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * 10, ForceMode.Impulse);
            photonView.RPC("ShootBall", PhotonTargets.Others, firePoint.transform.position, firePoint.transform.forward);
            readyToFire = false;

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

        GameObject ammoInstanc = Instantiate(ammo, location, Quaternion.identity);
        ammoInstanc.GetComponent<Rigidbody>().AddForce(forward * 10, ForceMode.Impulse);

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
