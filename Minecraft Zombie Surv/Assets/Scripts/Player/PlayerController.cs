using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum PlayerState { Walk, Run}

public class PlayerController : AInitializable
{
    [SerializeField] private Transform camParent;
    [SerializeField] private Transform camBody;
    [SerializeField] private Transform bodyTarget;
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
    [SerializeField] private float lookSensitivity = 115;

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

    private void OnValidate()
    {
        if (camParent && camBody) camBody.position = camParent.position;
    }

    [Inject]
    private void Construct(UIMobilePlayerInputs mobilePlayerInputs, UIManager uiManager)
    {
        this.playerInputs = mobilePlayerInputs;
        this.uiManager = uiManager;
    }

    public override void OnInit()
    {
        currentSpeed = walkSpeed;
        state = PlayerState.Walk;

        moveJoystick = playerInputs.MoveJoystick;
        lookJoystick = playerInputs.LookJoystick;

        inventory.Init();
        inventory.onItemUse += OnItemUse;
        inventory.onItemEquip += OnItemEquiped;
        inventory.onItemAdd += OnItemAddOrRemove;
        inventory.onItemRemove += OnItemAddOrRemove;

        weapons.onEquip += OnEquipWeapon;
        weapons.onDequip += OnDequipWeapon;
        weapons.onReloadEnd += OnReloadEnd;
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

                if (weapons.WeaponInHands.NeedAmmo)
                {
                    playerInputs.UpdateAmmoInfo(weapons.WeaponInHands.AmmoInMagazine, inventory.GetItemsAmount(weapons.WeaponInHands.AmmoInfo));
                }
            }
        });

        playerInputs.ReloadBtn.onClick.AddListener(() =>
        {
            if (weapons.TryReload())
            {
                animations.SetTrigger("reload");

                playerInputs.UpdateAmmoInfo(weapons.WeaponInHands.AmmoInMagazine, inventory.GetItemsAmount(weapons.WeaponInHands.AmmoInfo));
            }
        });

        playerInputs.InteractBtn.gameObject.SetActive(false);
        playerInputs.AttackBtn.gameObject.SetActive(false);
        playerInputs.ReloadBtn.gameObject.SetActive(false);

        playerInputs.onStartRun += OnStartRun;
        playerInputs.onEndRun += OnEndRun;

        interactions.onFindInteractable += OnFindInteractable;
        interactions.onLoseInteractable += OnLoseInteractable;

        needs.Init();

        health.onDie += OnDie;

        playerInputs.SetAmmoPanel(false);
    }

    private void OnDestroy()
    {
        playerInputs.onStartRun -= OnStartRun;
        playerInputs.onEndRun -= OnEndRun;

        interactions.onFindInteractable -= OnFindInteractable;
        interactions.onLoseInteractable -= OnLoseInteractable;

        inventory.onItemUse -= OnItemUse;
        inventory.onItemEquip -= OnItemEquiped;
        inventory.onItemAdd -= OnItemAddOrRemove;
        inventory.onItemRemove -= OnItemAddOrRemove;
        inventory.Destroy();

        weapons.onEquip -= OnEquipWeapon;
        weapons.onDequip -= OnDequipWeapon;
        weapons.onReloadEnd -= OnReloadEnd;

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

    private void LateUpdate()
    {
        camBody.position = camParent.position;
    }

    private void Looking()
    {
        transform.Rotate(0, lookJoystick.Horizontal * Time.deltaTime * lookSensitivity, 0);

        xRotation -= lookJoystick.Vertical * Time.deltaTime * lookSensitivity;
        xRotation = Mathf.Clamp(xRotation, camMinRotAngle, camMaxRotAngle);

        camBody.localRotation = Quaternion.Euler(xRotation, 0, 0);

        bodyTarget.position = new Ray(camBody.position, camBody.forward).GetPoint(5);
    }

    private void Movement()
    {
        moveInput = new Vector3(moveJoystick.Horizontal, 0, moveJoystick.Vertical);
        animations.SetMove(moveInput.sqrMagnitude >= 0.2f);

        float zMove = moveInput.z;
        if (state == PlayerState.Run) zMove = 1;

        Vector3 movement = moveInput.x * transform.right + zMove * transform.forward;

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

    private void OnEquipWeapon(WeaponModel weapon)
    {
        animations.SetTrigger(weapon.AnimKey);
        playerInputs.AttackBtn.gameObject.SetActive(true);
        playerInputs.ReloadBtn.gameObject.SetActive(weapon.NeedAmmo);
        playerInputs.SetAmmoPanel(weapon.NeedAmmo);

        if (weapon.NeedAmmo)
        {
            playerInputs.UpdateAmmoInfo(weapon.AmmoInMagazine, inventory.GetItemsAmount(weapon.AmmoInfo));
        }
    }

    private void OnDequipWeapon()
    {
        animations.SetTrigger("dequip");
        playerInputs.AttackBtn.gameObject.SetActive(false);
        playerInputs.ReloadBtn.gameObject.SetActive(false);
        playerInputs.SetAmmoPanel(false);
    }

    private void OnItemEquiped(ItemEntity item)
    {
        if (item.InfoPrefab.IsWeapon)
        {
            weapons.EquipWeapon(item.InfoPrefab.WeaponID);
        }
    }

    private void OnItemAddOrRemove(ItemEntity item)
    {
        if (weapons.WeaponInHands)
        {
            playerInputs.UpdateAmmoInfo(weapons.WeaponInHands.AmmoInMagazine, inventory.GetItemsAmount(weapons.WeaponInHands.AmmoInfo));
        }
    }

    private void OnItemUse(ItemEntity item)
    {
        foreach (ItemUseData useData in item.InfoPrefab.UseDatas)
        {
            needs.GetNeed(useData.NeedID).Value += useData.NeedIncreaseValue;
        }

        if (item.InfoPrefab.HealthIncrease != 0)
        {
            health.ChangeHealth(item.InfoPrefab.HealthIncrease);
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

    private void OnReloadEnd()
    {
        inventory.TryRemoveItem(weapons.WeaponInHands.AmmoInfo, weapons.WeaponInHands.AmmoInMagazine);
        playerInputs.UpdateAmmoInfo(weapons.WeaponInHands.AmmoInMagazine, inventory.GetItemsAmount(weapons.WeaponInHands.AmmoInfo));
    }

    #endregion
}
