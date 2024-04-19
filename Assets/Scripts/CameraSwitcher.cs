using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{

    public CinemachineVirtualCamera thirdpersonview;
    public CinemachineVirtualCamera firstPersonview;
    private CinemachineVirtualCamera activecam;
    //public GameObject teleporter;

    //to restrict the movement during the aim and throw phase 
    public PlayerMovement playerMovement;

    //to activate and inactivate the arc during perspective change by accessing the child of parent component from the player
    public GameObject parentofplayer;



    private void Start()
    {
        
        activecam = thirdpersonview;
        thirdpersonview.Priority = 10;
        firstPersonview.Priority = 8;
        //parentofplayer.active = false;
    }

   

    private void Update()
    {

        switchingCamera();
    }

    void switchingCamera()
    {
        if(Input.GetMouseButton(1))
        {
            //teleporter.SetActive(true);
            activecam = firstPersonview;
            firstPersonview.Priority = 10;
            thirdpersonview.Priority = 8;
            playerMovement.enabled = false;

            if(!parentofplayer.activeInHierarchy)
            {
                parentofplayer.active = true;
            }
        }
        else
        {
            activecam = thirdpersonview;
            thirdpersonview.Priority= 10;
            firstPersonview.Priority= 8;

            playerMovement.enabled = true;

            if (parentofplayer.activeInHierarchy)
            {
                parentofplayer.active = false;
            }
        }
    }

}
