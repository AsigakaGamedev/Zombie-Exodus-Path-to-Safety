using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIChoosesPopup : APopup
{
    [SerializeField] private Button[] allButtons;
    [SerializeField] private TextMeshProUGUI hintText;

    private UnityAction[] curEvents;

    public void ShowPopup(string hint, UnityAction[] events)
    {
        hintText.text = hint;
        curEvents = events;

        foreach (Button btn in allButtons)
        {
            btn.onClick.RemoveAllListeners();
            btn.gameObject.SetActive(false);
        }

        for (int i = 0; i < curEvents.Length; i++)
        {
            int index = i;

            allButtons[i].gameObject.SetActive(true);
            allButtons[i].onClick.AddListener(() =>
            {
                curEvents[index]?.Invoke();
            });
        }
    }
}
