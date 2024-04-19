using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    PlayerMovement playerMovement;
    public SurfaceDetection SurfaceDetection;
    public AimController AimController;

    //public ParticleSystem TeleportationEffect;
    public GameObject teleportation;

    //public GameObject startPosition;


    public bool isTeleported = false;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        //SurfaceDetection = GetComponent<SurfaceDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isTeleported = true;
            playerMovement.countref.text = "1";
            StartCoroutine(TeleportPlayer()); 
            AimController.Reset();
        }
    }

    IEnumerator TeleportPlayer()
    {
        playerMovement.disabled = true;
        yield return new WaitForSeconds(1);

        //Debug.Log("Just checking plane");
        //TeleportationEffect.Play();
        GameObject tempteleportation =  Instantiate(teleportation,this.transform);
        yield return new WaitForSeconds(1.3f);

        gameObject.transform.position = SurfaceDetection.tracker.transform.position;

        Destroy(tempteleportation);
        //Debug.Log("Just checking plane " + SurfaceDetection.tracker.transform.position.x + " " + SurfaceDetection.tracker.transform.position.y + " " + SurfaceDetection.tracker.transform.position.z);

        GameObject temp = Instantiate(teleportation, this.transform);
        yield return new WaitForSeconds(1);

        
        playerMovement.disabled = false;
        Destroy(temp,0.2f);
        //Destroy(SurfaceDetection.obj);
    }


   /* public void restartGame()
    {
        //isTeleported = true;
        //playerMovement.countref.text = "1";
        StartCoroutine(RestartTeleportPlayer());
        //AimController.Reset();
    }

    IEnumerator RestartTeleportPlayer()
    {
        playerMovement.disabled = true;
        yield return new WaitForSeconds(1);

        GameObject restartTele = Instantiate(teleportation, this.transform);
        yield return new WaitForSeconds(0.2f);

        gameObject.transform.position = startPosition.transform.position;
        Destroy(restartTele);

        GameObject temp = Instantiate(teleportation, this.transform);
        yield return new WaitForSeconds(1);


        playerMovement.disabled = false;
        Destroy(temp, 0.2f);
    }*/
}
