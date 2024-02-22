using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMobilePlayerInputs : MonoBehaviour
{
    [Header("Joysticks")]
    [SerializeField] private Joystick moveJoystick;
    [SerializeField] private Joystick lookJoystick;

    [Header("Buttons")]
    [SerializeField] private Button interactBtn;
    [SerializeField] private Button attackBtn;
    [SerializeField] private Button reloadBtn;

    [Header("Ammo")]
    [SerializeField] private GameObject ammoInfoPanel;
    [SerializeField] private TextMeshProUGUI ammoInfoText;

    public Joystick MoveJoystick { get => moveJoystick; }
    public Joystick LookJoystick { get => lookJoystick; }

    public Button InteractBtn { get => interactBtn; }
    public Button AttackBtn { get => attackBtn; }
    public Button ReloadBtn { get => reloadBtn; }

    public Action onStartRun;
    public Action onEndRun;

    public void OnStartRun()
    {
        onStartRun?.Invoke();
    }

    public void OnEndRun()
    {
        onEndRun?.Invoke();
    }

    public void SetAmmoPanel(bool isActive)
    {
        ammoInfoPanel.SetActive(isActive);
        ammoInfoText.gameObject.SetActive(isActive);
    }

    public void UpdateAmmoInfo(int ammoInMagazine, int ammoInInventory)
    {
        ammoInfoText.text = $"{ammoInMagazine}/{ammoInInventory}";
    }
}
