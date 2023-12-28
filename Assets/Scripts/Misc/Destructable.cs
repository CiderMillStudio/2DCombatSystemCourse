using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] GameObject destroyVFX;

    PickupSpawner pickupSpawner;

    private void Awake() {
        pickupSpawner = GetComponent<PickupSpawner>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<DamageSource>() || 
        other.gameObject.GetComponent<Projectile>())
        {
            pickupSpawner.DropItems();
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            // StartCoroutine(DestroyDelay(0.5f));
            Destroy(gameObject);
            
        }
    }

    // IEnumerator DestroyDelay(float timerDuration)
    // {
    //     float elapsedTime = 0;
    //     while (elapsedTime < timerDuration)
    //     {
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }
    //     Destroy(gameObject);
    // }
}


