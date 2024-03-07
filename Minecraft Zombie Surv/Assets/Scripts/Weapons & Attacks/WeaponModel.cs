using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public enum WeaponType { Pistol, Rifle, Axe}

public class WeaponModel : EquipmentModel
{
    [SerializeField] private AttacksHandler attacksHandler;

    [Space]
    [SerializeField] private string animBoolKey;

    [Header("Reloading")]
    [SerializeField] private bool hasReloading;
    [ShowIf(nameof(hasReloading)), SerializeField] private float reloadingTime;

    [Header("Ammo")]
    [SerializeField] private bool needAmmo;
    [ShowIf(nameof(needAmmo)), SerializeField] private int magazineCapacity = 8;
    [ShowIf(nameof(needAmmo)), SerializeField] private ItemInfo ammoInfo;

    [Space]
    [ShowIf(nameof(needAmmo)), ReadOnly, SerializeField] private int ammoInMagazine;

    public bool HasReloading { get => hasReloading; }

    public bool NeedAmmo { get => needAmmo; }
    public ItemInfo AmmoInfo { get => ammoInfo; }
    public int AmmoInMagazine { get => ammoInMagazine; }

    public string AnimKey { get => animBoolKey;}

    public void Init()
    {
        attacksHandler.Init();
    }

    public bool TryAttack()
    {
        return attacksHandler.TryAttack();
    }
}
