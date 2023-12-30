using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; //DON'T FORGET TO ADD CINEMACHINE HERE!!!!

public class CameraController : Singleton<CameraController>
{
   
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    // private void Start() {
    //     SetPlayerCameraFollow(); //Can keep this commented out for now, because the PlayerController.cs class calls this WHENEVER a new player instance is instantiated (i.e. whenever the player dies and respawns, this method is automatically called. This method is also called in the very beginning of the game when the first Player instance is instantiated)
    // }
    
    public void SetPlayerCameraFollow() //used in AreaExit/AreaEntrance to help new cameras navigate
    {
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
    }
}
