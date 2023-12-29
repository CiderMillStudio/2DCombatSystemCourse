using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainterController : MonoBehaviour
{
    [SerializeField] Sprite heartFull, heartHalf, heartEmpty;

    List<HeartImage> heartImages = new List<HeartImage>();
    HeartImage heartImage; 

    int numberOfActiveHeartContainers;
    int numberOfLockedHeartContainers;
    int currentHealth;

    void Awake() {
        
    }

    private void Start() {
        
        RefreshHeartContainers();
    }

    void RefreshHeartContainers() //call this whenever you alter the player's health
    {
        RefreshNumberOfMaxHearts();
        RefreshCurrentHealth();
        InitializeListOfHeartImages();
        DeactivateLockedHeartContainers();
        SetAllActiveHeartContainersToEmpty();
        SyncHeartContainersToCurrentHealth();
    }

    public void MiniRefreshHeartContainers()
    {
        RefreshNumberOfMaxHearts();
        RefreshCurrentHealth();
        // SetAllActiveHeartContainersToEmpty();
        SyncHeartContainersToCurrentHealth();

    }

    void RefreshNumberOfMaxHearts()
    {
        numberOfActiveHeartContainers = PlayerHealth.Instance.GetMaxNumberOfHeartContainers();
    }

    void RefreshCurrentHealth()
    {
        currentHealth = PlayerHealth.Instance.GetCurrentHealth();
    }

    void InitializeListOfHeartImages() //initializes the heartImages list
    {
        for (int i = 0; i<transform.childCount; i++)
        {
            HeartImage newHeartImage = transform.GetChild(i).GetComponent<HeartImage>();
            heartImages.Add(newHeartImage);            
        }
    }

    void DeactivateLockedHeartContainers()
    {
        numberOfLockedHeartContainers = transform.childCount - numberOfActiveHeartContainers;
        for (int i = 0; i < numberOfLockedHeartContainers; i++)
        {
            heartImages[transform.childCount - (i+1)].gameObject.SetActive(false);
            heartImages.RemoveAt(transform.childCount - (i+1));
        }
    }

    void SetAllActiveHeartContainersToEmpty()
    {
        foreach (HeartImage i in heartImages)
        {
            i.SetHeartSprite(heartEmpty);
        }
    }

    void SyncHeartContainersToCurrentHealth()
    {

        int mockCurrentHealth = currentHealth;
        for (int i = 0; i <= heartImages.Count-1; i++)
        {
            HeartImage currentHeartContainer = heartImages[i];
            if (mockCurrentHealth - 2 >= 0)
            {
                currentHeartContainer.SetHeartSprite(heartFull);
                mockCurrentHealth -= 2;
            }
            else if (mockCurrentHealth - 1 >= 0)
            {
                currentHeartContainer.SetHeartSprite(heartHalf);
                mockCurrentHealth--;
            }
            else
            {
                currentHeartContainer.SetHeartSprite(heartEmpty);
            }

        }
    }

    public void UnlockNextHeartContainer()
    {
        if (numberOfLockedHeartContainers > 0)
        {
        heartImages.Add(transform.GetChild(transform.childCount - (numberOfLockedHeartContainers)).GetComponent<HeartImage>());
        heartImages[transform.childCount - (numberOfLockedHeartContainers)].gameObject.SetActive(true);
        PlayerHealth.Instance.IncreaseMaxHealthByTwo();
        RefreshNumberOfMaxHearts();
        RefreshCurrentHealth();
        numberOfLockedHeartContainers = transform.childCount - numberOfActiveHeartContainers;
        }
        else {return;}
    }




    









    


}
