using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    [SerializeField] float fadeTime = 0.4f;
    
    SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator SlowFadeRoutine()
    {
        float elapsedTime = 0;
        float startValue = spriteRenderer.color.a;
        while (elapsedTime < fadeTime)
        {
            
            float newAlpha = Mathf.Lerp(startValue, 0f, elapsedTime / fadeTime);
            yield return null;
            spriteRenderer.color = new Color (spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            elapsedTime += Time.deltaTime;

        }

        Destroy(gameObject);
    }
}
