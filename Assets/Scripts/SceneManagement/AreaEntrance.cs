using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
[SerializeField] string sceneTransitionName;

private void Start() {
    if (sceneTransitionName == SceneManageMENT.Instance.SceneTransitionName)
    {
        PlayerController.Instance.transform.position = this.transform.position;
        CameraController.Instance.SetPlayerCameraFollow();
    }
}
} 
