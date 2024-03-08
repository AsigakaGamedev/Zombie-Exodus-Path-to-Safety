using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] private int startWeaponID = -1;
    [SerializeField] private SerializedDictionary<int, WeaponModel> allWeapons;

    [Space]
    [ReadOnly, SerializeField] private WeaponModel weaponInHands;

    public Action<WeaponModel> onEquip;
    public Action onDequip;
    public Action onReloadEnd;

    public WeaponModel WeaponInHands { get => weaponInHands; }

    public void Init()
    {
        foreach (var weapon in allWeapons.Values)
        {
            weapon.Init();
            weapon.gameObject.SetActive(false);
        }

        if (startWeaponID != -1) EquipWeapon(startWeaponID);
    }

    public bool TryAttack()
    {
        if (weaponInHands == null) return false;

        return weaponInHands.TryAttack();
    }

    public bool TryReload()
    {
        if (weaponInHands == null ||
            weaponInHands != null && !weaponInHands.NeedAmmo) return false;

        return weaponInHands.TryStartReload();
    }

    public void EquipWeapon(int weaponID)
    {
        if (weaponInHands)
        {
            onDequip?.Invoke();
            weaponInHands.onReloadEnd -= OnReloadEnd;
        }

        weaponInHands = allWeapons[weaponID];
        weaponInHands.OnEquip();
        onEquip?.Invoke(weaponInHands);
        onDequip += weaponInHands.OnDequip;
        onDequip += DequipWeapon;
        weaponInHands.onReloadEnd += OnReloadEnd;
    }

    public void DequipWeapon()
    {
        onDequip -= weaponInHands.OnDequip;
        onDequip -= DequipWeapon;
        weaponInHands.onReloadEnd -= OnReloadEnd;
    }

    private void OnReloadEnd()
    {
        onReloadEnd?.Invoke();
    }
}
