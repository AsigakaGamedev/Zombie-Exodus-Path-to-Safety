using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    [SerializeField] private string firstTimeLocalization = "ru";

    [Space]
    [SerializeField] private SerializedDictionary<string, LocalizationInfo> localizations;

    [Space]
    [ReadOnly, SerializeField] private LocalizationInfo currentLocalization;

    public Action<LocalizationInfo> onLocalizationChange;

    public LocalizationInfo CurrentLocalization { get => currentLocalization; }

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    private void Start()
    {
        ChangeLocalization(firstTimeLocalization);
    }

    public void ChangeLocalization(string key)
    {
        currentLocalization = localizations[key];
        onLocalizationChange?.Invoke(currentLocalization);
    }
}
