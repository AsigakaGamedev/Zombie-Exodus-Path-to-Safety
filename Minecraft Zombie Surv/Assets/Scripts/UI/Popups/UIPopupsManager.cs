using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupsManager : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<string, APopup> allPopups;

    [Space]
    [ReadOnly, SerializeField] private APopup currentPopup; 

    private void Awake()
    {
        foreach (var popup in allPopups.Values)
        {
            popup.OnInit();
            popup.gameObject.SetActive(false);
        }
    }

    public T OpenPopup<T>(string popupKey) where T: APopup
    {
        if (currentPopup) currentPopup.OnClose();

        currentPopup = allPopups[popupKey];
        currentPopup.OnOpen();
        return currentPopup as T;
    }

    public void CloseCurrentPopup()
    {
        if (currentPopup) currentPopup.OnClose();
    }
}
