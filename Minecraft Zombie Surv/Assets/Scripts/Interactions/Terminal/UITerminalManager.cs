using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class UITerminalManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI labelText;

    [Space]
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private float contentCharDelay = 0.1f;

    [Space]
    [SerializeField] private UITerminalComponent uiComponentPrefab;
    [SerializeField] private Transform componentsContent;

    private LocalizationManager localizationManager;
    private ObjectPoolingManager poolingManager;

    private List<UITerminalComponent> showedComponents;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    private void Start()
    {
        showedComponents = new List<UITerminalComponent>();

        localizationManager = ServiceLocator.GetServiceSafe<LocalizationManager>();
        poolingManager = ServiceLocator.GetServiceSafe<ObjectPoolingManager>();
    }

    public void OpenTerminal(TerminalController terminal)
    {
        foreach (var uiComponent in showedComponents)
        {
            uiComponent.onComponentClick -= OnComponentClick;
            uiComponent.gameObject.SetActive(false);
        }

        showedComponents.Clear();

        labelText.text = localizationManager.CurrentLocalization.GetValue(terminal.TerminalLabelKey);

        foreach (var logicComponent in terminal.Components)
        {
            UITerminalComponent newUIComp = poolingManager.GetPoolable(uiComponentPrefab);
            newUIComp.transform.SetParent(componentsContent);
            newUIComp.transform.localScale = Vector3.one;
            newUIComp.SetLink(logicComponent, localizationManager.CurrentLocalization.GetValue(logicComponent.LabelKey));
            newUIComp.onComponentClick += OnComponentClick;
        }
    }

    private void OnComponentClick(UITerminalComponent clickedComponent)
    {
        StopAllCoroutines();

        StartCoroutine(WriteContentSmooth(clickedComponent.LinkedComponent.ContentKey));
    }

    private IEnumerator WriteContentSmooth(string content)
    {
        string resultText = "";

        foreach (char contentChar in content)
        {
            resultText += contentChar;
            contentText.text = resultText;
            yield return new WaitForSeconds(contentCharDelay);
        }
    }
}