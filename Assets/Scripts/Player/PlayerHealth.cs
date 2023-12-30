using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] int maxPlayerHealth = 6;
    [SerializeField] float knockBackThrustAmount = 15f;
    [SerializeField] float damageRecoveryTime = 0.2f;

    int maximumNumberOfHeartContainers; // i.e. Max Health/2 (one health is a half heart)
    int currentHealth;
    Knockback knockback;
    Flash flash;
    HeartContainterController heartContainerController;
    PlayerDeath playerDeath;

    private bool canTakeDamage = true;

    protected override void Awake() {
        base.Awake();
        heartContainerController = FindObjectOfType<HeartContainterController>();
        SetMaximumNumberOfHeartContainers();
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        playerDeath = GetComponent<PlayerDeath>();
    }

    private void Start() {
        RestorePlayerHealth();
    }

    private void OnCollisionStay2D(Collision2D other) {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy)
        {
            TakeDamage(1, other.transform); //(other.gameObject.GetComponent<DamageSource>())
        }
    }

    void SetMaximumNumberOfHeartContainers()
    {
        if (maxPlayerHealth % 2 >= 1)
        {
            maximumNumberOfHeartContainers = (maxPlayerHealth/2) + 1;
        }
        else
        {
            maximumNumberOfHeartContainers = maxPlayerHealth/2;
        }
        

    }
    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
            if (!canTakeDamage || !PlayerController.Instance.PlayerIsAlive) { return; }
            
            knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
            canTakeDamage = false;
            currentHealth -= damageAmount;
            ScreenShakeManager.Instance.ShakeScreen();
            heartContainerController.MiniRefreshHeartContainers();
            StartCoroutine(flash.FlashRoutine());
            StartCoroutine(DamageRecoveryRoutine());
            CheckIfPlayerDeath();



    }

    IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    public void HealPlayer(int healAmount)
    {
            currentHealth += healAmount;
            if (currentHealth >= maxPlayerHealth)
            {
                currentHealth = maxPlayerHealth;
            }

            heartContainerController.MiniRefreshHeartContainers();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxNumberOfHeartContainers()
    {
        return maximumNumberOfHeartContainers;
    }

    public void IncreaseMaxHealthByTwo()
    {
        maxPlayerHealth += 2;
        SetMaximumNumberOfHeartContainers();
    }

    void CheckIfPlayerDeath()
    {
        if (currentHealth <= 0 && PlayerController.Instance.PlayerIsAlive)
        {
            currentHealth = 0;
            StartCoroutine(playerDeath.PlayerDeathEvent());
        }
    }

    public void RestorePlayerHealth() //you can call this after a respawn.
    {
        currentHealth = maxPlayerHealth;
        heartContainerController.MiniRefreshHeartContainers();
    }

    

}
