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

    private ServicesManager servicesManager;
    private UIManager uiManager;

    private void Start()
    {
        servicesManager = ServiceLocator.GetService<ServicesManager>();
        uiManager = ServiceLocator.GetService<UIManager>();

        signInBtn.onClick.AddListener(async () =>
        {
            try
            {
                await servicesManager.TrySignIn("Asigaka", "12345678$Aa");
                //await servicesManager.TrySignIn(loginInput.text, passwordInput.text);
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
                uiManager.ChangeScreen("main");
            }
            catch
            {
                uiManager.ChangeScreen("first_enter");
            }
        });
    }
}
