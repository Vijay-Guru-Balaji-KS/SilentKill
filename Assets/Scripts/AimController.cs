using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AimController : MonoBehaviour
{
    

    [Header("Shoot properties")]
    public float rotationSpeed = 1;
    public float BlastPower = 5;
    public TextMeshProUGUI teleportcount;

    //public GameObject Cannonball;
    [Header("Teleportation Properties")]
    public GameObject teleporter;

    [Header("Shoot Properties")]
    public Transform ShotPoint;
    public int ballcount = 1;

    [Header("Other Scripts References")]
    public DrawProjection drawProjection;

    //public GameObject Explosion; 

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            aimAndShoot();
        }
    }


    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.rigidbody.)
        {
            ballcount = 1;
            teleportcount.text = "1";
        }
    }*/

    public void Reset()
    {
        ballcount = 1;
        teleportcount.text = "1";
        drawProjection.enabled = true;
    }

    void aimAndShoot()
    {
        float HorizontalRotation = Input.GetAxis("Horizontal");
        float VericalRotation = Input.GetAxis("Vertical");

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
            new Vector3(0, HorizontalRotation * rotationSpeed, VericalRotation * rotationSpeed));

        if (Input.GetKeyDown(KeyCode.Space) && ballcount == 1)
        {
            //below correrct but i want to push it only once
            /*GameObject CreatedCannonball = Instantiate(Cannonball, ShotPoint.position, ShotPoint.rotation);
            CreatedCannonball.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * BlastPower;
            */

            GameObject CreatedCannonball = Instantiate(teleporter, ShotPoint.position, ShotPoint.rotation);
            CreatedCannonball.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * BlastPower;

            ballcount++;

            teleportcount.text = "0";

            // Added explosion for added effect
            //Destroy(Instantiate(Explosion, ShotPoint.position, ShotPoint.rotation), 2);

            // Shake the screen for added effect
            //Screenshake.ShakeAmount = 5;

        }
    }
}
