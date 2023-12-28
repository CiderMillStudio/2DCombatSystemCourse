using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PickupSpawner : MonoBehaviour //This class is attached to destructible objects and enemies that can drop loot
{
    [SerializeField] GameObject goldCoinPrefab;

    public void DropItems()
    {
        Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
    }
}
