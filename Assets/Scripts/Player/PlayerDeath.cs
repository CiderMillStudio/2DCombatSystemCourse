using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{

    Animator myAnimator;
    [SerializeField] float respawnDelayTime = 1.5f;
    
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public IEnumerator PlayerDeathEvent()
    {
        myAnimator.SetTrigger("playerDied");
        PlayerController.Instance.PlayerIsAlive = false;
        yield return new WaitForSeconds(respawnDelayTime);
        Destroy(gameObject);
        Destroy(ActiveWeapon.Instance.gameObject);
        // Destroy(gameObject.GetComponent<PlayerHealth>());
        // Destroy(FindObjectOfType<CameraController>().gameObject);
        // Destroy(FindObjectOfType<ActiveWeapon>().gameObject);
        
        SceneManager.LoadScene("Town");
        
        // PlayerHealth.Instance.RestorePlayerHealth();

    }
}
