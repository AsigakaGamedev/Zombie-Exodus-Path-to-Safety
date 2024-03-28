using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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

    [Inject]
    private void Construct(LocalizationManager localizationManager)
    {
        this.localizationManager = localizationManager;
    }

    private void Start()
    {
        btn.onClick.AddListener(() =>
        {
            localizationManager.ChangeLocalization(localizationKey);
        });
    }
}