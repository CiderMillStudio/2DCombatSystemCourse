using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PickupSpawner : MonoBehaviour //This class is attached to destructible objects and enemies that can drop loot
{
    [SerializeField] GameObject goldCoinPrefab, healthGlobe, staminaGlobe;

    public void DropItems() //Selects a random item to drop, then drops it! Can be assigned to any destructible object. This method is called in "Destructible.cs"
    {
        int randomNum = Random.Range(1,5);
        if (randomNum == 1)
        {
            int randomAmount = Random.Range(1,4);
            
            for (int i = 0; i < randomAmount; i++)
            {
                Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
            }
        }
        if (randomNum == 2)
        {
            Instantiate(healthGlobe, transform.position, Quaternion.identity);
        }

        if (randomNum == 3)
        {
            Instantiate(staminaGlobe, transform.position, Quaternion.identity);
        }
    }
}
