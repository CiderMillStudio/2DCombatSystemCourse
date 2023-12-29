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
        currentStamina = maxPlayerStamina;
        staminaContainerController = FindObjectOfType<StaminaContainerController>();

    }

    private void Start() {
        timer = 0;
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

        if (timer < staminaRecoveryTime)
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
            Debug.Log("currentStamina is " + currentStamina);
        }


    }

    public void UseStamina(int howMuchStamina)
    {
        if (currentStamina - howMuchStamina >= 0)
        {
            currentStamina -= howMuchStamina;
            Debug.Log(howMuchStamina + " stamina used. currentStamina is " + currentStamina);
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
        Debug.Log("Stamina Gained. currenSTamina is now" + currentStamina);
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

    

}
