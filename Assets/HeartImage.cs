using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartImage : MonoBehaviour
{
    Image heartSprite;

    private void Awake() {
        heartSprite = GetComponent<Image>();
    }

    public void SetHeartSprite(Sprite whichHeartSprite)
    {
        heartSprite.sprite = whichHeartSprite;
    }
}
