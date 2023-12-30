using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] GameObject slashAnimPrefab;
    [SerializeField] Transform slashAnimSpawnPoint;
    Transform weaponCollider;
    [SerializeField] private WeaponInfo  weaponInfo;

    Animator myAnimator; 
    SpriteRenderer mySpriteRenderer;

    

    GameObject slashAnim;

    

    
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Start() {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
    }

    private void Update() {
        if (PlayerController.Instance.PlayerIsAlive)
        { 
            MouseFollowWithOffset();
        }
    }

    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }

    public void Attack() //(Sword AS IWeapon)
    {
        
        
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            slashAnim = Instantiate(slashAnimPrefab,slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
            
        
    }


    public void SwingUpFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(180,0,0);
        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0,0,0);
        if(PlayerController.Instance.FacingLeft)
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
        Vector3 playerRealWorldPoint = PlayerController.Instance.transform.position;    
        Vector3 mouseScreenPoint = Mouse.current.position.ReadValue();
        Vector3 mouseRealWorldPoint = Camera.main.ScreenToWorldPoint(mouseScreenPoint);

        float swordAngle = Mathf.Atan2(mouseScreenPoint.y, mouseScreenPoint.x) * Mathf.Rad2Deg;
        if (mouseRealWorldPoint.x > playerRealWorldPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,0,swordAngle);
            weaponCollider.rotation = Quaternion.Euler(0,0,swordAngle);
        }
        else 
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,-180,swordAngle);
            weaponCollider.rotation = Quaternion.Euler(0,-180,swordAngle);
        }
    }


}
