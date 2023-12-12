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
    [SerializeField] float playerMoveSpeed = 1f;

    void Awake()
    {
        myRigidbody = FindObjectOfType<Rigidbody2D>();
        myAnimator = FindObjectOfType<Animator>();
    }

    
    void Update()
    {
        
    }


    void OnMove(InputValue value)
    {
            moveInput = value.Get<Vector2>() * playerMoveSpeed;
            myRigidbody.velocity = new Vector2 (moveInput.x, moveInput.y);
            Debug.Log(moveInput);
    }

}
