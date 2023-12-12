using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    Animator myAnimator;

    Vector2 moveInput;

    SpriteRenderer playerSpriteRenderer;
    [SerializeField] float playerMoveSpeed = 1f;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        
    }


    void OnMove(InputValue value)
    {
            moveInput = value.Get<Vector2>() * playerMoveSpeed;
            myRigidbody.velocity = new Vector2 (moveInput.x, moveInput.y);
            Debug.Log(moveInput);
        if (myRigidbody.velocity.x > Mathf.Epsilon)
        {
            playerSpriteRenderer.transform.localScale = new Vector3 (1,1,1);
        }
        else if (myRigidbody.velocity.x < -Mathf.Epsilon)
        {
            playerSpriteRenderer.transform.localScale = new Vector3 (-1,1,1);
        }

        if (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon || Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon)
        {
            myAnimator.SetBool("isRunning", true);
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }
    }

}
