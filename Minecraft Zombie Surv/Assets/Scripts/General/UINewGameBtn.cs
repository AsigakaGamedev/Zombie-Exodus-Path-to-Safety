using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINewGameBtn : MonoBehaviour
{
    [SerializeField] private Button newGameBtn;
    [Scene, SerializeField] private string firstLevelScene;

    private LoadingManager loadingManager;

    private void Start()
    {
        loadingManager = ServiceLocator.GetService<LoadingManager>();

        newGameBtn.onClick.AddListener(async () =>
        {
            newGameBtn.interactable = false;
            await loadingManager.LoadSceneAsync(firstLevelScene);
        });
    }
}
