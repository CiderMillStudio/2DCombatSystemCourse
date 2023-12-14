using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{

Animator myAnimator;

[SerializeField] bool flipEffectX = false;

void Awake()
{
    myAnimator = GetComponent<Animator>();
}



public void TriggerSlash()
{
    gameObject.SetActive(true);
    myAnimator.SetTrigger("ActivateSlash");
}


public void PlayerSwordSlashEffectOff()
{
    gameObject.SetActive(false);
}

public void FlipSwordSlashEffect()
{
    flipEffectX = !flipEffectX;
}

public bool GetIsSlashFlippedX()
{
    return flipEffectX;
}




}
