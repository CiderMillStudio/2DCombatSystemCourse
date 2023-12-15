using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
[SerializeField] int startingHealth = 3;
[SerializeField] float knockBackThrust = 15f;

Knockback knockBack;


int currentHealth;


void Awake() {
    knockBack = GetComponent<Knockback>();
}
void Start()
{
    currentHealth = startingHealth;
}
public void TakeDamage(int damage)
{
    currentHealth -= damage;
    knockBack.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
    Debug.Log(currentHealth);
    DetectDeath();

}


void DetectDeath()
{
    if (currentHealth <= 0)
    {
        Destroy(gameObject);
    }
}
}
