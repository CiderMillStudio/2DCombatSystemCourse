using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    [SerializeField] GameObject slashAnimPrefab;
    [SerializeField] Transform slashAnimSpawnPoint;
    [SerializeField] Transform weaponCollider;
    PlayerControls playerControls;
    Animator myAnimator; 
    SpriteRenderer mySpriteRenderer;
    ActiveWeapon activeWeapon;
    PlayerController playerController;

    GameObject slashAnim;

    
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
        weaponCollider.gameObject.SetActive(true);
        slashAnim = Instantiate(slashAnimPrefab,slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    public void SwingUpFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(180,0,0);
        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0,0,0);
        if(playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
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
            weaponCollider.rotation = Quaternion.Euler(0,0,swordAngle);
        }
        else 
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0,-180,swordAngle);
            weaponCollider.rotation = Quaternion.Euler(0,-180,swordAngle);
        }
    }


}
