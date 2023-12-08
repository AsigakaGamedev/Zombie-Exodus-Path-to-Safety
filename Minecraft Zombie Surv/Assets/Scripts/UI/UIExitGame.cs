using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExitGame : MonoBehaviour
{
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
} 
