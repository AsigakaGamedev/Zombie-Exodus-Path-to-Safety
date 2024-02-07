using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<string, LocalizationInfo> localizations;

    [Space]
    [ReadOnly, SerializeField] private LocalizationInfo currentLocalization;
    [ReadOnly, SerializeField] private string languageID;

    public Action<string, LocalizationInfo> onLocalizationChange;

    public LocalizationInfo CurrentLocalization { get => currentLocalization; }
    public string LanguageID { get => languageID; }

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    private void Awake()
    {
        currentLocalization = localizations["ru"];
        languageID = "ru";
    }

    public void ChangeLocalization(string key)
    {
        currentLocalization = localizations[key];
        languageID = key;
        onLocalizationChange?.Invoke(key, currentLocalization);
    }
}
