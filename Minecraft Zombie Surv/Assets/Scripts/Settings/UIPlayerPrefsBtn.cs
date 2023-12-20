using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIPlayerPrefsBtn : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private string value;

    [Space]
    [SerializeField] private Button btn;

    private void OnValidate()
    {
        if (!btn) btn = GetComponent<Button>();
    }

    private void Start()
    {
        btn.onClick.AddListener(() =>
        {
            PlayerPrefs.SetString(key, value);
        });
    }
}
