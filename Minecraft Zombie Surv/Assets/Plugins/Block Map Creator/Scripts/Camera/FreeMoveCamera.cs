using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMoveCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [Space]
    [SerializeField] private float moveSpeed = 4;
    [SerializeField] private float rotSpeed = 2;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveInput = moveInput.z * cam.transform.forward + moveInput.x * cam.transform.right;

        float speed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift)) speed *= 2;

        cam.transform.position += moveInput * speed * Time.deltaTime;

        Vector2 lookInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        cam.transform.localEulerAngles += new Vector3(-lookInput.y * rotSpeed, lookInput.x * rotSpeed);
    }
}
