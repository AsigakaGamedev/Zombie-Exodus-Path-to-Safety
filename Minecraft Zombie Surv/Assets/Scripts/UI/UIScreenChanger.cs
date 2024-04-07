using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UIScreenChanger : MonoBehaviour
{
    [SerializeField] private string screenName;

    private Button button;
    private UIManager uiManager;

    [Inject]
    private void Construct(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    private void Start()
    {
        if (!button) button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            uiManager.ChangeScreen(screenName);
        });
    }
}
