using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private string startScreen;
    [SerializeField] private UIScreen[] screens;

    [Space]
    [ReadOnly, SerializeField] private UIScreen curScreen;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    private void Start()
    {
        foreach (UIScreen screen in screens)
        {
            screen.Init();
        }

        ChangeScreen(startScreen);
    }

    private void Update()
    {
        if (curScreen)
        {
            curScreen.UpdateScreen();

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                Cursor.lockState = curScreen.CursorLockMode;
            }
        }
    }

    private UIScreen GetScreen(string screenName)
    {
        foreach (UIScreen screen in screens)
        {
            if (screen.ScreenName == screenName) return screen;
        }

        throw new System.Exception($"{screenName} экрана не существует!");
    }

    public void ChangeScreen(string screenName)
    {
        UIScreen nextScreen = GetScreen(screenName);

        if (curScreen && nextScreen != curScreen)
        {
            curScreen.HideScreen();
        }

        curScreen = nextScreen;
        curScreen.ShowScreen();
        Cursor.lockState = curScreen.CursorLockMode;
    }
}
