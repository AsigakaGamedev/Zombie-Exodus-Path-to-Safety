using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NeedData
{
    [SerializeField] private float value;
    [SerializeField] private float maxValue;
    [SerializeField] private float changeValue;

    public Action<float> onNeedValueChange;

    public float Value
    {
        get { return value; }
        set 
        {
            this.value = Mathf.Clamp(value, 0, maxValue);
            onNeedValueChange?.Invoke(this.value);
        }
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
