using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UILocalizationChangerBtn : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private string localizationKey;

    private LocalizationManager localizationManager;

    private void OnValidate()
    {
        if (!btn) btn = GetComponent<Button>();
    }

    private void Start()
    {
        localizationManager = ServiceLocator.GetService<LocalizationManager>();

        btn.onClick.AddListener(() =>
        {
            localizationManager.ChangeLocalization(localizationKey);
        });
    }
}