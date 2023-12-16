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
    [SerializeField] private Button signInBtn;
    [SerializeField] private Button signUpBtn;

    [Space]
    [SerializeField] private bool autoSignIn = true;

    private const string loginPrefsKey = "PLAYER_LOGIN";
    private const string passwordPrefsKey = "PLAYER_PASSWORD";

    private ServicesManager servicesManager;
    private UIManager uiManager;

    private void Start()
    {
        servicesManager = ServiceLocator.GetService<ServicesManager>();
        uiManager = ServiceLocator.GetService<UIManager>();

        TrySignInExisting();

        signInBtn.onClick.AddListener(async () =>
        {
            try
            {
                await servicesManager.TrySignIn(loginInput.text, passwordInput.text);
                PlayerPrefs.SetString(loginPrefsKey, loginInput.text);
                PlayerPrefs.SetString(passwordPrefsKey, passwordInput.text);
                uiManager.ChangeScreen("main");
            }
            catch
            {
                uiManager.ChangeScreen("first_enter");
            }
        });

        signUpBtn.onClick.AddListener(async () =>
        {
            try
            {
                await servicesManager.TrySignUp(loginInput.text, passwordInput.text);
                PlayerPrefs.SetString(loginPrefsKey, loginInput.text);
                PlayerPrefs.SetString(passwordPrefsKey, passwordInput.text);
                await servicesManager.Cloud.SavePlayerData();
                uiManager.ChangeScreen("main");
            }
            catch
            {
                uiManager.ChangeScreen("first_enter");
            }
        });
    }

    private async void TrySignInExisting()
    {
        string existingLogin = PlayerPrefs.GetString(loginPrefsKey, "");
        string existingPassword = PlayerPrefs.GetString(passwordPrefsKey, "");

        if (existingLogin != "" && existingPassword != "")
        {
            try
            {
                await servicesManager.TrySignIn(existingLogin, existingPassword);
                uiManager.ChangeScreen("main");
            }
            catch 
            {
                PlayerPrefs.DeleteKey(loginPrefsKey);
                PlayerPrefs.DeleteKey(passwordPrefsKey);
            }
        }
    }
}
