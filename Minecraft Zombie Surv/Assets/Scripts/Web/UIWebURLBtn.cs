using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIWebURLBtn : MonoBehaviour
{
    [SerializeField] private string url;

    [Space]
    [SerializeField] private Button btn;

    private void OnValidate()
    {
        if (!btn) btn = GetComponent<Button>();
    }

    private void Awake()
    {
        btn.onClick.AddListener(() =>
        {
            Application.OpenURL(url);
        });
    }
}