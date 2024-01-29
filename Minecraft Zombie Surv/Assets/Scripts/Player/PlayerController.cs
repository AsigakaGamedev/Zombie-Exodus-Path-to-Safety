using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform camBody;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private InventoryController inventory;
    [SerializeField] private InteractionsController interactions;
    [SerializeField] private AnimationsController animations;
    [SerializeField] private WeaponsController weapons;

    [Space]
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float lookSensitivity = 100;

    private UIMobilePlayerInputs playerInputs;
    private Joystick moveJoystick;
    private Joystick lookJoystick;

    private Vector3 moveInput;

    private float xRotation;

    public InventoryController Inventory { get => inventory; }
 
    public void Start()
    {
        Application.targetFrameRate = 60;
        playerInputs = ServiceLocator.GetService<UIMobilePlayerInputs>();
        moveJoystick = playerInputs.MoveJoystick;
        lookJoystick = playerInputs.LookJoystick;

        inventory.Init();
        
        weapons.onEquip += OnEquipWeapon;
        weapons.onDequip += OnDequipWeapon;
        weapons.Init();

        playerInputs.InteractBtn.onClick.AddListener(() =>
        {
            interactions.InteractWithCurrent(this);
        });

        playerInputs.AttackBtn.onClick.AddListener(() =>
        {
            if (weapons.TryAttack())
            {
                animations.SetAttackTrigger();
            }
        });

        playerInputs.InteractBtn.gameObject.SetActive(false);
        playerInputs.AttackBtn.gameObject.SetActive(false);

        interactions.onFindInteractable += OnFindInteractable;
        interactions.onLoseInteractable += OnLoseInteractable;
    }

    private void OnDestroy()
    {
        interactions.onFindInteractable -= OnFindInteractable;
        interactions.onLoseInteractable -= OnLoseInteractable;

        weapons.onEquip -= OnEquipWeapon;
        weapons.onDequip -= OnDequipWeapon;
    }

    private void Update()
    {
        interactions.CheckInteractionsFront();

        Looking();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Looking()
    {
        transform.Rotate(0, lookJoystick.Horizontal * Time.deltaTime * lookSensitivity, 0);

        xRotation -= lookJoystick.Vertical * Time.deltaTime * lookSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        camBody.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    private void Movement()
    {
        moveInput = new Vector3(moveJoystick.Horizontal, 0, moveJoystick.Vertical);
        animations.MoveTo(moveInput);

        Vector3 movement = moveInput.x * transform.right + moveInput.z * transform.forward;
        movement *= moveSpeed;
        movement.y = rb.velocity.y;

        rb.velocity = movement;
    }


    private void OnEquipWeapon(Weapon weapon)
    {
        animations.SetAnimType(weapon.AnimTypeIndex);
        playerInputs.AttackBtn.gameObject.SetActive(true);
    }

    private void OnDequipWeapon()
    {
        playerInputs.AttackBtn.gameObject.SetActive(false);
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
