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

    [Inject]
    private void Construct(UIPopupsManager popupsManager)
    {
        this.popupsManager = popupsManager;
    }

    private void Awake()
    {
        if (!button) button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            popupsManager.CloseCurrentPopup();
        });
    }
}
