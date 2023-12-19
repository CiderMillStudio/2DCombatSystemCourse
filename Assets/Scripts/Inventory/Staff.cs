using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Staff : MonoBehaviour, IWeapon //requires the public Attack method
{
    [SerializeField] private WeaponInfo  weaponInfo;
    // Transform weaponCollider;
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;

    Transform magicAnimSpawnPoint;
    
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Start() {
        // weaponCollider = PlayerController.Instance.GetWeaponCollider();
        magicAnimSpawnPoint = PlayerController.Instance.GetSlashAnimSpawnPoint();
    }

    private void Update() {
        MouseFollowWithOffset();
    }

    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }
    public void Attack() //without this "public attack" function, IWeapon cannot be implemented!!! (Try commenting this out and watch the IWeapon implementation return an error!)
    {
        Debug.Log("Staff attack!");

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
            // weaponCollider.rotation = Quaternion.Euler(0,0,swordAngle);
        }
        else 
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,-180,swordAngle);
            // weaponCollider.rotation = Quaternion.Euler(0,-180,swordAngle);
        }
    }
}
