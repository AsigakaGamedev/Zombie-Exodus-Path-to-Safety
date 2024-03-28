using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private string defaultLocalization = "ru";

    private LocalizationManager localizationManager;

    public const string localizationPrefsKey = "LOCALIZATION";

    [Inject]
    private void Construct(LocalizationManager localizationManager)
    {
        this.localizationManager = localizationManager;
    }

    private void Start()
    {
        localizationManager.ChangeLocalization(PlayerPrefs.GetString(localizationPrefsKey, defaultLocalization));
    }
}
