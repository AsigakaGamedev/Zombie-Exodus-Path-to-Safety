using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NeedsData
{
    [SerializeField] private string id;
    [SerializeField] private float value;
    [SerializeField] private float maxValue;
    [SerializeField] private float changeValue;

    public string Id
    {
        get { return id; }
    }

    public float Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public float MaxValue
    {
        get { return maxValue; }
    }

    public float ChangeValue
    {
        get { return changeValue; }
    }
}
