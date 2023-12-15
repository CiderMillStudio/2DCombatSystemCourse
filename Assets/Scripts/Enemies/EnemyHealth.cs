using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
public bool IsHit { get { return isHit; } set { isHit = value; } }
[SerializeField] int startingHealth = 3;
[SerializeField] float knockBackThrustAmount = 10f;

Knockback knockBack;
bool isHit = false;

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
    
    Invoke(nameof(HitAndDelay), 0f);
    
    
    DetectDeath();


}

void HitAndDelay()
{
    IsHit = true;
    knockBack.GetKnockedBack(PlayerController.Instance.transform, knockBackThrustAmount);
    Invoke(nameof(TurnIsHitFalse), 0.2f);
    
}

void TurnIsHitFalse(){
    IsHit = false;
}


void DetectDeath()
{
    if (currentHealth <= 0)
    {
        Destroy(gameObject);
    }
}
}
