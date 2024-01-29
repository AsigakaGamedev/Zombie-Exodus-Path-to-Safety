using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private InventoryController inventory;
    [SerializeField] private InteractionsController interactions;
    [SerializeField] private AnimationsController animations;
    [SerializeField] private WeaponsController weapons;

    [Space]
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float rotateSpeed = 5;

    [Space]
    [SerializeField] private float attackMagnitude = 0.7f;

    private UIMobilePlayerInputs playerInputs;
    private Joystick moveJoystick;
    private Joystick lookJoystick;

    private Vector3 moveInput;

    public InventoryController Inventory { get => inventory; }

    public void Init()
    {
        inventory.Init();

        weapons.onEquip += OnEquipWeapon;
        weapons.Init();

        playerInputs = ServiceLocator.GetService<UIMobilePlayerInputs>();
        moveJoystick = playerInputs.MoveJoystick;
        lookJoystick = playerInputs.LookJoystick;

        playerInputs.InteractBtn.onClick.AddListener(() =>
        {
            //interactions.InteractWithCurrent(this);
        });

        playerInputs.InteractBtn.gameObject.SetActive(false);

        interactions.onFindInteractable += OnFindInteractable;
        interactions.onLoseInteractable += OnLoseInteractable;
    }

    private void OnDestroy()
    {
        interactions.onFindInteractable -= OnFindInteractable;
        interactions.onLoseInteractable -= OnLoseInteractable;

        weapons.onEquip -= OnEquipWeapon;
    }

    private void Update()
    {
        interactions.CheckInteractions();

        moveInput = new Vector3(moveJoystick.Horizontal, 0, moveJoystick.Vertical);
        animations.MoveTo(moveInput);

        Vector3 lookInput = new Vector3(lookJoystick.Horizontal, 0, lookJoystick.Vertical);

        if (lookInput.sqrMagnitude == 0 && moveInput.sqrMagnitude != 0)
        {
            SmoothRotate(moveInput);
        }
        else if (lookInput.sqrMagnitude != 0)
        {
            SmoothRotate(lookInput);
        }

        if (lookInput.sqrMagnitude >= attackMagnitude)
        {
            if (weapons.TryAttack())
            {
                animations.SetAttackTrigger();
            }
        }

        moveInput *= moveSpeed * Time.deltaTime * 100;
        moveInput.y = rb.velocity.y;
        rb.velocity = moveInput;
    }

    private void OnEquipWeapon(Weapon weapon)
    {
        animations.SetAnimType(weapon.AnimTypeIndex);
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
