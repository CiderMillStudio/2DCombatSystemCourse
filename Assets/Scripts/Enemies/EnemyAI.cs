using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float roamChangeDirectionTime = 2f;
    [SerializeField] float thresholdAttackDistance = 0f;
    [SerializeField] float thresholdHuntingRadius = 8f;

    [SerializeField] MonoBehaviour enemyType; 
    private enum State 
    {
        Roaming,
        Attacking
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;
    private Vector2 roamPosition;
    private float timeRoaming =0f;
    private bool canAttack = true;
    [SerializeField] float attackCooldownTime = 1f;
    [SerializeField] bool StopMovingWhileAttacking = false;
    [SerializeField] float attackingMoveSpeed = 3f;
    [SerializeField] float roamingMoveSpeed = 1f;

    Vector2 enemyRoamingPosition;
    float distanceBetweenPlayerAndEnemy;

    private void Awake()
    {
        state = State.Roaming; //defaults initial state to roaming
        distanceBetweenPlayerAndEnemy = float.MaxValue;
        enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    void Start()
    {
        roamPosition = GetRoamingPosition();
        
    }

    private void Update() 
    {
        MovementStateControl();
        distanceBetweenPlayerAndEnemy = Vector2.Distance(PlayerController.Instance.transform.position, transform.position);
    }

    private void MovementStateControl() {
        
        switch (state) //if this, then that (over and over again)
        {
            default: //default case should be put at the very end, but for now we'll leave it up top
            case State.Roaming:
                Roaming();
            break;

            case State.Attacking:
                Attacking();
                AttackMovement();
            break;
        }
        
    }

    private void Roaming() 
    {
        timeRoaming += Time.deltaTime;
        
        enemyPathfinding.MoveTo(roamPosition);
        enemyPathfinding.SetEnemyMoveSpeed(roamingMoveSpeed);
        
        if (distanceBetweenPlayerAndEnemy < thresholdAttackDistance)
        {
            state = State.Attacking;
        }
        if (timeRoaming > roamChangeDirectionTime)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        
        if (distanceBetweenPlayerAndEnemy > thresholdHuntingRadius)
        {
            state = State.Roaming;
        }

        if (!canAttack) {return;}
        else
        {
            canAttack = false;
            (enemyType as IEnemy).Attack();
        }

        
        StartCoroutine(AttackCooldownRoutine());
        
    }

    private void AttackMovement()
    {
        enemyPathfinding.SetEnemyMoveSpeed(attackingMoveSpeed);
        if (StopMovingWhileAttacking)
        {
            enemyPathfinding.StopMoving();
            enemyPathfinding.MakeEnemyFacePlayer();
        }
        else
        {
            enemyPathfinding.MoveTo(enemyPathfinding.MoveToward(transform, PlayerController.Instance.transform));
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        float timeElapsed = 0f;
        while (timeElapsed < attackCooldownTime)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        canAttack = true;

    }

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0;
        return new Vector2 (Random.Range(-1f,1f), Random.Range(-1f,1f));
        
    }



    
}
