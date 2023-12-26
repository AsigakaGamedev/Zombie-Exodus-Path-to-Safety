using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MonologData
{
    [SerializeField] private string text;
    [SerializeField] private float time;

    public string Text
    {
        get { return text; }
    }

    public float Time
    {
        get { return time; }
    }
}
