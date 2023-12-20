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

    private ServicesManager servicesManager;

    private void Start()
    {
        servicesManager = ServiceLocator.GetService<ServicesManager>();

        signInBtn.onClick.AddListener(async () =>
        {
            signInBtn.interactable = false;
            signUpBtn.interactable = false;
            await servicesManager.TrySignIn(loginInput.text, passwordInput.text);
        });

        signUpBtn.onClick.AddListener(async () =>
        {
            signInBtn.interactable = false;
            signUpBtn.interactable = false;
            await servicesManager.TrySignUp(loginInput.text, passwordInput.text);
        });
    }
}
