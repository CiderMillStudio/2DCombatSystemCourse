using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{

    [SerializeField] string sceneToLoad;
    [Tooltip("This is the name of the ENTRANCE which you'll be teleported TO.")]
    [SerializeField] string sceneTransitionName;

    [SerializeField] float loadSceneDelay = 1f;


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            UIFade.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());            
        }
    }

    private IEnumerator LoadSceneRoutine() 
    {
        while (loadSceneDelay >= 0)  //This is a super nice timer!
        {
            loadSceneDelay -= Time.deltaTime;
            yield return null;
        }

        SceneManageMENT.Instance.SetTransitionName(sceneTransitionName);
        SceneManager.LoadScene(sceneToLoad);
    }
}
