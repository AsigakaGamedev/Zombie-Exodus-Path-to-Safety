using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private AInitializable[] systemManagers;

    [Space]
    [SerializeField] private Button offlineBtn;

    [Space]
    [Scene, SerializeField] private string mainMenuScene;

    private LoadingManager loadingManager;
    private ServicesManager servicesManager;

    private async void Start()
    {
        loadingManager = ServiceLocator.GetService<LoadingManager>();
        //servicesManager = ServiceLocator.GetService<ServicesManager>();

        foreach (var manager in systemManagers)
        {
            manager.OnInit();
        }

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
