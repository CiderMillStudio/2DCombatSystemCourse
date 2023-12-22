using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; //DON'T FORGET TO ADD CINEMACHINE HERE!!!!

public class CameraController : Singleton<CameraController>
{
   
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Start() {
        SetPlayerCameraFollow();
    }
    public void SetPlayerCameraFollow() //used in AreaExit/AreaEntrance to help new cameras navigate
    {
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
    }
}
