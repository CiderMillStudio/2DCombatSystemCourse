using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{

    int damageAmount = 1;


    private void Start() {
        MonoBehaviour currentActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        damageAmount = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        //if (other.gameObject.GetComponent<EnemyHealth>() != null)
        //{
            enemyHealth?.TakeDamage(damageAmount); //can get rid of this if statement,
        //}                                          //replace it with "?"... wow.
    }


}
