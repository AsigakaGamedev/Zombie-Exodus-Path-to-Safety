using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Walk, Run}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform camBody;
    [SerializeField] private float camMinRotAngle = -70;
    [SerializeField] private float camMaxRotAngle = 90;

    [Space]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private InventoryController inventory;
    [SerializeField] private InteractionsController interactions;
    [SerializeField] private AnimationsController animations;
    [SerializeField] private WeaponsController weapons;
    [SerializeField] private NeedsController needs;
    [SerializeField] private HealthComponent health;

    [Space]
    [SerializeField] private float walkSpeed = 2.6f;
    [SerializeField] private float runSpeed = 3.5f;
    [SerializeField] private float lookSensitivity = 100;

    [Space]
    [ReadOnly, SerializeField] private float currentSpeed;
    [ReadOnly, SerializeField] private PlayerState state;

    private UIManager uiManager;
    private UIMobilePlayerInputs playerInputs;
    private Joystick moveJoystick;
    private Joystick lookJoystick;

    private Vector3 moveInput;

    private float xRotation;

    public InventoryController Inventory { get => inventory; }
    public NeedsController Needs { get => needs; }
    public HealthComponent Health { get => health; }

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    public void Start()
    {
        currentSpeed = walkSpeed;
        state = PlayerState.Walk;
        
        uiManager = ServiceLocator.GetService<UIManager>();

        playerInputs = ServiceLocator.GetService<UIMobilePlayerInputs>();
        moveJoystick = playerInputs.MoveJoystick;
        lookJoystick = playerInputs.LookJoystick;

        inventory.Init();
        inventory.onItemUse += OnItemUse;

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

        playerInputs.onStartRun += OnStartRun;
        playerInputs.onEndRun += OnEndRun;

        interactions.onFindInteractable += OnFindInteractable;
        interactions.onLoseInteractable += OnLoseInteractable;

        needs.Init();

        health.onDie += OnDie;
    }

    private void OnDestroy()
    {
        playerInputs.onStartRun -= OnStartRun;
        playerInputs.onEndRun -= OnEndRun;

        interactions.onFindInteractable -= OnFindInteractable;
        interactions.onLoseInteractable -= OnLoseInteractable;

        weapons.onEquip -= OnEquipWeapon;
        weapons.onDequip -= OnDequipWeapon;

        inventory.onItemUse -= OnItemUse;
        inventory.Destroy();

        health.onDie -= OnDie;
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
        xRotation = Mathf.Clamp(xRotation, camMinRotAngle, camMaxRotAngle);
        camBody.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    private void Movement()
    {
        moveInput = new Vector3(moveJoystick.Horizontal, 0, moveJoystick.Vertical);

        float zMove = moveInput.z;
        if (state == PlayerState.Run) zMove = 1;

        Vector3 movement = moveInput.x * transform.right + zMove * transform.forward;
        animations.MoveTo(movement);

        movement *= currentSpeed;
        movement.y = rb.velocity.y;

        rb.velocity = movement;
    }

    #region Listeners

    private void OnStartRun()
    {
        currentSpeed = runSpeed;
        state = PlayerState.Run;
    }

    private void OnEndRun()
    {
        currentSpeed = walkSpeed;
        state = PlayerState.Walk;
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

    private void OnItemUse(ItemEntity item)
    {
        foreach (ItemUseData useData in item.InfoPrefab.UseDatas)
        {
            needs.GetNeed(useData.NeedID).Value += useData.NeedIncreaseValue;
        }
    }

    private void OnFindInteractable()
    {
        playerInputs.InteractBtn.gameObject.SetActive(true);
    }

    private void OnLoseInteractable()
    {
        playerInputs.InteractBtn.gameObject.SetActive(false);
    }

    private void OnDie()
    {
        uiManager.ChangeScreen("die");
    }

    #endregion
}
