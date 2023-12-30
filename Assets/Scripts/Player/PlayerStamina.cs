using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : Singleton<PlayerStamina>
{
    [SerializeField] int maxPlayerStamina = 2;
    [SerializeField] float staminaRecoveryTime = 0.2f;

    StaminaContainerController staminaContainerController;

    private bool canUseStamina;
    
    int currentStamina;

    float timer;

    protected override void Awake() {
        base.Awake();
        staminaContainerController = FindObjectOfType<StaminaContainerController>();

    }

    private void Start() {
        timer = 0;
        RestorePlayerStamina();
    }

    private void Update() {
        StaminaController();
    }

    void StaminaController()
    {
        if (currentStamina <= 0)
        {
            canUseStamina = false;
        }
        else
        {
            canUseStamina = true;
        }

        if (timer < staminaRecoveryTime && PlayerController.Instance.PlayerIsAlive)
        {
            timer += Time.deltaTime;
        }
        else if (timer >= staminaRecoveryTime)
        {
            timer = 0;
            GainStamina(1);
            if (currentStamina >= maxPlayerStamina)
            {
                currentStamina = maxPlayerStamina;
            }
        }


    }

    public void UseStamina(int howMuchStamina)
    {
        if (currentStamina - howMuchStamina >= 0)
        {
            currentStamina -= howMuchStamina;
            staminaContainerController.MiniRefreshStaminaContainers();
        }
    }

    public bool GetCanPlayerUseStamina()
    {
        return canUseStamina;
    }

    public void GainStamina(int howMuchStamina)
    { 
        currentStamina += howMuchStamina;
        if (currentStamina >= maxPlayerStamina)
        {
            currentStamina = maxPlayerStamina;
        }   
        staminaContainerController.MiniRefreshStaminaContainers();
    }

    public int GetMaxNumberOfStaminaContainers()
    {
        return maxPlayerStamina;
    }

    public int GetCurrentStamina()
    {
        return currentStamina;
    }


    public void RestorePlayerStamina()
    {
        currentStamina = maxPlayerStamina;
        staminaContainerController.MiniRefreshStaminaContainers();
    }

    

}
