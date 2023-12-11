using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenPartsManager : MonoBehaviour
{
    [SerializeField] private UIScreenPartBtn startPart;
    [SerializeField] private UIScreenPartBtn[] partsButtons;

    [Space]
    [ReadOnly, SerializeField] private UIScreenPartBtn curPart;

    private void Start()
    {
        foreach (var part in partsButtons)
        {
            part.Btn.onClick.AddListener(() =>
            {
                ShowPart(part);
            });

            part.HidePart();
        }

        ShowPart(startPart);
    }

    private void ShowPart(UIScreenPartBtn part)
    {
        if (curPart) curPart.HidePart();

        part.ShowPart();
        curPart = part;
    }
}
