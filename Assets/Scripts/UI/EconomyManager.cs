using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManager : Singleton<EconomyManager>
{
    int noCoins = 0;

    int currentCoins;

    [SerializeField] TextMeshProUGUI coinAmountText;



    private void Start() {
        currentCoins = noCoins;
        coinAmountText.text = currentCoins.ToString("D3");
    }

    public void UpdateCoinAmount(int howManyCoinsToAdd)
    {
        currentCoins += howManyCoinsToAdd;
        if (currentCoins == noCoins)
        {
            coinAmountText.text = "000";
        }
        else
        {
        coinAmountText.text = currentCoins.ToString("D3");
        }

    }



}
