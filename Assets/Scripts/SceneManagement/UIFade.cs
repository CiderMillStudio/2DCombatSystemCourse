using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : Singleton<UIFade>
{
    [SerializeField] Image fadeScreen;
    [SerializeField] float fadeSpeed = 1f;


    private IEnumerator fadeRoutine; //this is a private IEnumerator variable which allows us to start and stop our coroutine

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        while (!Mathf.Approximately(fadeScreen.color.a, targetAlpha)) //NEW Mathf.Approximately()
        {
            float alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime); //NEW Mathf.MoveTowards()
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
            yield return null; //No need for time here since we have Time.deltaTime (time between frames) in the MoveTowards function
        }
    }

    public void FadeToBlack() {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }
        
        fadeRoutine = FadeRoutine(1);
        StartCoroutine(fadeRoutine);
    }

    public void FadeToClear() {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }
        
        fadeRoutine = FadeRoutine(0);
        StartCoroutine(fadeRoutine);
    }

    


    //Below lies my personal attemp, which did not work!
    // public IEnumerator FadeRoutine()
    // {
    //     while (fadeScreen.color.a < 1)
    //     {
    //         float variable1 = Mathf.Lerp(0,1, fadeScreen.color.a/1f);
    //         float originalAlpha = fadeScreen.color.a;
    //         fadeScreen.color = new Color (fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 
    //                                                 originalAlpha + 0.1f*fadeSpeed);
    //         yield return new WaitForSeconds(0.01f);
    //     }


    //     Debug.Log("Fade Complete");


    // }
}
