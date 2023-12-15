using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft {get { return facingLeft;} set { facingLeft = value; }}
    public static PlayerController Instance; //What is the benefit of using an instance here??
    Rigidbody2D myRigidbody;
    Animator myAnimator;

    Vector2 moveInput;
    PlayerControls playerControls; //this refers to the script that automaticaly comes with player input map

    SpriteRenderer playerSpriteRenderer;
    [SerializeField] float playerMoveSpeed = 1f;
    [SerializeField] TrailRenderer trailRenderer;
    bool facingLeft = false;
    bool dashIsInCD = false;
    [SerializeField] float dashTime = 0.2f;
    [SerializeField] float dashCDTime = 1f;
    [SerializeField] float dashSpeedMultiplier = 4f;

    void Awake()
    {
        Instance = this; //what is going on?!
        playerControls = new PlayerControls();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
    }
     void OnEnable() //you need this for the new input system
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

        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        
        if (mouseWorldPosition.x > transform.position.x)
        {
            playerSpriteRenderer.flipX = false;
            FacingLeft = false;
        }
        else if (mouseWorldPosition.x < transform.position.x)
        {
            playerSpriteRenderer.flipX = true;
            FacingLeft = true;
        }
    }



    void Dash()
    {
        if (!dashIsInCD)
        {
            dashIsInCD = true;
            playerMoveSpeed *= dashSpeedMultiplier;
            trailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
        
    }

    IEnumerator EndDashRoutine()
    {
        yield return new WaitForSeconds(dashTime);
        playerMoveSpeed /= dashSpeedMultiplier;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCDTime);
        dashIsInCD = false;

    }
    
    // IEnumerator DashRoutine()
    // {
    //     dashIsInCD = true;
    //     playerMoveSpeed *= dashSpeedMultiplier;
    //     trailRenderer.emitting = true;
    //     yield return new WaitForSeconds(dashTime);    //This was my attempt, did not work.
    //     playerMoveSpeed /= dashSpeedMultiplier;
    //     trailRenderer.emitting = false;
    //     yield return new WaitForSeconds(dashCDTime);
    //     dashIsInCD = false;
    // }

    


}
