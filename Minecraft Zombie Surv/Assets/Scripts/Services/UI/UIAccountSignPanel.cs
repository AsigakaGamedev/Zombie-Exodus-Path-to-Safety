using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        servicesManager = ServiceLocator.GetService<ServicesManager>();

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
            await ServiceLocator.GetService<LoadingManager>().LoadSceneAsync(mainMenuScene);
        });
    }
}
