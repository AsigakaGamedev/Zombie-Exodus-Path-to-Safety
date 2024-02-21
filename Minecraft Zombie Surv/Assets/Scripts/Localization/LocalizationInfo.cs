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

    [Space]
    [SerializeField] private bool addNonContainsValue; 

    public string GetValue(string key)
    {
        if (values.ContainsKey(key))
        {
            if (string.IsNullOrEmpty(values[key])) return key;

            return values[key];
        }
        else
        {
            values.Add(key, string.Empty);
            return key;
        }
    }

    [Button]
    public void CopyKeys()
    {
        if (!copyLocalization) return;

        foreach (string key in copyLocalization.values.Keys)
        {
            if (addNonContainsValue && !values.ContainsKey(key)) values.Add(key, "");
        }
    }
}
