using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UISceneChanger : MonoBehaviour
{
    [Scene, SerializeField] private string sceneName;

    [Space]
    [SerializeField] private float delay;

    private Button button;
    private bool isClicked;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnBtnClick);
    }

    private void OnBtnClick()
    {
        if (isClicked) return;

        isClicked = true;   
        Invoke(nameof(ChangeScene), delay);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
