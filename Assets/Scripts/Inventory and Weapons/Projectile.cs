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
    
    private WeaponInfo weaponInfo;

    Vector3 startPosition;

    int arrowDamage;

    
    private void Start() {
        startPosition = transform.position;
    }
    void Update() {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateWeaponInfo(WeaponInfo newWeaponInfo)
    {
        this.weaponInfo = newWeaponInfo;
    }
    void MoveProjectile()
    {
        transform.Translate(new Vector3(projectileSpeed,0,0) * Time.deltaTime);
    }
    
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {return;}
        else if (other.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            
            Destroy(gameObject);
            ProjectileHitEnemy(gameObject.transform.position);
    
        }
        else
        {
            Destroy(gameObject);
            ProjectileHitOther(gameObject.transform.position);
        }
    }

    void DetectFireDistance()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, startPosition)) > weaponInfo.weaponRange)
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

