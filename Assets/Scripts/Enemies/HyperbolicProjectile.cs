using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HyperbolicProjectile : MonoBehaviour
{
    [SerializeField] float duration = 1f;
    [SerializeField] AnimationCurve animCurve;
    [SerializeField] float heightY = 3f;
    [SerializeField] GameObject shadowOfProjectile;

    [SerializeField] Sprite landedProjectile;
    [SerializeField] float landedProjectileLifetime = 1.5f;
    SpriteRenderer spriteRenderer;
    SpriteFade spriteFade;
    CircleCollider2D myCircleCollider; //the CIRCLE collider is the FLYING projectile's collider
    CapsuleCollider2D myCapsuleCollider; //the CAPSULE collider is the LANDED (splattered/exploded) projectile's collider

    int flyingProjectileDamage; //damage done if flying projectile hits player
    int landedProjectileDamage; //damage done if player walks through landed projectile's collider
    
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteFade = GetComponent<SpriteFade>();
        myCircleCollider = GetComponent<CircleCollider2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        

    }
    private void Start() {
        myCapsuleCollider.enabled = false;
        myCircleCollider.enabled = false;
        StartCoroutine(ProjectileCurveRoutine(gameObject.GetComponentInParent<Transform>().position, PlayerController.Instance.transform.position));
    }

    public void UpdateProjectileDamage(int newflyingProjDamage, int newLandedProjDamage)
    {
        this.flyingProjectileDamage = newflyingProjDamage;
        this.landedProjectileDamage = newLandedProjDamage;
    }

    private IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;
        GameObject shadowInstance = Instantiate(shadowOfProjectile, transform.position, Quaternion.identity);


        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed/duration;
            if (linearT >= 0.8f || linearT <= 0.2f) {myCircleCollider.enabled = true;}
            float heightT = animCurve.Evaluate(linearT); //essentially Height over time
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = 
            Vector2.Lerp(startPosition, endPosition, linearT) + 
            new Vector2(0f, height); 

            shadowInstance.transform.position = 
            Vector2.Lerp(startPosition, endPosition, linearT);

            yield return null;
        }

        spriteRenderer.sprite = landedProjectile;
        myCircleCollider.enabled = false;
        myCapsuleCollider.enabled = true;
        Destroy(shadowInstance);

        
        yield return new WaitForSeconds(landedProjectileLifetime);
        
        StartCoroutine(spriteFade.SlowFadeRoutine());
        // yield return new WaitForSeconds(landedProjectileLifetime*(1/3));
        // myCapsuleCollider.enabled = false;

        float waitTime = landedProjectileLifetime * 0.5f;
        yield return new WaitForSeconds(waitTime);

        myCapsuleCollider.enabled = false;

        yield return new WaitForSeconds(landedProjectileLifetime);

        Destroy(gameObject);
        
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.GetComponent<PlayerHealth>())
        {
            PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
            if (myCircleCollider.enabled)
            {
                player.TakeDamage(flyingProjectileDamage, transform);
            }
            if (myCapsuleCollider.enabled)
            {
                player.TakeDamage(landedProjectileDamage, transform);
            }

        }
    }
}
