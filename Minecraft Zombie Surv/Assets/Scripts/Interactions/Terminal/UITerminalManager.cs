using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

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

    [Inject]
    private void Construct(ObjectPoolingManager poolingManager, LocalizationManager localizationManager)
    {
        this.poolingManager = poolingManager;
        this.localizationManager = localizationManager;
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