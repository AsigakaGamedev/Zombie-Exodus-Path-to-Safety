using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UISceneChanger : MonoBehaviour
{
    [Scene, SerializeField] private string sceneName;

    [Space]
    [SerializeField] private float delay;

    private Button button;
    private bool isClicked;
    private LoadingManager loadingManager;

    private void OnValidate()
    {
        if (!button) button = GetComponent<Button>();
    }

    [Inject]
    private void Construct(LoadingManager loadingManager)
    {
        this.loadingManager = loadingManager;
    }

    private void Start()
    {
        isClicked = false;

        button.onClick.AddListener(() =>
        {
            if (isClicked) return;

            isClicked = true;
            Invoke(nameof(ChangeScene), delay);
        });
    }

    private async void ChangeScene()
    {
        await loadingManager.LoadSceneAsync(sceneName);
    }
}
