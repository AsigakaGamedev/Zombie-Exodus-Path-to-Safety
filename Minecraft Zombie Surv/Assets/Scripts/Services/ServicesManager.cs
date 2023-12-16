using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    public Action onInitialized;

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

        ServiceLocator.GetService<LoadingManager>().LoadScene(mainMenuScene);
    }

    private void OnDestroy()
    {
        ads.DestroyService();
    }

    private async void InitializeAllServices()
    {
        ads.InitService();

        await economy.Refresh();

        await cloud.Init();
        await cloud.LoadPlayerData();

        onInitialized?.Invoke();
    }

    public async Task TrySignUp(string login, string password)
    {
        await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(login, password);
        Debug.Log("SignUp is successful.");

        InitializeAllServices();
    }

    public async Task TrySignIn(string login, string password)
    {
        await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(login, password);
        Debug.Log("SignIn is successful.");

        InitializeAllServices();
    }
}
