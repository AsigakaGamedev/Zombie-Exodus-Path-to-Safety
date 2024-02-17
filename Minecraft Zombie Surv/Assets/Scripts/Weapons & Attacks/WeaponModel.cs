using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public enum WeaponType { Pistol, Rifle}

public class WeaponModel : EquipmentModel
{
    [SerializeField] private AttacksHandler attacksHandler;

    [Space]
    [SerializeField] private WeaponType weaponType;

    [Header("Reloading")]
    [SerializeField] private bool hasReloading;

    [Header("Ammo")]
    [SerializeField] private bool needAmmo;
    [SerializeField] private ItemInfo ammoInfo;

    public WeaponType WeaponType { get => weaponType; }

    public bool HasReloading { get => hasReloading; }

    public bool NeedAmmo { get => needAmmo; }
    public ItemInfo AmmoInfo { get => ammoInfo; }

    public void Init()
    {
        attacksHandler.Init();
    }

    public bool TryAttack()
    {
        return attacksHandler.TryAttack();
    }
}
