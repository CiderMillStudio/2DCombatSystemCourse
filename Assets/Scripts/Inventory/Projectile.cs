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

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {return;}
        else{
        {
            Destroy(gameObject);
            ProjectileHitEnemy(gameObject.transform.position);
        }
        }
    }


    
    void ProjectileHitEnemy(Vector3 otherPosition)
    {
        ParticleSystem HitEnemyVFXinstance = Instantiate(HitEnemyVFX, otherPosition, quaternion.identity);
    }

}

