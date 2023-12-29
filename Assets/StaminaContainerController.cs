using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaContainerController : MonoBehaviour
{
    [SerializeField] Sprite staminaFull, staminaEmpty;

    List<StaminaImage> staminaImages = new List<StaminaImage>();

    int numberOfActiveStaminaContainers;
    int numberOfLockedStaminaContainers;
    int currentStamina;

    private void Start() {
        RefreshStaminaContainers();
    }
    void RefreshStaminaContainers() //call this whenever you alter the player's stamina
    {
        RefreshNumberOfMaxStamina();
        RefreshCurrentStamina();
        InitializeListOfStaminaImages();
        DeactivateLockedStaminaContainers();
        SetAllActiveStaminaContainersToEmpty();
        SyncStaminaContainersToCurrentStamina();
    }

    public void MiniRefreshStaminaContainers()
    {
        RefreshNumberOfMaxStamina();
        RefreshCurrentStamina();
        // SetAllActiveHeartContainersToEmpty();
        SyncStaminaContainersToCurrentStamina();

    }

    void RefreshNumberOfMaxStamina()
    {
        numberOfActiveStaminaContainers = PlayerStamina.Instance.GetMaxNumberOfStaminaContainers();

    }

    void RefreshCurrentStamina()
    {
        currentStamina = PlayerStamina.Instance.GetCurrentStamina();
    }

    void InitializeListOfStaminaImages() //initializes the staminaImages list
    {
        for (int i = 0; i<transform.childCount; i++)
        {
            StaminaImage newStaminaImage = transform.GetChild(i).GetComponent<StaminaImage>();
            staminaImages.Add(newStaminaImage);            
        }
    }

    void DeactivateLockedStaminaContainers()
    {
        numberOfLockedStaminaContainers = transform.childCount - numberOfActiveStaminaContainers;
        for (int i = 0; i < numberOfLockedStaminaContainers; i++)
        {
            staminaImages[transform.childCount - (i+1)].gameObject.SetActive(false);
            staminaImages.RemoveAt(transform.childCount - (i+1));
        }
    }

    void SetAllActiveStaminaContainersToEmpty()
    {
        foreach (StaminaImage i in staminaImages)
        {
            i.SetStaminaSprite(staminaEmpty);
        }
    }


    void SyncStaminaContainersToCurrentStamina()
    {

        int mockCurrentStamina = currentStamina;
        for (int i = 0; i <= staminaImages.Count-1; i++)
        {
            StaminaImage currentStaminaContainer = staminaImages[i];
            if (mockCurrentStamina - 1 >= 0)
            {
                currentStaminaContainer.SetStaminaSprite(staminaFull);
                mockCurrentStamina -= 1;
            }
            else
            {
                currentStaminaContainer.SetStaminaSprite(staminaEmpty);
            }

        }
    }

    public void UnlockNextStaminaContainer()
    {
        if (numberOfLockedStaminaContainers > 0)
        {
        staminaImages.Add(transform.GetChild(transform.childCount - (numberOfLockedStaminaContainers)).GetComponent<StaminaImage>());
        staminaImages[transform.childCount - (numberOfLockedStaminaContainers)].gameObject.SetActive(true);
        PlayerHealth.Instance.IncreaseMaxHealthByTwo();
        RefreshNumberOfMaxStamina();
        RefreshCurrentStamina();
        numberOfLockedStaminaContainers = transform.childCount - numberOfActiveStaminaContainers;
        }
        else {return;}
    }

}
