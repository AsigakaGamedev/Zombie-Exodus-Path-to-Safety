using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMobilePlayerInputs : MonoBehaviour
{
    [SerializeField] private Joystick moveJoystick;
    [SerializeField] private Joystick lookJoystick;

    public Joystick MoveJoystick { get => moveJoystick; }
    public Joystick LookJoystick { get => lookJoystick; }

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }
}
