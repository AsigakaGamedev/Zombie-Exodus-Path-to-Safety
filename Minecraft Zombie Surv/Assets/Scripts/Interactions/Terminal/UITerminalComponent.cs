using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITerminalComponent : PoolableObject
{
    [SerializeField] private TextMeshProUGUI labelText;
    [SerializeField] private Button btn;

    private TerminalComponent linkedComponent;

    public Action<UITerminalComponent> onComponentClick;

    public TerminalComponent LinkedComponent { get => linkedComponent; }

    private void Start()
    {
        btn.onClick.AddListener(() =>
        {
            linkedComponent.onPress.Invoke();
            onComponentClick.Invoke(this);
        });
    }

    public void SetLink(TerminalComponent linkedComponent, string validatedLabelText)
    {
        this.linkedComponent = linkedComponent;
        labelText.text = validatedLabelText;
    }
}