using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIPopupCloseBtn : MonoBehaviour
{
    [SerializeField] private Button button;

    private void OnValidate()
    {
        if (!button) button = GetComponent<Button>();
    }

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            ServiceLocator.GetService<UIPopupsManager>().CloseCurrentPopup();
        });
    }
}
