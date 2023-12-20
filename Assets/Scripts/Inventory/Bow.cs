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

    [SerializeField] float arrowSpeed =1f;

    public void Attack() //without this "public attack" function, IWeapon cannot be implemented!!! (Try commenting this out and watch the IWeapon implementation return an error!)
    {
        ShootArrow(arrowObject);
    }

    void ShootArrow(GameObject whichGameObject)
    {
        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        Vector2 direction = transform.position - mousePosition;

        float arrowShootAngle = Mathf.Atan2(-direction.y, -direction.x);
        Vector2 shootDirection = new (Mathf.Cos(arrowShootAngle), Mathf.Sin(arrowShootAngle));
        
        GameObject arrowInstance = Instantiate(whichGameObject, transform.position, Quaternion.Euler(0,0, arrowShootAngle * Mathf.Rad2Deg));
        
        arrowInstance.GetComponent<Rigidbody2D>().velocity = shootDirection * arrowSpeed;
    }

    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }
}
