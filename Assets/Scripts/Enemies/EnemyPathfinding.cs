using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPathfinding : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    SpriteRenderer spriteRenderer;
    [SerializeField] float enemyMoveSpeed = 1f;
    Vector2 moveTarget;
    Knockback knockback;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
    }

    private void FixedUpdate() 
    {
        if (knockback.GettingKnockedBack)
        {
            return;
        }
        myRigidbody.MovePosition(myRigidbody.position + moveTarget * (enemyMoveSpeed * Time.fixedDeltaTime));
        EnemySpriteFlipper();
    }

    void EnemySpriteFlipper()
    {
        if (moveTarget.x > Mathf.Epsilon)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveTarget.x < -Mathf.Epsilon)
        {
            spriteRenderer.flipX = true;
        }
    }


    public void MoveTo(Vector2 vector2)
    {
        moveTarget = vector2;
    }



    
}
