using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeEnemy : MonoBehaviour, IEnemy
{
    
    [SerializeField] GameObject grapeProjectilePrefab;
    Animator myAnimator;
    SpriteRenderer spriteRenderer;
    
    
    private void Awake() {
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Attack()
    {
        myAnimator.SetTrigger("isAttacking");
    }
}
