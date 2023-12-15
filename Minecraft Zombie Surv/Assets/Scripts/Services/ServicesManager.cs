using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class ServicesManager : MonoBehaviour
{
    [SerializeField] private GameEconomyService economy;

    [Space]
    [Scene, SerializeField] private string mainMenuScene;

    public GameEconomyService Economy { get => economy; }

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

        ServiceLocator.GetService<LoadingManager>().LoadScene(mainMenuScene);
    }
}
