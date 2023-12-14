using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    
    PlayerControls playerControls;
    Animator myAnimator; 
    SpriteRenderer mySpriteRenderer;
    ActiveWeapon activeWeapon;
    PlayerController playerController;
    void Awake()
    {
        playerControls = new PlayerControls();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        playerController = GetComponentInParent<PlayerController>();
        
    }
    void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update() {
        MouseFollowWithOffset();
    }

    void Attack()
    {
        myAnimator.SetTrigger("Attack");
    }

    void MouseFollowWithOffset()
    {
        Vector3 playerRealWorldPoint = playerController.transform.position;    
        Vector3 mouseScreenPoint = Mouse.current.position.ReadValue();
        Vector3 mouseRealWorldPoint = Camera.main.ScreenToWorldPoint(mouseScreenPoint);

        float swordAngle = Mathf.Atan2(mouseScreenPoint.y, mouseScreenPoint.x) * Mathf.Rad2Deg;
        if (mouseRealWorldPoint.x > playerRealWorldPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0,0,swordAngle);
        }
        else 
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0,-180,swordAngle);
        }
    }


}
