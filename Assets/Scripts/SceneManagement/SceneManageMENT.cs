using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManageMENT : Singleton<SceneManageMENT>
{
    // protected override void Awake()
    // {
    //     base.Awake();
    // }

    public string SceneTransitionName { get; private set; }
   
    public void SetTransitionName(string sceneTransitionName)
    {
        this.SceneTransitionName = sceneTransitionName;
    }
}
