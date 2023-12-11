using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Localization")]
public class LocalizationInfo : ScriptableObject
{
    [SerializeField] private SerializedDictionary<string, string> values;

    [Space]
    [SerializeField] private LocalizationInfo copyLocalization; 

    public string GetValue(string key)
    {
        if (values.ContainsKey(key))
        {
            return values[key];
        }
        else
        {
            return key;
        }
    }

    [Button]
    public void CopyKeys()
    {
        if (!copyLocalization) return;

        foreach (string key in copyLocalization.values.Keys)
        {
            if (!values.ContainsKey(key)) values.Add(key, "");
        }
    }
}
