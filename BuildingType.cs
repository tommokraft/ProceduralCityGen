using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BuildingType
{
    [SerializeField]
    private GameObject[] prefabs;
    public int sizeNeeded;
    public int quantity;
    public int quantityPlaced;

    public GameObject GetPrefab()
    {
        quantityPlaced++;
        if (prefabs.Length > 1)
        {
            var random = UnityEngine.Random.Range(0, prefabs.Length);
            return prefabs[random];
        }
        return prefabs[0];
    }
    public bool buildingAvailable()
    {
        return quantityPlaced < quantity;
    }
    public void reset()
    {
        quantityPlaced = 0;
    }
}
