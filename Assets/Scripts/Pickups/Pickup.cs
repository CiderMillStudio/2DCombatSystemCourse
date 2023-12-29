using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour //This class is attached to items that are defined as "pickups" like coins, heart containers, etc...
{
    private enum PickUpType
    {
        GoldCoin,
        StaminaGlobe,
        HealthGlobe,

        GoldenHealthGlobe,
    }

    [SerializeField] private PickUpType pickUpType;
    [SerializeField] int healthGlobeHealAmount = 1;
    [SerializeField] int staminaGlobeRecoveryAmount = 1;
    
    [SerializeField] float pickupDistance = 5f; //how close the item needs to be to the player before it automatically starts traveling toward the player, like a magnet.
    
    [SerializeField] float moveSpeedModifier = 3f;
    float moveSpeed = 0f;

    Vector3 moveDirection;

    Rigidbody2D rb;

    HeartContainterController heartContainterController;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        heartContainterController = FindObjectOfType<HeartContainterController>();
        
    }
    
    private void Update() {
        Vector3 playerPos = PlayerController.Instance.transform.position;

        if (Vector3.Distance(transform.position, playerPos) < pickupDistance)
        {
            moveDirection = (playerPos - transform.position).normalized;
            moveSpeed += 0.1f * moveSpeedModifier;
        }
        else
        {
            moveDirection = Vector3.zero;
            moveSpeed = 0;
        }
    }

    private void FixedUpdate() {
        rb.velocity = moveDirection * moveSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            PickupTypeControl();
            Destroy(gameObject);
        }
    }
    

    void PickupTypeControl()
    {
        switch (pickUpType)
        {
            case PickUpType.GoldCoin:
            //do gold coin stuff
            break;

            case PickUpType.HealthGlobe:
            PlayerHealth.Instance.HealPlayer(healthGlobeHealAmount);
            break;

            case PickUpType.StaminaGlobe:
            PlayerStamina.Instance.GainStamina(staminaGlobeRecoveryAmount);
            break;

            case PickUpType.GoldenHealthGlobe:
            heartContainterController.UnlockNextHeartContainer();
            break;
        
            default:
            break;
        }
    }
}
