using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaImage : MonoBehaviour
{
Image staminaSprite;

    private void Awake() {
        staminaSprite = GetComponent<Image>();
    }

    public void SetStaminaSprite(Sprite whichStaminaSprite)
    {
        staminaSprite.sprite = whichStaminaSprite;
    }
}
