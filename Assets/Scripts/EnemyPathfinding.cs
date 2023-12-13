using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    EnemyAI enemyAI;
    Rigidbody2D myRigidbody;
    SpriteRenderer spriteRenderer;
    [SerializeField] float enemyMoveSpeed = 1f;
    Vector2 moveTarget;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyAI>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {  
        moveTarget = new (enemyAI.enemyRoamingPosition.x, enemyAI.enemyRoamingPosition.y);
        Vector2 currentPosition = new (gameObject.transform.position.x, gameObject.transform.position.y);
        Vector2 movePositionVector = currentPosition + moveTarget * 
        enemyMoveSpeed * Time.fixedDeltaTime;
        myRigidbody.MovePosition(movePositionVector);

        if (moveTarget.x > Mathf.Epsilon)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveTarget.x < -Mathf.Epsilon)
        {
            spriteRenderer.flipX = true;
        }
    }
}
