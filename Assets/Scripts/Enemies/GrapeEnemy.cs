using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeEnemy : MonoBehaviour, IEnemy
{
    
    [SerializeField] GameObject grapeProjectilePrefab;

    [SerializeField] public int grapeBulletDamage = 2;
    [SerializeField] public int splatteredGrapeDamage = 1;
    Animator myAnimator;
    SpriteRenderer spriteRenderer;
    
    
    private void Awake() {
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
    }

    public void Attack()
    {
        myAnimator.SetTrigger("isAttacking");
    }

    public void ShootGrapeAnim()
    {
        GameObject grapeProjectileInstance = Instantiate(grapeProjectilePrefab, transform.position, Quaternion.identity);
        grapeProjectileInstance.GetComponent<HyperbolicProjectile>().UpdateProjectileDamage(grapeBulletDamage, splatteredGrapeDamage);
    }
}
