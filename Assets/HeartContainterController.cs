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

    public void RefreshHeartContainers()
    {
        RefreshNumberOfMaxHearts();
        RefreshCurrentHealth();
        InitializeListOfHeartImages();
        DeactivateLockedHeartContainers();
        SetAllActiveHeartContainersToEmpty();
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
            Debug.Log(i);
            i.SetHeartSprite(heartEmpty);
        }
    }

    void SyncHeartContainersToCurrentHealth()
    {

        int mockCurrentHealth = currentHealth;
        int sillyNum = currentHealth/2;
        for (int i = 0; i <= sillyNum; i++)
        {
            HeartImage currentHeartContainer = heartImages[i];
            Debug.Log(currentHeartContainer);
            if (mockCurrentHealth - 2 >= 0)
            {
                currentHeartContainer.SetHeartSprite(heartFull);
                mockCurrentHealth -= 2;
            }
            else
            {
                currentHeartContainer.SetHeartSprite(heartHalf);
            }

        }
    }

    public void UnlockNextHeartContainer()
    {
        heartImages.Add(transform.GetChild(transform.childCount - (numberOfLockedHeartContainers)).GetComponent<HeartImage>());
        heartImages[transform.childCount - (numberOfLockedHeartContainers)].gameObject.SetActive(true);
        PlayerHealth.Instance.IncreaseMaxHealthByTwo();
        RefreshNumberOfMaxHearts();
        RefreshCurrentHealth();
        numberOfLockedHeartContainers = transform.childCount - numberOfActiveHeartContainers;
    }




    









    


}
