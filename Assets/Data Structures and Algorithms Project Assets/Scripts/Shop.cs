using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private float _coins = 0;

    public Action<float> OnCoinsChanged;

    public void BuySeed(string name, float price)
    {
        if(_coins >= price)
        {
            _coins -= price;
            Planter._instance.AddSeeds(name, 1);
            OnCoinsChanged(_coins);
        }
        else
        {
            Debug.Log("Not enough coins to buy seeds");
            UIManager._instance.UpdateStatus("Not enough coins");
        }
        
    }

    // Get the harvest, add coins for the value, update UI and remove the item from the data structure
    public void SellHarvest(CollectedHarvest harvestElement, float valuePerItem)
    {
        //Add coins
        _coins += valuePerItem * harvestElement._amount;
        OnCoinsChanged.Invoke(_coins);
        Harvester._instance.RemoveHarvest(harvestElement);
    }
}
