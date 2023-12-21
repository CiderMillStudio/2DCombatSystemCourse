using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] float laserGrowTime = 2f;
    [SerializeField] Transform PoofPoint;
    private bool isGrowing = true;
    private float laserRange;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D myCapsuleCollider;
    
    [SerializeField] ParticleSystem LaserHitsEnemyParticleVFX;
    [SerializeField] ParticleSystem LaserHitsIndestructibleVFX;


    SpriteFade spriteFade;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteFade = GetComponent<SpriteFade>();
    }

    private void Start() {
      LaserFaceMouse(transform);
      LaserFaceMouse(PoofPoint);
    }

    public void UpdateLaserRange(float newLaserRange)
    {
        this.laserRange = newLaserRange;
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

    IEnumerator IncreaseLaserLengthRoutine()
    {
        float timePassed = 0f;

        while (spriteRenderer.size.x < laserRange && isGrowing)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed/laserGrowTime;

            spriteRenderer.size = new Vector2 (Mathf.Lerp(1f, laserRange, linearT), 1f);
            myCapsuleCollider.offset = new Vector2 (Mathf.Lerp(1f, laserRange/2, linearT), 0f);
            myCapsuleCollider.size = new Vector2 (Mathf.Lerp(1f, laserRange, linearT), myCapsuleCollider.size.y);
            PoofPoint.localPosition += new Vector3 (Time.deltaTime*laserRange/laserGrowTime,0,0);
            yield return null;
        }

        StartCoroutine(spriteFade.SlowFadeRoutine());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<Indestructible>() && !other.isTrigger)
        {
            
            isGrowing = false;
            if (other.GetComponent<TilemapRenderer>())
            {
                ParticleSystem LaserHitsIndestructibleTileVFXInstance = Instantiate(LaserHitsIndestructibleVFX, PoofPoint);
            }
            else
            {
                ParticleSystem LaserHitsIndestructibleParticleVFXInstance = Instantiate(LaserHitsIndestructibleVFX, other.transform.position, other.transform.rotation);
            }

        }
        if (other.gameObject.GetComponent<EnemyAI>() && !other.isTrigger)
        {
            ParticleSystem LaserHitsEnemyParticleVFXInstance = Instantiate(LaserHitsEnemyParticleVFX, other.transform.position, other.transform.rotation);

        }
    }

    

    
    void LaserFaceMouse(Transform whichTransform)
    {
        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector2 direction = transform.position - mousePosition;

        whichTransform.right = -direction;
    }



}
