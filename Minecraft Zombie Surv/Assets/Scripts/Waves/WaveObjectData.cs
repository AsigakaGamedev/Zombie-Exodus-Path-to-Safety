using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveObjectData 
{
    [SerializeField] private PoolableObject prefab;
    [SerializeField] private float amount;
    [SerializeField] private float timeBetweenSpawn;

    public PoolableObject Prefab
    {
        get { return prefab; }
    }

    public float Amount
    {
        get { return amount; }
    }

    public float TimeBetweenSpawn
    {
        get { return timeBetweenSpawn; }
    }
}
