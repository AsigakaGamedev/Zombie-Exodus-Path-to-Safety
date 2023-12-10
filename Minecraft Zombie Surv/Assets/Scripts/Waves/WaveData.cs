using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    [SerializeField] private float timeBetweenWave;
    [SerializeField] private List<WaveObjectData> waveInfoList;

    public float TimeBetweenWave
    {
        get { return timeBetweenWave; }
    }

    public List<WaveObjectData> WaveInfoList
    {
        get { return waveInfoList; }
    }
}
