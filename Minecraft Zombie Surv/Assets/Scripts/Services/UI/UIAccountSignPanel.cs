using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIAccountSignPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField loginInput;
    [SerializeField] private TMP_InputField passwordInput;

    [Space]
    [Scene, SerializeField] private string mainMenuScene = "MainMenu";

    [Space]
    [SerializeField] private Button signInBtn;
    [SerializeField] private Button signUpBtn;
    [SerializeField] private Button offlineBtn;

    private ServicesManager servicesManager;
    private LoadingManager loadingManager;

    [Inject]
    private void Construct(ServicesManager servicesManager, LoadingManager loadingManager)
    {
        this.servicesManager = servicesManager;
        this.loadingManager = loadingManager;
    }

    private void Start()
    {
        signInBtn.onClick.AddListener(async () =>
        {
            signInBtn.interactable = false;
            signUpBtn.interactable = false;
            offlineBtn.interactable = false;
            await servicesManager.TrySignIn(loginInput.text, passwordInput.text);
        });

        signUpBtn.onClick.AddListener(async () =>
        {
            signInBtn.interactable = false;
            signUpBtn.interactable = false;
            offlineBtn.interactable = false;
            await servicesManager.TrySignUp(loginInput.text, passwordInput.text);
        });

        offlineBtn.onClick.AddListener(async () =>
        {
            signInBtn.interactable = false;
            signUpBtn.interactable = false;
            offlineBtn.interactable = false;
            await loadingManager.LoadSceneAsync(mainMenuScene);
        });
    }
}
