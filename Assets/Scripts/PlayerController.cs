using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    Animator myAnimator;

    Vector2 moveInput;
    PlayerControls playerControls;

    SpriteRenderer playerSpriteRenderer;
    [SerializeField] float playerMoveSpeed = 1f;

    void Awake()
    {
        playerControls = new PlayerControls();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

     void OnEnable() 
    {
        playerControls.Enable();
    }
    
    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        moveInput = playerControls.Movement.Move.ReadValue<Vector2>();
        
        myAnimator.SetFloat("moveAlongX", moveInput.x);
        myAnimator.SetFloat("moveAlongY", moveInput.y);
    }

    void FixedUpdate() //Fixed update is good for physics, while update is good for player input
    {
        Move();
    }

    void Move()
    {
        Vector2 moveTarget = new Vector2 (moveInput.x, moveInput.y);
        Vector2 currentPosition = new Vector2 (transform.position.x, transform.position.y);
        myRigidbody.MovePosition(currentPosition + moveTarget * playerMoveSpeed * Time.fixedDeltaTime);

        if (moveTarget.x > Mathf.Epsilon)
        {
            playerSpriteRenderer.transform.localScale = new Vector3 (1,1,1);
        }
        else if (moveTarget.x < -Mathf.Epsilon)
        {
            playerSpriteRenderer.transform.localScale = new Vector3 (-1,1,1);
        }
    }


}
