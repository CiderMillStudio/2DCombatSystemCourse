using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Staff : MonoBehaviour, IWeapon //requires the public Attack method AND GetWeaponInfo method;
{
    [SerializeField] private WeaponInfo  weaponInfo;
    // Transform weaponCollider;
    [SerializeField] GameObject magicLaser;
    [SerializeField] Transform magicLaserSpawnPoint;
    [SerializeField] ParticleSystem magicStaffVFX;
    [SerializeField] ParticleSystem magicStaffVFXBottom;
    [SerializeField] Transform magicStaffVFXSpawnPoint;
    [SerializeField] Transform magicStaffVFXSpawnPointBottom;

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
        
        myAnimator.SetTrigger("isAttacking");
        Debug.Log("Staff attack!");

    }


    public void SpawnStaffProjectileAnimEvent()
    {
        GameObject magicLaserInstance = Instantiate(magicLaser, magicLaserSpawnPoint.position, Quaternion.identity);
        ParticleSystem staffVFXBottomInstance = Instantiate(magicStaffVFXBottom, magicStaffVFXSpawnPointBottom.position, magicStaffVFXSpawnPointBottom.rotation);
    }

    public void SpawnStaffParticleVFX()
    {
        ParticleSystem staffVFXInstance = Instantiate(magicStaffVFX, magicStaffVFXSpawnPoint, false);
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
