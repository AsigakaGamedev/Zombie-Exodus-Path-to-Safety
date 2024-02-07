using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private List<UITerminalComponent> showedComponents = new List<UITerminalComponent>();

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    public void OpenTerminal(TerminalController terminal)
    {
        foreach (var uiComponent in showedComponents)
        {
            uiComponent.onComponentClick -= OnComponentClick;
            uiComponent.gameObject.SetActive(false);
        }

        showedComponents.Clear();

        if (!localizationManager) localizationManager = ServiceLocator.GetService<LocalizationManager>();

        labelText.text = localizationManager.CurrentLocalization.GetValue(terminal.TerminalLabelKey);

        if (!poolingManager) poolingManager = ServiceLocator.GetService<ObjectPoolingManager>();

        foreach (var logicComponent in terminal.Components)
        {
            UITerminalComponent newUIComp = poolingManager.GetPoolable(uiComponentPrefab);
            newUIComp.transform.SetParent(componentsContent);
            newUIComp.transform.localScale = Vector3.one;
            newUIComp.SetLink(logicComponent, localizationManager.CurrentLocalization.GetValue(logicComponent.LabelKey));
            newUIComp.onComponentClick += OnComponentClick;
            showedComponents.Add(newUIComp);
        }

        contentText.text = "";
    }

    private void OnComponentClick(UITerminalComponent clickedComponent)
    {
        StopAllCoroutines();
        
        StartCoroutine(WriteContentSmooth(localizationManager.CurrentLocalization.GetValue(clickedComponent.LinkedComponent.ContentKey)));
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