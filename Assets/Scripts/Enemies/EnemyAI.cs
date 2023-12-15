using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State 
    {
        Roaming
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;

    Vector2 enemyRoamingPosition;

    private void Awake()
    {
        state = State.Roaming; //defaults initial state to roaming
        enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    void Start()
    {
        StartCoroutine(RoamingRoutine());
    }



    //We need a coroutine to fire every few seconds to determine the random
    //direction of enemy movement:
    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            enemyRoamingPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(enemyRoamingPosition);
            yield return new WaitForSeconds(2f);
        }
    }



    private Vector2 GetRoamingPosition()
    {
        return new Vector2 (Random.Range(-1f,1f), Random.Range(-1f,1f));
    }
    
}
