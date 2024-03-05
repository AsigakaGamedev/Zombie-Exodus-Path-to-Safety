using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Localization")]
public class LocalizationInfo : ScriptableObject
{
    [SerializeField] private SerializedDictionary<string, LocalizationValue> values;

    [Space]
    [SerializeField] private LocalizationInfo copyLocalization;

    [Space]
    [SerializeField] private bool addNonContainsValue; 

    public string GetValue(string key)
    {
        if (values.ContainsKey(key))
        {
            if (string.IsNullOrEmpty(values[key].Value)) return key;

            return values[key].Value;
        }
        else
        {
            values.Add(key, new LocalizationValue());
            return key;
        }
    }

    [Button]
    public void CopyKeys()
    {
        if (!copyLocalization) return;

        foreach (string key in copyLocalization.values.Keys)
        {
            if (addNonContainsValue && !values.ContainsKey(key)) values.Add(key, new LocalizationValue());
        }
    }
}

[System.Serializable]
public class LocalizationValue
{
    [TextArea(0, 3), SerializeField] private string value;

    public string Value { get => value; }
}
