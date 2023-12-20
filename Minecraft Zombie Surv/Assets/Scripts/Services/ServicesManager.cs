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
    [SerializeField] private bool autoSignIn = true;

    [Space]
    [Scene, SerializeField] private string mainMenuScene;

    private LoadingManager loadingManager;

    private const string loginPrefsKey = "PLAYER_LOGIN";
    private const string passwordPrefsKey = "PLAYER_PASSWORD";

    public Action onInitialized;

    public GameEconomyService Economy { get => economy; }
    public GameCloudService Cloud { get => cloud; }
    public AdsService Ads { get => ads; set => ads = value; }

    public bool IsInitialized { get; private set; }

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
        loadingManager = ServiceLocator.GetService<LoadingManager>();

        IsInitialized = false;
        await UnityServices.InitializeAsync();

        if (autoSignIn)
        {
            await TryAutoSignIn();
        }
    }

    private void OnDestroy()
    {
        ads.DestroyService();
    }

    private async Task InitializeAllServices()
    {
        ads.InitService();

        await economy.Refresh();

        await cloud.Init();

        onInitialized?.Invoke();
        IsInitialized = true;
        await economy.Refresh();
    }

    private async Task TryAutoSignIn()
    {
        string existingLogin = PlayerPrefs.GetString(loginPrefsKey, "");
        string existingPassword = PlayerPrefs.GetString(passwordPrefsKey, "");

        if (!string.IsNullOrEmpty(existingLogin) && !string.IsNullOrEmpty(existingPassword))
        {
            await TrySignIn(existingLogin, existingPassword);

            //try
            //{
            //    await TrySignIn(existingLogin, existingPassword);
            //}
            //catch
            //{
            //    PlayerPrefs.DeleteKey(loginPrefsKey);
            //    PlayerPrefs.DeleteKey(passwordPrefsKey);
            //}
        }
    }

    public async Task TrySignUp(string login, string password)
    {
        await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(login, password);
        Debug.Log("SignUp is successful.");

        PlayerPrefs.SetString(loginPrefsKey, login);
        PlayerPrefs.SetString(passwordPrefsKey, password);


        ServiceLocator.GetService<PlayerManager>().SetNickname(login);

        Task[] tasks = new Task[]
        {
             InitializeAllServices(),
             cloud.SavePlayerData()
        };

        await loadingManager.LoadSceneAsync(mainMenuScene, tasks);
    }

    public async Task TrySignIn(string login, string password)
    {
        await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(login, password);
        Debug.Log("SignIn is successful.");

        PlayerPrefs.SetString(loginPrefsKey, login);
        PlayerPrefs.SetString(passwordPrefsKey,password);

        Task[] tasks = new Task[]
        {
            InitializeAllServices(),
             cloud.LoadPlayerData()
        };

        await loadingManager.LoadSceneAsync(mainMenuScene, tasks);
    }
}
