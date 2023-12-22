using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    public bool FacingLeft {get { return facingLeft;} }
    // public static PlayerController Instance; //What is the benefit of using an instance here??
    //We don't need the instance anymore, because we are now inheriting from the singleton class
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
    [SerializeField] Transform weaponCollider;

    Knockback knockback;

    float startingMoveSpeed;

    protected override void Awake() //Since the "Base Awake()" function is from our Singleton.cs class, and because this class inherits from Singleton.cs, we need to call our local Awake() function as "protect override".
    {
        base.Awake(); //By calling base.Awake(), we call the code from the singleton class, and make this instance into a singleton. Without this line of code, PlayerController.Awake() will be summoned and ignore all code in the Singleton.Awake() mother-class from which this class inherits, resulting in a NON-singleton result.
        // Instance = this; //what is going on?! 
        //no need for instance anymore.
        playerControls = new PlayerControls();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
    }

    void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash(); //this is called subscribing
        startingMoveSpeed = playerMoveSpeed;
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
        if (!knockback.GettingKnockedBack) 
        {
            Move();


        }
    }

    public Transform GetWeaponCollider()
    {
        return weaponCollider;
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
            facingLeft = false;
        }
        else if (mouseWorldPosition.x < transform.position.x)
        {
            playerSpriteRenderer.flipX = true;
            facingLeft = true;
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
        playerMoveSpeed = startingMoveSpeed;
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
