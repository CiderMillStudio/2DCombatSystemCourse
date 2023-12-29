using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
[SerializeField] int startingHealth = 3;
[SerializeField] float knockBackThrust = 15f;
[SerializeField] ParticleSystem splatSlime;
[SerializeField] float splatTime = 0.1f;

Knockback knockBack;
Flash flash;




int currentHealth;


void Awake() {
    knockBack = GetComponent<Knockback>();
    flash = GetComponent<Flash>();
}
void Start()
{
    currentHealth = startingHealth;
}
public void TakeDamage(int damage)
{
    currentHealth -= damage;
    knockBack.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
    StartCoroutine(flash.FlashRoutine());
    StartCoroutine(DetectDeath(flash.GetRestoreMatTime()));

}


IEnumerator DetectDeath(float deathDelayTime)
{
    yield return new WaitForSeconds(deathDelayTime);
    if (currentHealth <= 0)
    {
        ParticleSystem splatInstance = Instantiate(splatSlime, gameObject.transform.position, Quaternion.identity);
        GetComponent<PickupSpawner>().DropItems();
        GetComponent<PickupSpawner>().DropItems();
        Destroy(gameObject);
        yield return new WaitForSeconds(splatTime);
        Destroy(splatInstance);
    }
}
}
