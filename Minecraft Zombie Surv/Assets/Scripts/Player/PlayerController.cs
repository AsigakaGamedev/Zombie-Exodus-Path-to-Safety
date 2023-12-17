using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private InventoryController inventory;
    [SerializeField] private InteractionsController interactions;

    [Space]
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float rotateSpeed = 2;

    private UIMobilePlayerInputs playerInputs;
    private Joystick moveJoystick;
    private Joystick lookJoystick;

    private Vector3 moveInput;

    public InventoryController Inventory { get => inventory; }

    public void Init()
    {
        inventory.Init();

        playerInputs = ServiceLocator.GetService<UIMobilePlayerInputs>();
        moveJoystick = playerInputs.MoveJoystick;
        lookJoystick = playerInputs.LookJoystick;

        playerInputs.InteractBtn.onClick.AddListener(() =>
        {
            interactions.InteractWithCurrent(this);
        });

        playerInputs.InteractBtn.gameObject.SetActive(false);

        interactions.onFindInteractable += OnFindInteractable;
        interactions.onLoseInteractable += OnLoseInteractable;
    }

    private void OnDestroy()
    {
        interactions.onFindInteractable -= OnFindInteractable;
        interactions.onLoseInteractable -= OnLoseInteractable;
    }

    private void Update()
    {
        interactions.CheckInteractions();

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

    private void OnFindInteractable()
    {
        playerInputs.InteractBtn.gameObject.SetActive(true);
    }

    private void OnLoseInteractable()
    {
        playerInputs.InteractBtn.gameObject.SetActive(false);
    }
}
