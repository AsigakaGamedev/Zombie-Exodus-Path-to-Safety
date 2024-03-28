using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Button offlineBtn;

    [Space]
    [Scene, SerializeField] private string mainMenuScene;

    private LoadingManager loadingManager;
    private ServicesManager servicesManager;

    [Inject]
    private void Construct(LoadingManager loadingManager)
    {
        this.loadingManager = loadingManager;
    }

    private async void Start()
    {
        //servicesManager = ServiceLocator.GetService<ServicesManager>();

        offlineBtn.onClick.AddListener(async () =>
        {
            await loadingManager.LoadSceneAsync(mainMenuScene);
            offlineBtn.interactable = false;
        });

        //if (Application.internetReachability == NetworkReachability.NotReachable)
        //{
        //     await loadingManager.LoadSceneAsync(mainMenuScene);
        //}
        //else
        //{
        //    await servicesManager.StartServices();
        //    await loadingManager.LoadSceneAsync(mainMenuScene);
        //}
        await loadingManager.LoadSceneAsync(mainMenuScene);
    }
}
