using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIScreen : MonoBehaviour
{
    [SerializeField] private string screenName;
    //[SerializeField] private RuntimePlatform targetPlatform = RuntimePlatform.Android;
    [SerializeField] private CursorLockMode cursorLockMode;

    private List<IScreenListener> listeners;

    //public RuntimePlatform TargetPlatform { get => targetPlatform; }
    public string ScreenName { get => screenName; }
    public CursorLockMode CursorLockMode { get => cursorLockMode; }

    public void Init()
    {
        listeners = GetComponentsInChildren<IScreenListener>(true).ToList();

        foreach (IScreenListener listener in listeners)
        {
            listener.OnScreenInit();
        }

        gameObject.SetActive(false);
    }

    public void UpdateScreen()
    {
        foreach (IScreenListener listener in listeners)
        {
            if (listener is IScreenUpdateListener updateListener) updateListener.OnScreenUpdate();
        }
    }

    public void ShowScreen()
    {
        gameObject.SetActive(true);

        foreach (IScreenListener listener in listeners)
        {
            if (listener is IScreenShowListener showListener) showListener.OnScreenShow();
        }
    }

    public void HideScreen()
    {
        gameObject.SetActive(false);

        foreach (IScreenListener listener in listeners)
        {
            if (listener is IScreenHideListener hideListener) hideListener.OnScreenHide();
        }
    }
}
