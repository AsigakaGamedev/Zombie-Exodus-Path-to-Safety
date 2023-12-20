using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private string defaultLocalization = "ru";

    private LocalizationManager localizationManager;

    public const string localizationPrefsKey = "LOCALIZATION";

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
        localizationManager = ServiceLocator.GetService<LocalizationManager>();
        localizationManager.ChangeLocalization(PlayerPrefs.GetString(localizationPrefsKey, defaultLocalization));
    }
}
