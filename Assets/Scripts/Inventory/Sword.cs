using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] GameObject slashAnimPrefab;
    [SerializeField] Transform slashAnimSpawnPoint;
    [SerializeField] Transform weaponCollider;
    [SerializeField] float minTimeBetweenSwordAttacks = 0.3f;

    Animator myAnimator; 
    SpriteRenderer mySpriteRenderer;
    PlayerController playerController;

    ActiveWeapon activeWeapon;
    

    GameObject slashAnim;

    

    
    void Awake()
    {
        
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        
    }




    private void Update() {
        MouseFollowWithOffset();
    }

    public void Attack() //(Sword AS IWeapon)
    {
        
        
            ActiveWeapon.Instance.ToggleIAttacking(true);
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            slashAnim = Instantiate(slashAnimPrefab,slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
            StartCoroutine(AttackCDRoutine());
            
        
    }

    IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(minTimeBetweenSwordAttacks);
        ActiveWeapon.Instance.ToggleIAttacking(false);
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
