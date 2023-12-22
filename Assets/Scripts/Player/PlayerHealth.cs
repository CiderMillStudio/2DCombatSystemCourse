using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxPlayerHealth = 3;
    [SerializeField] float knockBackThrustAmount = 15f;
    [SerializeField] float damageRecoveryTime = 0.2f;
    int currentHealth;
    Knockback knockback;
    Flash flash;

    private bool canTakeDamage = true;

    private void Awake() {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start() {
        currentHealth = maxPlayerHealth;
    }

    private void OnCollisionStay2D(Collision2D other) {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy)
        {
            TakeDamage(1, other.transform); //(other.gameObject.GetComponent<DamageSource>())
        }
    }

    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
            if (!canTakeDamage) { return; }
            
            knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
            canTakeDamage = false;
            currentHealth -= damageAmount;
            StartCoroutine(flash.FlashRoutine());
            StartCoroutine(DamageRecoveryRoutine());
    }

    IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

}
