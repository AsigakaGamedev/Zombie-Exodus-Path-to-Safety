using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private AInitializable[] systemManagers;

    [Space]
    [Scene, SerializeField] private string mainMenuScene;

    private LoadingManager loadingManager;
    private ServicesManager servicesManager;

    private async void Start()
    {
        loadingManager = ServiceLocator.GetService<LoadingManager>();
        servicesManager = ServiceLocator.GetService<ServicesManager>();

        foreach (var manager in systemManagers)
        {
            manager.OnInit();
        }

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
             await loadingManager.LoadSceneAsync(mainMenuScene);
        }
        else
        {
            await servicesManager.StartServices();
            await loadingManager.LoadSceneAsync(mainMenuScene);
        }
    }
}
