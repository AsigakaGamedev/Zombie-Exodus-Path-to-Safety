using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIScreenPartBtn : MonoBehaviour
{
    [SerializeField] private bool changeInteractable = true;
    [SerializeField] private Button btn;

    [Space]
    [SerializeField] private GameObject[] linkedObjects;

    public Button Btn { get => btn; }

    private void OnValidate()
    {
        if (!btn) btn = GetComponent<Button>();
    }

    public void ShowPart()
    {
        foreach (var obj in linkedObjects)
            obj.SetActive(true);

        if (changeInteractable) btn.interactable = false;
    }

    public void HidePart()
    {
        foreach (var obj in linkedObjects)
            obj.SetActive(false);

        if (changeInteractable) btn.interactable = true;
    }
}
