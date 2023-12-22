using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo  weaponInfo;
    [SerializeField] GameObject arrowObject;



    [SerializeField] Transform arrowSpawnPoint;

    Animator myAnimator;

    



    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }
    public void Attack() //without this "public attack" function, IWeapon cannot be implemented!!! (Try commenting this out and watch the IWeapon implementation return an error!)
    {
        ShootArrow(arrowObject);
        myAnimator.SetTrigger("isFiring");
    }

    void ShootArrow(GameObject arrow)
    {
        
        Quaternion arrowRotation = ActiveWeapon.Instance.transform.rotation;
        GameObject arrowInstance = Instantiate(arrow, arrowSpawnPoint.position, arrowRotation) ;
        arrowInstance.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);

    }



    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }
}
