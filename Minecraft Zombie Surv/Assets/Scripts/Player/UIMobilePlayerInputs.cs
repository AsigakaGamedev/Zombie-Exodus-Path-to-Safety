using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMobilePlayerInputs : MonoBehaviour
{
    [SerializeField] private Joystick moveJoystick;
    [SerializeField] private Joystick lookJoystick;

    [Space]
    [SerializeField] private Button interactBtn;

    public Joystick MoveJoystick { get => moveJoystick; }
    public Joystick LookJoystick { get => lookJoystick; }

    public Button InteractBtn { get => interactBtn; }

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }
}
