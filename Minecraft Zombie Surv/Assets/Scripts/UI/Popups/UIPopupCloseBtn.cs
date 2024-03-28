using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UIPopupCloseBtn : MonoBehaviour
{
    [SerializeField] private Button button;

    private UIPopupsManager popupsManager;

    private void OnValidate()
    {
        if (!button) button = GetComponent<Button>();
    }

    [Inject]
    private void Construct(UIPopupsManager popupsManager)
    {
        this.popupsManager = popupsManager;
    }

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            popupsManager.CloseCurrentPopup();
        });
    }
}
