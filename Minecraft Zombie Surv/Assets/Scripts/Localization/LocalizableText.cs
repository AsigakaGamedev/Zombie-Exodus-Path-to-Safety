using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizableText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private string localizationKey;

    private LocalizationManager localizationManager;

    private void OnValidate()
    {
        if (!targetText) targetText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        localizationManager = ServiceLocator.GetService<LocalizationManager>();
        localizationManager.onLocalizationChange += OnLocalizationChange;

        OnLocalizationChange(localizationManager.CurrentLocalization);
    }

    private void OnDestroy()
    {
        localizationManager.onLocalizationChange -= OnLocalizationChange;
    }

    private void OnLocalizationChange(LocalizationInfo localization)
    {
        if (!localization) return;

        targetText.text = localization.GetValue(localizationKey);
    }
}