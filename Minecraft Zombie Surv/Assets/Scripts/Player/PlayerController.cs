using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [Space]
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float rotateSpeed = 2;

    private Joystick moveJoystick;
    private Joystick lookJoystick;

    private Vector3 moveInput;

    private void Start()
    {
        UIMobilePlayerInputs playerInputs = ServiceLocator.GetService<UIMobilePlayerInputs>();
        moveJoystick = playerInputs.MoveJoystick;
        lookJoystick = playerInputs.LookJoystick;
        playerInputs.enabled = false;
    }

    private void Update()
    {
        moveInput = new Vector3(moveJoystick.Horizontal, 0, moveJoystick.Vertical);
        Vector3 lookInput = new Vector3(lookJoystick.Horizontal, 0, lookJoystick.Vertical);

        if (lookInput.sqrMagnitude == 0 && moveInput.sqrMagnitude != 0)
        {
            SmoothRotate(moveInput);
        }
        else if (lookInput.sqrMagnitude != 0)
        {
            SmoothRotate(lookInput);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
    }

    private void SmoothRotate(Vector3 dir)
    {
        Quaternion lookRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * rotateSpeed);
    }
}
