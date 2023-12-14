using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    [SerializeField] Transform weaponCollider;
    PlayerControls playerControls;
    Animator myAnimator; 
    SpriteRenderer mySpriteRenderer;
    ActiveWeapon activeWeapon;
    PlayerController playerController;

    [SerializeField] GameObject slashEffect;
    [SerializeField] float timeBetweenSwordAndSlashEffect = 0.2f;
    [SerializeField] float timeToDelayBeforeFlippingXSlashEffect = 0.2f;

    
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
        Invoke("TriggerASlashEffect",timeBetweenSwordAndSlashEffect);      
    }

    void TriggerASlashEffect()
    {
        slashEffect.GetComponent<SlashEffect>().TriggerSlash();
        Invoke("DelayForFlipXSlash", timeToDelayBeforeFlippingXSlashEffect);
    }

    void DelayForFlipXSlash()
    {
        slashEffect.GetComponent<SlashEffect>().FlipSwordSlashEffect();
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
            if (slashEffect.GetComponent<SlashEffect>().GetIsSlashFlippedX())
            {
                slashEffect.transform.rotation = Quaternion.Euler(-180,0, swordAngle);
            }
            else
            {
                slashEffect.transform.rotation = Quaternion.Euler(0,0, swordAngle);
            }
        }
        else 
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0,-180,swordAngle);
            weaponCollider.rotation = Quaternion.Euler(0,-180,swordAngle);
            
            if (slashEffect.GetComponent<SlashEffect>().GetIsSlashFlippedX())
            {
                slashEffect.transform.rotation = Quaternion.Euler(-180,-180, swordAngle);
            }
            else
            {
                slashEffect.transform.rotation = Quaternion.Euler(0,-180, swordAngle);
            }
        }
    }


}
