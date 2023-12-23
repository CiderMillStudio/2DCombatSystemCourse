using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    [SerializeField] ParticleSystem HitEnemyVFX;
    [SerializeField] ParticleSystem HitOtherVFX;
    [SerializeField] float projectileSpeed =5f;
    [SerializeField] bool isEnemyProjectile = false;
    
    [SerializeField] private float projectileRange = 10f;

    Vector3 startPosition;

    int arrowDamage;

    
    private void Start() {
        startPosition = transform.position;
    }
    void Update() {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }
    void MoveProjectile()
    {
        transform.Translate(new Vector3(projectileSpeed,0,0) * Time.deltaTime);
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (!other.isTrigger && enemyHealth || player)
        {
            if (enemyHealth && isEnemyProjectile) {return;}
            if (player && !isEnemyProjectile) {return;}
            if (player && isEnemyProjectile)
            {
                player.TakeDamage(1, transform);
            }
            Destroy(gameObject);
            ProjectileHitEnemy(gameObject.transform.position);
        }
        else if (!other.isTrigger && indestructible)
        {
            Destroy(gameObject);
            ProjectileHitOther(gameObject.transform.position);
        }
    }


    void DetectFireDistance()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, startPosition)) > projectileRange)
        {
            Destroy(gameObject);
        }
    }


    
    void ProjectileHitEnemy(Vector3 otherPosition)
    {
        ParticleSystem HitEnemyVFXinstance = Instantiate(HitEnemyVFX, otherPosition, quaternion.identity);
    }
    void ProjectileHitOther (Vector3 otherPosition)
    {
        ParticleSystem HitOtherVFXinstance = Instantiate(HitOtherVFX, otherPosition, quaternion.identity);
    }

}

