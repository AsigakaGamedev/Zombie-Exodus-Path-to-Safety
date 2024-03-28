using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

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

    [Inject]
    private void Construct(LocalizationManager localizationManager)
    {
        this.localizationManager = localizationManager;
    }

    private void Start()
    {
        localizationManager.onLocalizationChange += OnLocalizationChange;

        OnLocalizationChange("", localizationManager.CurrentLocalization);
    }

    private void OnDestroy()
    {
        if (localizationManager) localizationManager.onLocalizationChange -= OnLocalizationChange;
    }

    private void OnLocalizationChange(string language, LocalizationInfo localization)
    {
        if (!localization) return;

        targetText.text = localization.GetValue(localizationKey);
    }
}