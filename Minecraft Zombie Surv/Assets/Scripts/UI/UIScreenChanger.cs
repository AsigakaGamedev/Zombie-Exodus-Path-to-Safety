using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIScreenChanger : MonoBehaviour
{
    [SerializeField] private string screenName;

    private Button button;
    private UIManager uiManager;

    private void Start()
    {
        uiManager = ServiceLocator.GetService<UIManager>();

        button = GetComponent<Button>();
        button.onClick.AddListener(OnBtnClick);
    }

    private void OnBtnClick()
    {
        uiManager.ChangeScreen(screenName);
    }
}
