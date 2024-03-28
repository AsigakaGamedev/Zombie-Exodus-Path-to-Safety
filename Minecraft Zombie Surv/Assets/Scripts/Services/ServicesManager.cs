using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using Zenject;

public class ServicesManager : MonoBehaviour
{
    [SerializeField] private GameEconomyService economy;
    [SerializeField] private GameCloudService cloud;
    [SerializeField] private AdsService ads;

    //[Space]
    //[SerializeField] private GameObject loadingScreen; 

    [Space]
    [SerializeField] private bool autoSignIn = true;

    private PlayerManager playerManager;

    private const string loginPrefsKey = "PLAYER_LOGIN";
    private const string passwordPrefsKey = "PLAYER_PASSWORD";

    public Action onInitialized;

    public GameEconomyService Economy { get => economy; }
    public GameCloudService Cloud { get => cloud; }
    public AdsService Ads { get => ads; set => ads = value; }

    public bool IsInitialized { get; private set; }

    public static ServicesManager Instance;

    [Inject]
    private void Construct(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
    }

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);

        Instance = this;

        //loadingScreen.SetActive(false);

        IsInitialized = false;
    }

    private void OnDestroy()
    {
        ads.DestroyService();
        Instance = null;
    }

    public async Task StartServices()
    {
        //loadingScreen.SetActive(true);
        await UnityServices.InitializeAsync();
        await InitializeAllServices();
        //loadingScreen.SetActive(false);

        if (autoSignIn)
        {
            await TryAutoSignIn();
        }
    }

    private async Task InitializeAllServices()
    {
        ads.InitService();

        await cloud.CheckServices();

        onInitialized?.Invoke();
        IsInitialized = true;
    }

    private async Task TryAutoSignIn()
    {
        //loadingScreen.SetActive(true);
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

        //loadingScreen.SetActive(false);
    }

    public async Task TrySignUp(string login, string password)
    {
        await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(login, password);
        //Debug.Log("SignUp is successful.");

        PlayerPrefs.SetString(loginPrefsKey, login);
        PlayerPrefs.SetString(passwordPrefsKey, password);

        playerManager.SetNickname(login);

        await cloud.SavePlayerData();
        await economy.Refresh();
    }

    public async Task TrySignIn(string login, string password)
    {
        await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(login, password);
        //Debug.Log("SignIn is successful.");

        PlayerPrefs.SetString(loginPrefsKey, login);
        PlayerPrefs.SetString(passwordPrefsKey,password);

        await cloud.LoadPlayerData();
        await economy.Refresh();
    }
}
