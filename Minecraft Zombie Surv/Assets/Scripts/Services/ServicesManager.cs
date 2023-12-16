using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class ServicesManager : MonoBehaviour
{
    [SerializeField] private GameEconomyService economy;
    [SerializeField] private GameCloudService cloud;
    [SerializeField] private AdsService ads;

    [Space]
    [Scene, SerializeField] private string mainMenuScene;

    public GameEconomyService Economy { get => economy; }
    public GameCloudService Cloud { get => cloud; }
    public AdsService Ads { get => ads; set => ads = value; }

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    private async void Start()
    {
        await UnityServices.InitializeAsync();

        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        print($"Player Authenticated {AuthenticationService.Instance.PlayerId}");

        economy.Refresh();

        cloud.Init();
        cloud.LoadPlayerData();

        ServiceLocator.GetService<LoadingManager>().LoadScene(mainMenuScene);
    }
}
