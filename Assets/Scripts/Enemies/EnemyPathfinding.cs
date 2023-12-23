using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

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

    public void StopMoving()
    {
        moveTarget = Vector3.zero;
    }


    public void MoveTo(Vector2 vector2)
    {
        moveTarget = vector2;
    }

    public void SetEnemyMoveSpeed(float newMoveSpeed)
    {
        this.enemyMoveSpeed = newMoveSpeed;
    }

    public Vector2 MoveToward(Transform thisObject, Transform ObjectToMoveTowards)
    {
        Vector2 thisObjectsPosition = new (thisObject.position.x, thisObject.position.y);
        Vector2 OtherObjectsPosition = new (ObjectToMoveTowards.position.x, ObjectToMoveTowards.position.y);

        Vector2 moveTowardsDirection = OtherObjectsPosition - thisObjectsPosition;
        
        Vector3 normalizedVector = Vector3.Normalize(moveTowardsDirection);

        return normalizedVector;
    }

    public void MakeEnemyFacePlayer()
    {
        Vector2 vector = PlayerController.Instance.transform.position - transform.position;

        if (vector.x <= 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }


    
}
