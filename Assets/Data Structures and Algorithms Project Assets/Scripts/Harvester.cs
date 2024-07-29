using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : MonoBehaviour
{
    [SerializeField] public Harvest _harvest;
    [SerializeField] public Seed _seed;

    // Harvest Analytics
    private Dictionary<string, int> _harvests = new Dictionary<string, int>();

    // Harvest to sell
    // Assignment 2 - Data structure to hold collected harvests
    private List<CollectedHarvest> collectedHarvests = new List<CollectedHarvest>();
    public Action OnCollectionChanged;

    public static Harvester _instance;
       
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;
    }

    // Assignment 2
    public List<CollectedHarvest> GetCollectedHarvest()
    {
        return collectedHarvests;
    }

    // Assignment 2
    public void RemoveHarvest(CollectedHarvest harvest)
    {
        collectedHarvests.Remove(harvest);
        UpdateAnalytics(harvest._name, -harvest._amount);
        OnCollectionChanged.Invoke();
    }

    // Assignment 2 - CollectHarvest method to collect the harvest when picked up
    

    public void ShowHarvest(string plantName, int harvestAmount, int seedAmount, Vector2 position)
    {
        // initiate a harvest with random amount
        Harvest harvest = Instantiate(_harvest, position + Vector2.up + Vector2.right, Quaternion.identity);
        harvest.SetHarvest(plantName, harvestAmount);
        
        // initiate one seed object
        Seed seed = Instantiate(_seed, position + Vector2.up + Vector2.left, Quaternion.identity);
        seed.SetSeed(plantName, seedAmount);
    }

    //Assignment 3
    public void SortHarvestByAmount()
    {
        // Sort the collected harvest using Quick sort
        Quicksort(collectedHarvests, 0, collectedHarvests.Count - 1);
        OnCollectionChanged.Invoke();
    }

    private void Quicksort(List<CollectedHarvest> list, int start, int end)
    {
        if (end <= start) return;

        int pivot = Partition(list, start, end);
        Quicksort(list, start, pivot - 1);
        Quicksort(list, pivot + 1, end);
    }

    private int Partition(List<CollectedHarvest> list, int start, int end)
    { 
        int pivot = list[end]._amount;
        int i = start - 1;

        for(int j = start; j <= end; j++)
        {
            if (list[j]._amount < pivot)
            {
                i++;
                CollectedHarvest temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        i++;
        CollectedHarvest tmp = list[i];
        list[i] = list[end];
        list[end] = tmp;

        return i;
    }

    public void CollectHarvest(string plantName, int harvestAmount)
    {
        string time = DateTime.Now.ToString("g");
        CollectedHarvest harvest = new CollectedHarvest(plantName, time, harvestAmount);
        collectedHarvests.Add(harvest);
        UpdateAnalytics(plantName, harvestAmount);
        UIManager._instance.UpdateStatus(MakeHarvestString(plantName, harvestAmount, time));
        OnCollectionChanged.Invoke();
    }

    private void UpdateAnalytics(string plantName, int harvestAmount)
    {
        int currentAmount;
        if(_harvests.TryGetValue(plantName, out currentAmount))
        {
            //Debug.Log("current amount: " + currentAmount.ToString());
            _harvests[plantName] = currentAmount + harvestAmount;
            //Debug.Log("NEW amount" + _harvests[plantName].ToString());
        } else
        {
            _harvests.Add(plantName, harvestAmount);
            //Debug.Log("made new item in dictionary");
        }
    }

    private string MakeHarvestString(string plantName, int harvestAmount, string time)
    {
        string str;
        if(harvestAmount > 1)
        {
            str = "s were harvested";
        } else
        {
            str = " was harvested";
        }
        return "On " + time + ", " + harvestAmount.ToString() + " " + plantName + str;
    }
}

// For Assignment 2, this holds a collected harvest object
[System.Serializable]
public struct CollectedHarvest
{
    public string _name;
    public string _time;
    public int _amount;
    
    public CollectedHarvest(string name, string time, int amount)
    {
        _name = name;
        _time = time;
        _amount = amount;
    }
}