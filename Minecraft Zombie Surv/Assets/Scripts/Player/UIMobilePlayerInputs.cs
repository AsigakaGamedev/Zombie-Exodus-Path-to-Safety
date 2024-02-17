using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMobilePlayerInputs : MonoBehaviour
{
    [SerializeField] private Joystick moveJoystick;
    [SerializeField] private Joystick lookJoystick;

    [Space]
    [SerializeField] private Button interactBtn;
    [SerializeField] private Button attackBtn;
    [SerializeField] private Button reloadBtn;

    public Joystick MoveJoystick { get => moveJoystick; }
    public Joystick LookJoystick { get => lookJoystick; }

    public Button InteractBtn { get => interactBtn; }
    public Button AttackBtn { get => attackBtn; }
    public Button ReloadBtn { get => reloadBtn; }

    public Action onStartRun;
    public Action onEndRun;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    public void OnStartRun()
    {
        onStartRun?.Invoke();
    }

    public void OnEndRun()
    {
        onEndRun?.Invoke();
    }
}
